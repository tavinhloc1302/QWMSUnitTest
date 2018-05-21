using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.DatabaseModels
{
    [Table("t_token")]
    public class Token
    {
        public Token()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Required]
        [Column("token_string")]
        public string TokenString { get; set; }

        [Column("issued_on")]
        public DateTime IssuedOn { get; set; }

        [Column("expires_in")]
        public int ExpiresIn { get; set; }

        [NotMapped]
        public DateTime ExpiresOn => IssuedOn.AddSeconds(ExpiresIn);

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
