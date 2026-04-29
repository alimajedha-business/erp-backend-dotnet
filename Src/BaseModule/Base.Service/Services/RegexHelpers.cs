using System.Text.RegularExpressions;

namespace NGErp.Base.Service.Services;

public static partial class RegexHelpers
{
    [GeneratedRegex(@"@(\d+)")]
    public static partial Regex DynamicLinqArgRegex();

    [GeneratedRegex(@"constraint\s+""(?<name>[^""]+)""")]
    public static partial Regex SqlConstraintRegex();

    [GeneratedRegex(@"^@u[1-9]\d*$")]
    public static partial Regex UnitOfMeasurementRefRegex();
}
