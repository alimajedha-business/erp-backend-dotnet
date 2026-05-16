namespace NGErp.Base.Service.Authorization;

public enum ActionType
{
    Default = 0, // Use HTTP Method mapping
    Read,
    Create,
    Edit,
    Delete,
    Log,
    Print,
    Import,
    Export
}
