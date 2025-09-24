using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoreSample.Domain.Model.Dto
{
    public class CountryResponse
    {
        public CountryResponse(City data)
        {
            if (data != null)
            {
                CountryId = data.CountryId;
                IsoCode = data.IsoCode;
                Name = data.Name;
                UserId = data.UpdatedBy;
                Enabled = data.Enabled;
            }
        }

        public CountryResponse()
        {
        }

        public int CountryId { get; set; }
        public string IsoCode { get; set; }
        public string Name { get; set; }
        public string? UserId { get; set; }
        public bool? Enabled { get; set; }
    }
}
