namespace NGErp.Base.Service.ResponseModels;

public class ExcelResponseModel
{
    public byte[] FileContents { get; set; } = default!;
    public string ContentType { get; set; } = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
}
