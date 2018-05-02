using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_weight_record")]
	public class WeightRecord
	{
		public WeightRecord ()
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

        [Column("weightTime")]
        public DateTime weightTime { get; set; }

        [Column("weightValue")]
        public float weightValue { get; set; }

        [Column("weightEmployeeID")]
        public int weightEmployeeID { get; set; }

        [Column("weighBridgeID")]
        public int weighBridgeID { get; set; }

        [StringLength(50)]
        [Column("frontCameraCapturePath")]
        public string frontCameraCapturePath { get; set; }

        [StringLength(50)]
        [Column("gearCameraCapturePath")]
        public string gearCameraCapturePath { get; set; }

        [StringLength(50)]
        [Column("cabinCameraCapturePath")]
        public string cabinCameraCapturePath { get; set; }

        [StringLength(50)]
        [Column("containerCameraCapturePath")]
        public string containerCameraCapturePath { get; set; }

        [Column("weightNo")]
        public int weightNo { get; set; }

        [Column("gatepassID")]
        public int gatepassID { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [ForeignKey("weightEmployeeID")]
        public Employee employee { get; set; }

        [ForeignKey("weighBridgeID")]
        public WeighBridge weighBridge { get; set; }

        [ForeignKey("gatepassID")]
        public GatePass gatePass { get; set; }
    }
}
