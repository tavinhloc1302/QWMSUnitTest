using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_order_item")]
	public class OrderItem
	{
		public OrderItem ()
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

        [Column("isDelete")]
        public bool isDelete { get; set; }

        //[StringLength(50)]
        //[Column("batchNo")]
        //public string batchNo { get; set; }

        //[StringLength(50)]
        //[Column("grossWeight")]
        //public float grossWeight { get; set; }

        //[StringLength(50)]
        //[Column("orderCode")]
        //public string orderCode { get; set; }

        //[ForeignKey("orderCode")]
        //public Order order { get; set; }

        //public ICollection<OrderMaterial> orderMaterials { get; set; }

    }
}
