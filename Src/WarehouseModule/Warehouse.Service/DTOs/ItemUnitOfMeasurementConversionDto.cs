namespace NGErp.Warehouse.Service.DTOs;

public sealed class ItemUnitConversionEquationDto
{
    public required object? Operand1 { get; set; }
    public required object? Operand2 { get; set; }
    public object? Operand3 { get; set; }
    public object? Operand4 { get; set; }

    public required string Op1 { get; set; }
    public string? Op2 { get; set; }
    public string? Op3 { get; set; }
}