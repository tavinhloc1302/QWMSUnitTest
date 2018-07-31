using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_order_material")]
	public class OrderMaterial
	{
		public OrderMaterial ()
		{

		}

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(50)]
        [Column("code")]
        public string code { get; set; }

        [Column("orderID")]
        public int? orderID { get; set; }

        [Column("materialID")]
        public int? materialID { get; set; }

        [Column("fMaterialName")]
        public string fMaterialName { get; set; }

        [Column("QCQuantity")]
        public int? QCQuantity { get; set; }

        [Column("QCGrossWeight")]
        public float? QCGrossWeight { get; set; }

        [Column("registQuantity")]
        public int? registQuantity { get; set; }

        [Column("registGrossWeight")]
        public float? registGrossWeight { get; set; }

        [Column("registNetWeight")]
        public float? registNetWeight { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [ForeignKey("orderID")]
        public Order order { get; set; }

        [ForeignKey("materialID")]
        public Material material { get; set; }
    }
}
