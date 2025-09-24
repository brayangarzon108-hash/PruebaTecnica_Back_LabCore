namespace StoreSample.Domain.Model.Dto
{
    public class ProviderCustomFieldResponse
    {
        public ProviderCustomFieldResponse(ProviderCustomField data)
        {
            this.CustomFieldId = data.CustomFieldId;
            this.ProviderId = data.ProviderId;
            this.FieldName = data.FieldName;
            this.FieldValue = data.FieldValue;
            this.CreatedBy = data.CreatedBy;
            this.UpdatedBy = data.UpdatedBy;
            this.CreatedDate = data.CreatedDate.ToString("yyyy-MM-dd");
            this.UpdatedDate = data.UpdatedDate.Value.ToString("yyyy-MM-dd");
            this.Enabled = data.Enabled;
        }

        public ProviderCustomFieldResponse()
        {
        }
        public int CustomFieldId { get; set; }
        public int ProviderId { get; set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string? CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedDate { get; set; }
        public bool? Enabled { get; set; }
    }
}
