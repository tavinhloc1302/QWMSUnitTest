using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_system_function")]
	public class SystemFunction
	{
		public SystemFunction ()
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

        [StringLength(50)]
        [Column("functionName")]
        public string functionName { get; set; }

        [StringLength(255)]
        [Column("functionDescription")]
        public string functionDescription { get; set; }

        [StringLength(50)]
        [Column("API")]
        public string API { get; set; }

        public ICollection<EmployeeGroup_SystemFunction> employeeGroupMaps { get; set; }
    }
}
