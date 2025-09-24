namespace StoreSample.Domain.Model.Dto
{
    public class ServiceResponse
    {
        public ServiceResponse(Patients data)
        {
            this.ServiceId = data.ServiceId;
            this.ProviderId = data.ProviderId;
            this.Name = data.Name;
            this.HourlyRate = data.HourlyRate;
            this.CreatedBy = data.CreatedBy;
            this.UpdatedBy = data.UpdatedBy;
            this.CreatedDate = data.CreatedDate.ToString("yyyy-MM-dd");
            this.UpdatedDate = data.UpdatedDate.Value.ToString("yyyy-MM-dd");
            this.Enabled = data.Enabled;
        }

        public ServiceResponse()
        {
        }

        public int ServiceId { get; set; }
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public List<string> Countries { get; set; }
        public decimal HourlyRate { get; set; }
        public string? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedDate { get; set; }
        public bool? Enabled { get; set; }
    }
}
