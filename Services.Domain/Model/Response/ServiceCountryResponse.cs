namespace StoreSample.Domain.Model.Dto
{
    public class ServiceCountryResponse
    {
        public ServiceCountryResponse(ServiceCountry data)
        {
            this.ServiceId = data.ServiceId;
            this.CountryId = data.CountryId;
            this.ServiceCountryId = data.ServiceCountryId;
            this.Enabled = data.Enabled;
        }

        public ServiceCountryResponse()
        {
        }

        public int ServiceCountryId { get; set; }
        public int ServiceId { get; set; }
        public int CountryId { get; set; }
        public string? UserId { get; set; }
        public bool? Enabled { get; set; }
    }

    public class ReportSummaryDto
    {
        public List<ClientsByCountryDto> ClientsByCountry { get; set; }
        public List<ServicesByCountryDto> ServicesByCountry { get; set; }
    }

    public class ClientsByCountryDto
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int ClientsCount { get; set; }
    }

    public class ServicesByCountryDto
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int ServicesCount { get; set; }
    }
}
