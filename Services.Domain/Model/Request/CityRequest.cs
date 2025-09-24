namespace StoreSample.Domain.Model.Dto
{
    public class CityRequest
    {
        public CityRequest()
        {
        }

        public int CityId { get; set; }
        public string IsoCode { get; set; }
        public string Name { get; set; }
        public string? UserId { get; set; }       
        public bool? Enabled { get; set; } = true;
    }
}
