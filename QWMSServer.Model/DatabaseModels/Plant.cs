using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_plant")]
	public class Plant
	{
		public Plant ()
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

        [StringLength(255)]
        [Column("nameVi")]
        public string nameVi { get; set; }

        [StringLength(50)]
        [Column("nameEn")]
        public string nameEn { get; set; }

        [Column("companyID")]
        public int? companyID { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [ForeignKey("companyID")]
        public Company company { get; set; }
    }
}
