namespace StoreSample.Domain.Model.Dto
{
    public class ProviderRequest
    {
        public ProviderRequest()
        {
        }

        public int ProviderId { get; set; }
        public string TaxId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? UserId { get; set; }
        public bool? Enabled { get; set; }
        public List<ProviderCustomFieldRequest>? dynamicFields { get; set; }
    }
}
