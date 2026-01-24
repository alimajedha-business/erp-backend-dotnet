namespace NGErp.Base.Domain.ResponseModels;

public record SuccessGetEntityModel<T> : ResponseModel
{
    public override bool Success { get; set; } = true;
    public T? Data { get; set; }

    // coumputed property
    public bool Matched => Data != null;
}
