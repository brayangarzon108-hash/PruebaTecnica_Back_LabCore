using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreSample.Domain.Model.Dto
{
    public class PatientsResponse
    {
        public PatientsResponse(Patients data)
        {
            this.Id = data.Id;
            this.TypeDocument = data.TypeDocument;
            this.DescDocument = data.Id == 1 ? "CC" : data.Id == 2 ? "CE" : data.Id == 3 ? "TI" : "PP";
            this.Document = data.Document;
            this.Name = data.Name;
            this.LastName = data.LastName;
            this.BirthDate = data.BirthDate.ToString("yyyy-MM-dd");
            this.CityId = data.CityId;
            this.Phone = data.Phone;
            this.Email = data.Email;
            this.CreatedBy = data.CreatedBy;
            this.UpdatedBy = data.UpdatedBy;
            this.CreatedDate = data.CreatedDate.ToString("yyyy-MM-dd");
            this.UpdatedDate = data.UpdatedDate.Value.ToString("yyyy-MM-dd");
            this.Enabled = data.Enabled;
        }

        public PatientsResponse()
        {
        }

        public int Id { get; set; }
        public int TypeDocument { get; set; }
        public string DescDocument { get; set; }
        public string Document { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public int CityId { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }
        public string? CreatedBy { get; set; }
        public string CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public string? UpdatedDate { get; set; }
        public bool? Enabled { get; set; }
    }
}
