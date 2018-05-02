using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_state_record")]
	public class StateRecord
	{
		public StateRecord ()
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

        [Column("stateID")]
        public int? stateID { get; set; }

        [Column("stateStatus")]
        public int? stateStatus { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }


        [ForeignKey("gatePassID")]
        public GatePass gatePass { get; set; }

        [ForeignKey("stateID")]
        public State state { get; set; }
    }
}