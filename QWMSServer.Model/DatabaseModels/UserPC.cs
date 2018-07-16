using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.DatabaseModels
{
    [Table("t_userPC")]
    //[DataContract(IsReference = true)]
    public class UserPC
    {
        public UserPC()
        {

        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("Code")]
        [StringLength(50)]
        public string Code { get; set; }

        [Column("IPAddress")]
        [StringLength(50)]
        public string IPAddress { get; set; }

        [Column("Function")]
        [StringLength(50)]
        public string Function { get; set; }

        [Column("Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Column("CameraActive")]
        public bool CameraActive { get; set; }

        [Column("BageReaderActive")]
        public bool BadgeReaderActive { get; set; }

        public ICollection<WeighBridge> weighBridges { get; set; }
        public ICollection<BadgeReader> badgeReaders { get; set; }
        public ICollection<Camera> cameras { get; set; }

    }
}
