using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_truck_type")]
	public class TruckType
	{
		public TruckType ()
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
        
        [StringLength(255)]
        [Column("desciption")]
        public string desciption { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

    }
}
