using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_employee_employeegroup")]
	public class Employee_EmployeeGroup
	{
		public Employee_EmployeeGroup ()
		{

		}

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        //[Key]
        [Required]
        [Index("idx", 1, IsUnique = true)]
        [Column("employeeID")]
        public int employeeID { get; set; }

        //[Key]
        [Required]
        [Index("idx", 2, IsUnique = true)]
        [Column("employeeGroupID")]
        public int employeeGroupID { get; set; }

        [ForeignKey("employeeID")]
        public Employee employee { get; set; }

        [ForeignKey("employeeGroupID")]
        public EmployeeGroup employeeGroup { get; set; }

    }
}
