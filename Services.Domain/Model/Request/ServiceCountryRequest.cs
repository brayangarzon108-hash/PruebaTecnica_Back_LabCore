namespace StoreSample.Domain.Model.Dto
{
    public class ServiceCountryRequest
    {
        public int ServiceCountryId { get; set; }
        public int ServiceId { get; set; }
        public int CountryId { get; set; }
        public string? UserId { get; set; }
        public bool? Enabled { get; set; } = true;
    }
}