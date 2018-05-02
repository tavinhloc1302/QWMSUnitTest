using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_carrier_vendor")]
	public class CarrierVendor
	{
		public CarrierVendor ()
		{

		}

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Index(IsUnique = true)]
        [StringLength(50)]
        [Column("code")]
        public string code { get; set; }

        [StringLength(255)]
        [Column("nameVi")]
        public string nameVi { get; set; }

        [StringLength(255)]
        [Column("nameEn")]
        public string nameEn { get; set; }

        [StringLength(50)]
        [Column("shortName")]
        public string shortName { get; set; }

        [StringLength(255)]
        [Column("addressVi")]
        public string addressVi { get; set; }

        [StringLength(255)]
        [Column("addressEn")]
        public string addressEn { get; set; }

        [StringLength(50)]
        [Column("taxCode")]
        public string taxCode { get; set; }

        [StringLength(50)]
        [Column("contactPerson")]
        public string contactPerson { get; set; }

        [StringLength(50)]
        [Column("department")]
        public string department { get; set; }

        [StringLength(50)]
        [Column("telNo")]
        public string telNo { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }
    }
}
