using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_queue_list")]
	public class QueueList
	{
		public QueueList ()
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

        [Column("gatePassID")]
        public int? gatePassID { get; set; }

        [Column("queueTime")]
        public DateTime queueTime { get; set; }

        [Column("queueOrder")]
        public int? queueOrder { get; set; }

        [Column("estimateTime")]
        public int? estimateTime { get; set; }

        [Column("truckID")]
        public int? truckID { get; set; }

        //[Column("truckGroupID")]
        //public int? truckGroupID { get; set; }

        [Column("laneID")]
        public int? laneID { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [ForeignKey("truckID")]
        public Truck truck { get; set; }

        [ForeignKey("laneID")]
        public Lane lane { get; set; }

        [ForeignKey("gatePassID")]
        public GatePass gatePass { get; set; }

        //[ForeignKey("truckGroupID")]
        //public TruckGroup truckGroup { get; set; }
    }
}
