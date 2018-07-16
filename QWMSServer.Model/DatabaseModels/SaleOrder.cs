using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_sale_order")]
	public class SaleOrder
	{
		public SaleOrder ()
		{

        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Index(IsUnique = true)]
        [StringLength(50)]
        [Column("Code")]
        public string Code { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

        public virtual ICollection<DeliveryOrder> deliveryOrder { get; set; }
    }
}
