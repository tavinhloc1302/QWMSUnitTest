using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_gate_pass")]
	public class GatePass
	{
		public GatePass ()
		{
            queueLists = new HashSet<QueueList>();

        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Index(IsUnique = true)]
        [StringLength(50)]
        [Column("code")]
        public string code { get; set; }

        [Column("driverID")]
        public int? driverID { get; set; }

        [Column("truckID")]
        public int? truckID { get; set; }

        [Column("createDate")]
        public DateTime createDate { get; set; }

        [Column("employeeID")]
        public int? employeeID { get; set; }

        [Column("truckTyeID")]
        public int? truckTyeID { get; set; }

        [Column("truckGroupID")]
        public int? truckGroupID { get; set; }

        [StringLength(50)]
        [Column("driverCamCapturePath")]
        public string driverCamCapturePath { get; set; }

        [Column("stateID")]
        public int? stateID { get; set; }

        [Column("enterTime")]
        public DateTime? enterTime { get; set; }

        [Column("leaveTime")]
        public DateTime? leaveTime { get; set; }

        [Column("RFIDCardID")]
        public int? RFIDCardID { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [Column("loadingBayID")]
        public int? loadingBayID { get; set; }

        [Column("theoryWeightValue")]
        public float? theoryWeightValue { get; set; }


        [ForeignKey("RFIDCardID")]
        public RFIDCard RFIDCard { get; set; }

        [ForeignKey("driverID")]
        public Driver driver { get; set; }

        [ForeignKey("truckID")]
        public Truck truck { get; set; }

        [ForeignKey("stateID")]
        public State state { get; set; }

        [ForeignKey("employeeID")]
        public Employee employee { get; set; }

        [ForeignKey("truckGroupID")]
        public TruckGroup truckGroup { get; set; }

        public ICollection<Order> orders { get; set; }

        public ICollection<QueueList> queueLists { get; set; }
    }
}
