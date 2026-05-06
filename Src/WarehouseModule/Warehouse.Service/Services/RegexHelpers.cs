using System.Text.RegularExpressions;

namespace NGErp.Warehouse.Service.Services;

public static partial class RegexHelpers
{
    [GeneratedRegex(@"^@u[1-9]\d*$")]
    public static partial Regex UnitOfMeasurementRefRegex();

    [GeneratedRegex(@"^unit(\d)$")]
    public static partial Regex UnitOfMeasurementConversionRegex();
}
