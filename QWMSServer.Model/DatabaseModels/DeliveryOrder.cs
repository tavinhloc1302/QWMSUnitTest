using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_delivery_order")]
	public class DeliveryOrder
	{
		public DeliveryOrder ()
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

        //[StringLength(50)]
        //[Column("doNumber")]
        //public string doNumber { get; set; }

        [Column("createDate")]
        public DateTime createDate { get; set; }

        [Column("soID")]
        public int? soID { get; set; }

        [Column("customerID")]
        public int? customerID { get; set; }

        [Column("carrierVendorID")]
        public int? carrierVendorID { get; set; }

        [StringLength(50)]
        [Column("remark")]
        public string remark { get; set; }

        [StringLength(50)]
        [Column("sloc")]
        public string sloc { get; set; }

        [Column("doTypeID")]
        public int? doTypeID { get; set; }

        [Column("customerWarehouseID")]
        public int? customerWarehouseID { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [ForeignKey("customerWarehouseID")]
        public CustomerWarehouse customerWarehouse { get; set; }

        [ForeignKey("customerID")]
        public Customer customer { get; set; }

        [ForeignKey("carrierVendorID")]
        public CarrierVendor carrierVendor { get; set; }

        [ForeignKey("soID")]
        public SaleOrder saleOrder { get; set; }

        public ICollection<Order> order { get; set; }

        [ForeignKey("doTypeID")]
        public DeliveryOrderType deliveryOrderType { get; set; }
    }
}

