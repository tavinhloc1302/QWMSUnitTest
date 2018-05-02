using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_employee_group")]
	public class EmployeeGroup
	{
		public EmployeeGroup ()
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

        [StringLength(255)]
        [Column("description")]
        public string description { get; set; }

        public ICollection<Employee_EmployeeGroup> employeeMaps { get; set; }

        public ICollection<EmployeeGroup_SystemFunction> functionMaps { get; set; }
    }
}
