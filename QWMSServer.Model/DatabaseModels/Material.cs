using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_material")]
	public class Material
	{
		public Material ()
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
        [Column("materialNameVi")]
        public string materialNameVi { get; set; }

        [StringLength(255)]
        [Column("materialNameEn")]
        public string materialNameEn { get; set; }

        [Column("unitID")]
        public int? unitID { get; set; }

        [Column("netWeight")]
        public double netWeight { get; set; }

        [Column("grossWeight")]
        public float grossWeight { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [ForeignKey("unitID")]
        public UnitType unit { get; set; }
    }
}
