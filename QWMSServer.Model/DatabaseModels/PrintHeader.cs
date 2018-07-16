using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.DatabaseModels
{
    [Table("t_print_header")]
    public class PrintHeader
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column("companyName1")]
        public string companyName { get; set; }

        [Column("companyName2")]
        public string companyName2 { get; set; }

        [Column("phoneNo")]
        public string phoneNo { get; set; }

        [Column("faxNo")]
        public string faxNo { get; set; }

        [Column("address")]
        public string address { get; set; }

        [Column("imagePath")]
        public string path { get; set; }
    }
}
