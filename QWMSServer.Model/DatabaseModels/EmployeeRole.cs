using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_employee_role")]
	public class EmployeeRole
	{
		public EmployeeRole ()
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

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [StringLength(255)]
        [Column("description")]
        public string description { get; set; }

    }
}
