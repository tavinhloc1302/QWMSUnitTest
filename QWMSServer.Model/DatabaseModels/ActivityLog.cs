using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
    [Table("t_activity_log")]
    public class ActivityLog
    {
        public ActivityLog()
        {

        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("employeeID")]
        public int? employeeID { get; set; }

        [Column("driverID")]
        public int? driverID { get; set; }

        [Column("logTime")]
        public DateTime logTime { get; set; }

        [Column("screen")]
        public string screen { get; set; }

        [Column("action")]
        public string action { get; set; }

        [Column("target")]
        public string target { get; set; }

        [Column("targetValue")]
        public string targetValue { get; set; }

        [Column("StargetValue")]
        public string StargetValue { get; set; }

        [Column("result")]
        public string result { get; set; }

        [Column("comment")]
        public string comment { get; set; }

        [Column("currentStateID")]
        public int? currentStateID { get; set; }

        [Column("queueID")]
        public int? queueID { get; set; }

        [Column("RFIDcardID")]
        public int? RFIDcardID { get; set; }

        [Column("gatePassID")]
        public int? gatePassID { get; set; }


        [ForeignKey("employeeID")]
        public Employee employee { get; set; }

        [ForeignKey("driverID")]
        public Driver driver { get; set; }

        [ForeignKey("currentStateID")]
        public State state { get; set; }

        [ForeignKey("RFIDcardID")]
        public RFIDCard RFIDCard { get; set; }

        [ForeignKey("gatePassID")]
        public GatePass gatePass { get; set; }
    }
}
