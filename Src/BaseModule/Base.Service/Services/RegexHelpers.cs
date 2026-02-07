using System.Text.RegularExpressions;

namespace NGErp.Base.Service.Services;

public static partial class RegexHelpers
{
    [GeneratedRegex(@"@(\d+)")]
    public static partial Regex DynamicLinqArgRegex();
}
