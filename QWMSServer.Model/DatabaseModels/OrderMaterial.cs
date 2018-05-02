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

        [Index(IsUnique = true)]
        [StringLength(50)]
        [Column("code")]
        public string code { get; set; }

        [Column("orderID")]
        public int? orderID { get; set; }

        [Column("materialID")]
        public int? materialID { get; set; }

        [StringLength(50)]
        [Column("quantity")]
        public string quantity { get; set; }

        [Column("grossWeight")]
        public float grossWeight { get; set; }

        [StringLength(50)]
        [Column("theoryQuantity")]
        public string theoryQuantity { get; set; }

        [Column("theoryGrossWeight")]
        public float theoryGrossWeight { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [ForeignKey("orderID")]
        public Order order { get; set; }

        [ForeignKey("materialID")]
        public Material material { get; set; }
    }
}
