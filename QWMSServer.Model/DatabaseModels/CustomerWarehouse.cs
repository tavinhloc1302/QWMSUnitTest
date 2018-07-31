using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
    [Table("t_customer_warehouse")]
    public class CustomerWarehouse
    {
        public CustomerWarehouse()
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
        [Column("deliveryCode")]
        public string deliveryCode { get; set; }

        [StringLength(255)]
        [Column("warehouseName")]
        public string warehouseName { get; set; }

        [StringLength(255)]
        [Column("deliveryAddressVi")]
        public string deliveryAddressVi { get; set; }

        [StringLength(255)]
        [Column("deliveryAddressEn")]
        public string deliveryAddressEn { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [Column("customerID")]
        public int? customerID { get; set; }

        [ForeignKey("customerID")]
        public Customer customer { get; set; }
    }
}
