using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_sensor")]
	public class Sensor
	{
		public Sensor ()
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

        [Column("valueControllerID")]
        public int valueControllerID { get; set; }

        [Column("valueControllerPort")]
        public int valueControllerPort { get; set; }

        [Column("statusControllerID")]
        public int statusControllerID { get; set; }

        [Column("statusControllerPort")]
        public int statusControllerPort { get; set; }

        [Column("IsActive")]
        public bool isActive { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [ForeignKey("valueControllerID")]
        public Controller valueController { get; set; }

        [ForeignKey("statusControllerID")]
        public Controller statusController { get; set; }

        [NotMapped]
        public int status { get; set; }

        [NotMapped]
        public bool portValue { get; set; }

        [NotMapped]
        public DateTime stableDuration { get; set; }
    }
}
