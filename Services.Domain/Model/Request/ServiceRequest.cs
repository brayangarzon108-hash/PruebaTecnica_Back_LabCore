using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoreSample.Domain.Model.Dto
{
    public class ServiceRequest
    {
        public ServiceRequest()
        {
        }
        public int ServiceId { get; set; }
        public int ProviderId { get; set; } = 0;
        public string Name { get; set; }
        public decimal HourlyRate { get; set; }
        public string? UserId { get; set; }       
        public bool? Enabled { get; set; }
        public List<int> Countries { get; set; }
    }
}
