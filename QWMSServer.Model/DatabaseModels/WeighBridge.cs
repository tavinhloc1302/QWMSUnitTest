using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_weight_bridge")]
	public class WeighBridge
	{
		public WeighBridge ()
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

        [Column("Description")]
        public string Description { get; set; }

        [Column("Location")]
        public string Location { get; set; }

        [Column("PCIpAddress")]
        public string PCIpAddress { get; set; }

        [Column("WBConfigCode")]
        public string WBConfigCode { get; set; }

        [Column("UserPCID")]
        public int? UserPCID { get; set; }

        [ForeignKey("WBConfigCode")]
        public WeighbridgeConfiguration WBConfiguration { get; set; }

        [Column("isDelete")]
        public bool? isDelete { get; set; }

        [ForeignKey("UserPCID")]
        public UserPC userPC { get; set; }

        [NotMapped]
        public int status { get; set; }

        [NotMapped]
        public float weight { get; set; }

        [NotMapped]
        public DateTime updateTime { get; set; }
    }
}
