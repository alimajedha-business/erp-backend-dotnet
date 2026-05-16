namespace NGErp.Base.Service.Authorization;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class InherentlyActionAttribute(ActionType actionType) : Attribute
{
    public ActionType ActionType { get; } = actionType;
}
