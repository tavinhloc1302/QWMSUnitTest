using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_wareshouse")]
	public class Wareshouse
	{
		public Wareshouse ()
		{
            loadingBays = new HashSet<LoadingBay>();
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
        [Column("nameVi")]
        public string nameVi { get; set; }

        [StringLength(50)]
        [Column("nameEn")]
        public string nameEn { get; set; }

        [Column("plantID")]
        public int? plantID { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }


        [ForeignKey("plantID")]
        public Plant plant { get; set; }

        public virtual ICollection<LoadingBay> loadingBays { get; set; } 
    }
}
