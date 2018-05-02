using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_employeegroup_systemfunction")]
	public class EmployeeGroup_SystemFunction
	{
		public EmployeeGroup_SystemFunction ()
		{

		}

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        //[Key]
        [Required]
        [Index("idx", 1, IsUnique = true)]
        [Column("systemFunctionID")]
        public int systemFunctionID { get; set; }

        //[Key]
        [Required]
        [Index("idx", 2, IsUnique = true)]
        [Column("employeeGroupID")]
        public int employeeGroupID { get; set; }

        [ForeignKey("systemFunctionID")]
        public SystemFunction systemFunction { get; set; }

        [ForeignKey("employeeGroupID")]
        public EmployeeGroup employeeGroup { get; set; }

    }
}
