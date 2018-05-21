using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
    [Table("t_company")]
    public class Company
    {
        public Company()
        {
            
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Index(IsUnique = true)]
        [StringLength(255)]
        [Column("code")]
        public string code { get; set; }

        [StringLength(255)]
        [Column("nameVi")]
        public string nameVi { get; set; }

        [StringLength(255)]
        [Column("nameEn")]
        public string nameEn { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }
    }
}
