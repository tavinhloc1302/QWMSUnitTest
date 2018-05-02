using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_loadingbay_type")]
	public class LoadingBayType
	{
		public LoadingBayType ()
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
        [Column("description")]
        public string description { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

    }
}
