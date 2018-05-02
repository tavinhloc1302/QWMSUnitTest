using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.DatabaseModels
{
    [Table("t_products")]
    public class Products
    {
        public Products()
        {

        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        [StringLength(50)]
        [Column("ProductTypeCode")]
        public string ProductTypeCode { get; set; }

        [ForeignKey("ProductTypeCode")]
        public ProductTypes ProductType { get; set; }

        [StringLength(255)]
        [Column("ProductDescription")]
        public string ProductDescription { get; set; }

        [StringLength(50)]
        [Column("ProductName")]
        public string ProductName { get; set; }

        [Key]
        [Required]
        [Index(IsUnique = true)]
        [StringLength(50)]
        [Column("ProductCode")]
        public string ProductCode { get; set; }
    }
}
