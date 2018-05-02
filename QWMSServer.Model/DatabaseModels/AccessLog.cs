using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
    [Table("t_access_log")]
    public class AccessLog
    {
        public AccessLog()
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

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [Column("employeeID")]
        public int? employeeID { get; set; }

        [Column("driverID")]
        public int? driverID { get; set; }

        [Column("logTime")]
        public DateTime logTime { get; set; }

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
