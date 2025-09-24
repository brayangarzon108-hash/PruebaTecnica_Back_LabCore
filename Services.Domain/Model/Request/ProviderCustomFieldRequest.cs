namespace StoreSample.Domain.Model.Dto
{
    public class ProviderCustomFieldRequest
    {
        public ProviderCustomFieldRequest()
        {
        }

        public int CustomFieldId { get; set; }
        public int? ProviderId { get; set; } = 0;
        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public string? UserId { get; set; }
        public bool? Enabled { get; set; }
    }
}
