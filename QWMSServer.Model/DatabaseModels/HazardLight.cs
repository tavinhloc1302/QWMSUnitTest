using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.DatabaseModels
{
    [Table("t_hazzardLight")]
    public class HazardLight
    {
        public HazardLight()
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

        [Column("Name")]
        public string Name { get; set; }

        [StringLength(255)]
        [Column("description")]
        public string description { get; set; }

        [Column("controllerID")]
        public int controllerID { get; set; }

        [Column("port")]
        public int port { get; set; }

        [Column("isActive")]
        public bool isActive { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [ForeignKey("controllerID")]
        public Controller controller { get; set; }

        [NotMapped]
        public int status { get; set; }

        [NotMapped]
        public bool portValue { get; set; }
    }
}
