namespace NGErp.Base.API.ActionFilters;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class SkipModelValidationAttribute : Attribute { }
