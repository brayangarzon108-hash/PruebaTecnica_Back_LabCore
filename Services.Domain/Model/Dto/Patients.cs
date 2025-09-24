using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoreSample.Domain.Model.Dto
{
    [Table("Patients", Schema = "dbo")]
    public class Patients
    {
        public Patients()
        {
        }

        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("TypeDocument")]
        [Required]
        public int TypeDocument { get; set; }
        [Column("Document")]
        [Required]
        [StringLength(50)]
        public string Document { get; set; }

        [Column("name")]
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Column("LastName")]
        [Required]
        [StringLength(150)]
        public string LastName { get; set; }

        [Column("BirthDate")]
        [Required]
        public DateTime BirthDate { get; set; }

        [Column("CityId")]
        public int CityId { get; set; }

        [Column("Phone")]
        public int Phone { get; set; }
        [Column("Email")]
        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        // Audit fields
        [Column("createdby")]
        [StringLength(100)]
        public string? CreatedBy { get; set; }

        [Column("createddate")]
        public DateTime CreatedDate { get; set; }

        [Column("updatedby")]
        [StringLength(100)]
        public string? UpdatedBy { get; set; }

        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }

        // Navigation properties
        [ForeignKey("ProviderId")]
        public virtual Item Provider { get; set; }
        [Column("Enabled")]
        public bool? Enabled { get; set; }
    }
}
