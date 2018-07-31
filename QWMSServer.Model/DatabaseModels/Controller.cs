using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.DatabaseModels
{
    [Table("t_controller")]
    public class Controller
    {
        public Controller()
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

        [Column("ipAddress")]
        public string ipAdrress { get; set; }

        [Column("port")]
        public int port { get; set; }

        [Column("getPath")]
        public string getPath { get; set; }

        [Column("setPath")]
        public string setPath { get; set; }

        [Column("setParam")]
        public string setParam { get; set; }

        [Column("user")]
        public string user { get; set; }

        [Column("password")]
        public string password { get; set; }

        [Column("location")]
        public string pcIPAddress { get; set; }

        [Column("isActive")]
        public bool isActive { get; set; }

        [Column("UserPCID")]
        public int? UserPCID { get; set; }

        [ForeignKey("UserPCID")]
        public UserPC userPC { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [InverseProperty("valueController")]
        public ICollection<Sensor> valueSensors { get; set; }

        [InverseProperty("statusController")]
        public ICollection<Sensor> statusSensors { get; set; }

        [InverseProperty("openController")]
        public ICollection<Barrier> openBarriers { get; set; }

        [InverseProperty("closeController")]
        public ICollection<Barrier> closeBarriers { get; set; }

        [InverseProperty("controller")]
        public ICollection<HazardLight> hazardLights { get; set; }

        [NotMapped]
        public int status { get; set; }

        [NotMapped]
        public DateTime updateTime { get; set; }
    }
}
