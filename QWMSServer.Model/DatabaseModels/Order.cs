using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_order")]
	public class Order
	{
		public Order ()
		{

		}

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(50)]
        [Column("code")]
        public string code { get; set; }

        public DateTime? createDate { get; set; }

        [Column("orderTypeID")]
        public int? orderTypeID { get; set; }

        [Column("registGrossWeight")]
        public float registGrossWeight { get; set; }

        [Column("registNetWeight")]
        public float? registNetWeight { get; set; }

        [Column("QCGrossWeight")]
        public float? QCGrossWeight { get; set; }

        [Column("gatePassID")]
        public int? gatePassID { get; set; }

        [Column("plantID")]
        public int? plantID { get; set; }

        [Column("doID")]
        public int? doID { get; set; }

        [Column("poID")]
        public int? poID { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [ForeignKey("orderTypeID")]
        public OrderType orderType { get; set; }

        [ForeignKey("gatePassID")]
        public GatePass gatePass { get; set; }

        [ForeignKey("plantID")]
        public Plant plant { get; set; }

        [ForeignKey("doID")]
        public DeliveryOrder deliveryOrder { get; set; }

        [ForeignKey("poID")]
        public PurchaseOrder purchaseOrder { get; set; }

        public ICollection<OrderMaterial> orderMaterials { get; set; }

    }
}
