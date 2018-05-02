using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_employee")]
	public class Employee
	{
		public Employee ()
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

        [StringLength(50)]
        [Column("firstName")]
        public string firstName { get; set; }

        [StringLength(50)]
        [Column("lastName")]
        public string lastName { get; set; }

        [Column("RFIDCardID")]
        public int? RFIDCardID { get; set; }

        [Column("userID")]
        public int? userID { get; set; }

        [ForeignKey("RFIDCardID")]
        public RFIDCard rfidCard { get; set; }

        [ForeignKey("userID")]
        public User user { get; set; }

        public ICollection<Employee_EmployeeGroup> groupMaps { get; set; }
    }
}
