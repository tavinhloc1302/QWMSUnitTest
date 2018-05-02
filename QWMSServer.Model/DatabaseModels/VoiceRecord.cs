using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_voice_record")]
	public class VoiceRecord
	{
		public VoiceRecord ()
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

        [StringLength(255)]
        [Column("path")]
        public string path { get; set; }

        [StringLength(255)]
        [Column("description")]
        public string description { get; set; }

        [StringLength(255)]
        [Column("name")]
        public string name { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }
    }
}
