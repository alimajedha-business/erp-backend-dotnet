using NGErp.Base.Service.ResponseModels;

namespace NGErp.Base.Service.Services;

public interface IExcelExportService
{
    ExcelResponseModel ExportToExcel<T>(
        IEnumerable<T> data,
        IEnumerable<string>? selectedColumns = null,
        IEnumerable<string>? excludedColumns = null
    );
}
