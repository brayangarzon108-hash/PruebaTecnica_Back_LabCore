using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoreSample.Domain.Model.Dto
{
    public class ProviderResponse
    {
        public ProviderResponse(Item data)
        {
            ProviderId = data.ProviderId;
            TaxId = data.TaxId;
            Name = data.Name;
            Email = data.Email;
            CreatedBy = data.CreatedBy;           
            UpdatedBy = data.UpdatedBy;
            CreatedDate = data.CreatedDate.ToString("yyyy-MM-dd");
            UpdatedDate = data.UpdatedDate.Value.ToString("yyyy-MM-dd");
            Enabled = data.Enabled;

        }

        public ProviderResponse() 
        { 
        }

        public int ProviderId { get; set; }
        public string TaxId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedDate { get; set; }
        public bool? Enabled { get; set; }
        public List<ProviderCustomFieldResponse>? CustomFields { get; set; }
    }
}
