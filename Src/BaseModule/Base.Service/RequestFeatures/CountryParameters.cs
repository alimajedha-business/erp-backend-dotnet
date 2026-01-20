namespace NGErp.Base.Service.RequestFeatures
{
    public class CountryParameters : RequestParameters
    {
        public string? SearchTerm { get; set; }
        public CountryParameters() => OrderBy = "name";
    }
}
