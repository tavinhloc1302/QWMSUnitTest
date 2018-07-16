using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.DatabaseModels
{
    public class Constrain
    {
        public Constrain()
        {

        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("name")]
        public string name { get; set; }

        [Column("value")]
        public float value { get; set; }

        [Column("svalue")]
        public string svalue { get; set; }

        [Column("category")]
        public string category { get; set; }

        [Column("description")]
        public string description { get; set; }
    }
}
