using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

using ClosedXML.Excel;

using Microsoft.Extensions.Localization;

using NGErp.Base.Service.ResponseModels;

namespace NGErp.Base.Service.Services;

/// <summary>
/// Provides high-performance Excel export capabilities using ClosedXML.
/// Includes support for dynamic column selection, localization of headers, and Right-to-Left (RTL) cultures.
/// </summary>
/// <typeparam name="TResource">The resource type used for localizing Excel column headers.</typeparam>
public class ExcelExportService<TResource>(IStringLocalizer<TResource> localizer) : IExcelExportService
{
    private readonly IStringLocalizer<TResource> _localizer = localizer;

    /// <summary>
    /// Thread-safe cache for storing compiled expression tree delegates. 
    /// This provides $O(1)$ lookup time for property getters, significantly improving performance over standard reflection.
    /// </summary>
    private static readonly ConcurrentDictionary<PropertyInfo, Delegate> _getterCache = new();

    /// <summary>
    /// Exports a collection of data into an Excel byte array representation.
    /// </summary>
    /// <typeparam name="T">The type of the data model being exported.</typeparam>
    /// <param name="data">The collection of data records to export.</param>
    /// <param name="selectedColumns">An optional collection of property names to include. If null or empty, all properties are included.</param>
    /// <param name="excludedColumns">An optional collection of property names to exclude. Defaults to excluding the "Id" property.</param>
    /// <returns>An <see cref="ExcelResponseModel"/> containing the generated Excel file content as a byte array.</returns>
    public virtual ExcelResponseModel ExportToExcel<T>(
        IEnumerable<T> data,
        IEnumerable<string>? selectedColumns = null,
        IEnumerable<string>? excludedColumns = null)
    {
        // Initialize exclusion list with case-insensitive comparison. Defaults to ignoring "Id".
        var excluded = new HashSet<string>(excludedColumns ?? ["Id"], StringComparer.OrdinalIgnoreCase);

        // Initialize selection list if provided.
        var selected = selectedColumns != null
            ? new HashSet<string>(selectedColumns, StringComparer.OrdinalIgnoreCase)
            : null;

        // Retrieve all public instance properties of the target type.
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        // Filter properties based on the selected (whitelist) and excluded (blacklist) criteria.
        var propertiesToExport = properties
            .Where(p => (selected == null || selected.Count == 0 || selected.Contains(p.Name)) &&
                        !excluded.Contains(p.Name))
            .ToList();

        var columnCount = propertiesToExport.Count;
        var headers = new string[columnCount];
        var getters = new Func<T, object>[columnCount];

        // Prepare headers and fast property getters for the export process.
        for (int i = 0; i < columnCount; i++)
        {
            var prop = propertiesToExport[i];
            var localizedString = _localizer[prop.Name];

            // Resolve column header name: 
            // 1. Try IStringLocalizer.
            // 2. Fallback to [Display(Name = "...")] attribute.
            // 3. Fallback to the raw property name.
            headers[i] = !localizedString.ResourceNotFound
                ? localizedString.Value
                : (prop.GetCustomAttribute<DisplayAttribute>()?.Name ?? prop.Name);

            // Generate or retrieve the pre-compiled getter delegate for this property.
            getters[i] = CreateFastGetter<T>(prop);
        }

        // Determine layout direction based on the current UI culture.
        bool isRightToLeft = CultureInfo.CurrentUICulture.TextInfo.IsRightToLeft;

        using var workbook = new XLWorkbook { RightToLeft = isRightToLeft };
        var worksheet = workbook.Worksheets.Add("Data");

        // Write the header row (1-based index in ClosedXML).
        for (int i = 0; i < columnCount; i++)
        {
            worksheet.Cell(1, i + 1).Value = headers[i];
        }

        // Transform the strongly-typed data collection into a 2D object array structure.
        // This allows bulk insertion into ClosedXML, which is drastically faster than cell-by-cell iteration.
        var rowData = new List<object[]>();
        foreach (var item in data)
        {
            var row = new object[columnCount];
            for (int i = 0; i < columnCount; i++)
            {
                // Invoke the fast compiled getter, substituting null values with empty strings.
                row[i] = getters[i](item) ?? string.Empty;
            }
            rowData.Add(row);
        }

        // Bulk insert the data starting from row 2, column 1.
        if (rowData.Count > 0)
        {
            worksheet.Cell(2, 1).InsertData(rowData);
        }

        // Define the data range and format it as a native Excel Table.
        var dataRange = worksheet.Range(1, 1, rowData.Count + 1, columnCount);
        var excelTable = dataRange.CreateTable("ExportedData");

        // Can declare theme
        // excelTable.Theme = XLTableTheme.TableStyleLight1;

        // Auto-fit column widths based on the contents.
        worksheet.Columns().AdjustToContents();

        // Apply specific RTL styling configurations if required by the current culture.
        if (isRightToLeft)
        {
            worksheet.Style.Font.SetFontName("B Mitra");
            worksheet.Style.Font.SetFontCharSet(XLFontCharSet.Arabic);
        }

        // Save the workbook to a memory stream and return as a byte array.
        using var stream = new MemoryStream();
        workbook.SaveAs(stream);

        return new ExcelResponseModel
        {
            FileContents = stream.ToArray()
        };
    }

    /// <summary>
    /// Creates a dynamically compiled expression tree to access a property value.
    /// This drastically reduces the performance overhead commonly associated with <see cref="PropertyInfo.GetValue(object)"/>.
    /// </summary>
    /// <typeparam name="TModel">The type of the object containing the property.</typeparam>
    /// <param name="propertyInfo">The reflection information of the property to access.</param>
    /// <returns>A compiled function delegate that takes an instance of <typeparamref name="TModel"/> and returns the property value as an object.</returns>
    private static Func<TModel, object> CreateFastGetter<TModel>(PropertyInfo propertyInfo)
    {
        // Check the cache first to avoid recompiling the expression tree for the same property.
        var compiledDelegate = _getterCache.GetOrAdd(propertyInfo, prop =>
        {
            // Create an expression parameter for the target instance: e.g., (TModel instance)
            var instanceParam = Expression.Parameter(typeof(TModel), "instance");

            // Create property access expression: e.g., instance.PropertyName
            var propertyAccess = Expression.Property(instanceParam, prop);

            // Cast the property value to object: e.g., (object)instance.PropertyName
            var castToObject = Expression.Convert(propertyAccess, typeof(object));

            // Compile into a Func<TModel, object> delegate: e.g., instance => (object)instance.PropertyName
            return Expression.Lambda<Func<TModel, object>>(castToObject, instanceParam).Compile();
        });

        return (Func<TModel, object>)compiledDelegate;
    }
}
