namespace NGErp.Base.Domain.ResponseModels;

public record SuccessGetEntityListModel<T> : ResponseModel
{
    public override bool Success { get; set; } = true;
    public IEnumerable<T> Data { get; set; } = [];

    // coumputed property
    public int Count => Data.Count();
}
