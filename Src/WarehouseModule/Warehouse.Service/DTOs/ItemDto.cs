namespace NGErp.Warehouse.Service.DTOs;

public record ItemDto(string Code, string Title, string Sku, bool IsActive);
public record CreateItemDto();
public record UpdateItemDto();
