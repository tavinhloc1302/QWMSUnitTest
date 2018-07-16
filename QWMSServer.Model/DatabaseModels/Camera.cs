using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_camera")]
	public class Camera
	{
		public Camera ()
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

        [Column("Name")]
        public string Name { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("IP")]
        public string IP { get; set; }

        [Column("Port")]
        public string Port { get; set; }

        [Column("StreammingProtocol")]
        public string StreammingProtocol { get; set; }

        [Column("StreammingPath")]
        public string StreammingPath { get; set; }

        [Column("User")]
        public string User { get; set; }

        [Column("Pass")]
        public string Pass { get; set; }

        [Column("ModifyOn")]
        public string ModifyOn { get; set; }

        [Column("ModifyBy")]
        public string ModifyBy { get; set; }

        [Column("isDelete")]
        public bool? isDelete { get; set; }

        [Column("status")]
        public bool? status { get; set; }

        [Column("isActive")]
        public bool? isActive { get; set; }

        [Column("location")]
        public int? location { get; set; }

        [Column("usage")]
        public int? usage { get; set; }

        [Column("UserPCID")]
        public int? UserPCID { get; set; }

        [ForeignKey("UserPCID")]
        public UserPC userPC { get; set; }
    }
}
