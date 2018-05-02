using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_driver")]
	public class Driver
	{
		public Driver ()
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

        [Required]
        [StringLength(50)]
        [Column("nameVi")]
        public string nameVi { get; set; }

        [StringLength(50)]
        [Column("nameViNoSign")]
        public string nameViNoSign { get; set; }

        [StringLength(50)]
        [Column("nameEn")]
        public string nameEn { get; set; }

        [Required]
        [StringLength(50)]
        [Column("IDNo")]
        public string IDNo { get; set; }

        [Required]
        [StringLength(50)]
        [Column("driverLicenseNo")]
        public string driverLicenseNo{ get; set; }

        [StringLength(50)]
        [Column("phoneNo")]
        public string phoneNo { get; set; }

        [Column("carrierVendorID")]
        public int carrierVendorID { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [StringLength(50)]
        [Column("remark")]
        public string remark { get; set; }

        [ForeignKey("carrierVendorID")]
        public CarrierVendor carrierVendor { get; set; }
    }
}
