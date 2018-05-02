using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
    [Table("t_product_types")]
    public class ProductTypes
    {
        public ProductTypes()
        {
            //Products = new HashSet<Products>();
        }

        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductTypeID { get; set; }

        [StringLength(255)]
        [Column("ProductTypeDescription")]
        public string ProductTypeDescription { get; set; }

        [Key]
        [Required]
        [Index(IsUnique = true)]
        [StringLength(50)]
        [Column("ProductTypeCode")]
        public string ProductTypeCode { get; set; }

        //[InverseProperty("ProductTypes")]
        //public virtual ICollection<Products> Products { get; set; }
    }
}
