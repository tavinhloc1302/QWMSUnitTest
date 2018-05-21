using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_truck")]
	public class Truck
	{
		public Truck ()
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
        [Column("plateNumber")]
        public string plateNumber { get; set; }

        [Required]
        [Column("weightValueRegistWithCalofig")]
        public float weightValueRegistWithCalofig { get; set; }

        [Column("carrierVendorID")]
        public int carrierVendorID { get; set; }

        [Column("truckLenght")]
        public float truckLenght { get; set; }

        [Column("truckHeight")]
        public float truckHeight { get; set; }

        [Column("truckWidth")]
        public float truckWidth { get; set; }

        [Column("containerLenght")]
        public float containerLenght { get; set; }

        [Column("containerWidth")]
        public float containerWidth { get; set; }

        [Column("containerHeight")]
        public float containerHeight { get; set; }

        [Required]
        [Column("truckNetWeight")]
        public float truckNetWeight { get; set; }

        [Column("weightValueRegistWithTransportDepartment")]
        public float weightValueRegistWithTransportDepartment { get; set; }

        [Required]
        [Column("totalWeight")]
        public float totalWeight { get; set; }

        [Column("expireYear")]
        public int? expireYear { get; set; }

        [Required]
        [Column("truckTypeID")]
        public int truckTypeID { get; set; }

        [Required]
        [Column("loadingTypeID")]
        public int loadingTypeID { get; set; }

        [Column("KPI")]
        public int KPI { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }


        [ForeignKey("truckTypeID")]
        public TruckType truckType { get; set; }

        [ForeignKey("loadingTypeID")]
        public LoadingType loadingType { get; set; }

        [ForeignKey("carrierVendorID")]
        public CarrierVendor carrierVendor { get; set; }

        [Column("suggestDriverID")]
        public int? suggestDriverID { get; set; }

        [ForeignKey("suggestDriverID")]
        public Driver driver { get; set; }
    }
}
