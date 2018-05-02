using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_loadingbay")]
	public class LoadingBay
	{
		public LoadingBay ()
		{
            lanes = new HashSet<Lane>();
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

        [Column("warehouseID")]
        public int? warehouseID { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }


        [ForeignKey("warehouseID")]
        public Wareshouse wareshouse { get; set; }

        public ICollection<Lane> lanes { get; set; }

    }
}
