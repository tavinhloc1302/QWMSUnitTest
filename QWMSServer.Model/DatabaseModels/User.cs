using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_user")]
    public class User
	{
		public User ()
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

        [Column("isActive")]
        public bool? isActive { get; set; }

        [Column("loginTimes")]
        public int? loginTimes { get; set; }

        [Column("isBlock")]
        public bool? isBlock { get; set; }

        [StringLength(50)]
        [Column("username")]
        public string username { get; set; }

        [StringLength(255)]
        [Column("password")]
        public string password { get; set; }

        [Column("employeeID")]
        public int? employeeID { get; set; }

        [Column("loginTime")]
        public DateTime? loginTime { get; set; }

        [ForeignKey("employeeID")]
        public Employee employee { get; set; }

        public virtual ICollection<UserPassword> userPasswords { get; set; }
    }
}
