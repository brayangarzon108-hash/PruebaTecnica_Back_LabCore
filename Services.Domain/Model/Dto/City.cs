using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StoreSample.Domain.Model.Dto
{
    [Table("City", Schema = "dbo")]
    public class City
    {
        public City()
        {
        }

        [Key]
        [Column("CityId")]
        public int CityId { get; set; }

        [Column("isocode")]
        [Required]
        [StringLength(5)]
        public string IsoCode { get; set; }

        [Column("name")]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

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

        [Column("Enabled")]
        public bool? Enabled { get; set; }

    }
}
