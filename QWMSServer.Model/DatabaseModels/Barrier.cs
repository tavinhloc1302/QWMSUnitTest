using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_barrier")]
	public class Barrier
	{
		public Barrier ()
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

        [StringLength(255)]
        [Column("description")]
        public string description { get; set; }

        [Column("openControllerID")]
        public int openControllerID { get; set; }

        [Column("openPort")]
        public int openPort { get; set; }

        [Column("closeControllerID")]
        public int closeControllerID { get; set; }

        [Column("closePort")]
        public int closePort { get; set; }

        [Column("isActive")]
        public bool isActive { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [ForeignKey("openControllerID")]
        public Controller openController { get; set; }

        [ForeignKey("closeControllerID")]
        public Controller closeController { get; set; }

        [NotMapped]
        public int status { get; set; }

        [NotMapped]
        public bool openPortValue { get; set; }

        [NotMapped]
        public bool closePortValue { get; set; }
    }
}
