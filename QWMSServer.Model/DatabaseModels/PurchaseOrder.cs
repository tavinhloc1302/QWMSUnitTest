using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_purchase_order")]
	public class PurchaseOrder
	{
		public PurchaseOrder ()
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

        [StringLength(50)]
        [Column("poNumber")]
        public string poNumber { get; set; }

        //[Column("createDate")]
        //public DateTime createDate { get; set; }

        [Column("carrierVendorID")]
        public int? carrierVendorID { get; set; }

        [StringLength(50)]
        [Column("remark")]
        public string remark { get; set; }

        [StringLength(50)]
        [Column("sloc")]
        public string sloc { get; set; }

        [Column("poTypeID")]
        public int? poTypeID { get; set; }

        [StringLength(50)]
        [Column("invoiceNumber")]
        public string invoiceNumber { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }


        [ForeignKey("carrierVendorID")]
        public CarrierVendor carrierVendor { get; set; }

        [ForeignKey("poTypeID")]
        public PurchaseOrderType purchaseOrderType { get; set; }
    }
}
