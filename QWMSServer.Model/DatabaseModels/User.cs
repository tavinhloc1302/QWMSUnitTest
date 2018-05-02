using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [StringLength(50)]
        [Column("username")]
        public string username { get; set; }

        [StringLength(255)]
        [Column("password")]
        public string password { get; set; }

        public ICollection<Employee> employees { get; set; }

    }
}
