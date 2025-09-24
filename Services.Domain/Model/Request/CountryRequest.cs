namespace StoreSample.Domain.Model.Dto
{
    public class CountryRequest
    {
        public CountryRequest()
        {
        }

        public int CountryId { get; set; }
        public string IsoCode { get; set; }
        public string Name { get; set; }
        public string? UserId { get; set; }       
        public bool? Enabled { get; set; } = true;
    }
}
