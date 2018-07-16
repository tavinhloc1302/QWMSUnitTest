using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.DatabaseModels
{
    [Table("t_user_password")]
    public class UserPassword
    {
        public UserPassword()
        {

        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int userID { get; set; }

        [StringLength(255)]
        [Column("passwordString")]
        public string passwordString { get; set; }

        [Column("createDate")]
        public DateTime createDate { get; set; }

        [Column("isUsing")]
        public bool? isUsing { get; set; }

        [ForeignKey("userID")]
        public User user { get; set; }
    }
}
