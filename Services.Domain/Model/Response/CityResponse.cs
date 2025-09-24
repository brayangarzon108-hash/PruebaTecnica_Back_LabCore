using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoreSample.Domain.Model.Dto
{
    public class CityResponse
    {
        public CityResponse(City data)
        {
            if (data != null)
            {
                CityId = data.CityId;
                IsoCode = data.IsoCode;
                Name = data.Name;
                UserId = data.UpdatedBy;
                Enabled = data.Enabled;
            }
        }

        public CityResponse()
        {
        }

        public int CityId { get; set; }
        public string IsoCode { get; set; }
        public string Name { get; set; }
        public string? UserId { get; set; }
        public bool? Enabled { get; set; }
    }
}
