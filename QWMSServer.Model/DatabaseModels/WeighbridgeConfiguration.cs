using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMSServer.Model.DatabaseModels
{
    [Table("t_weight_bridge_configuration")]
    public class WeighbridgeConfiguration
    {
        public WeighbridgeConfiguration()
        {

        }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Key]
        [Index(IsUnique = true)]
        [StringLength(50)]
        [Column("Code")]
        public string Code { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("ComPort")]
        public string ComPort { get; set; }

        [Column("BaundRate")]
        public int? BaundRate { get; set; }

        [Column("DataBits")]
        public int? DataBits { get; set; }

        [Column("Parity")]
        public string Parity { get; set; }

        [Column("StopBits")]
        public string StopBits { get; set; }

        [Column("OutputMode")]
        public string OutputMode { get; set; }

        [Column("CheckSum")]
        public bool? CheckSum { get; set; }

        [Column("StartChar")]
        public byte? startChar { get; set; }

        [Column("NumPrefixChar")]
        public int? NumPrefixChar { get; set; }

        [Column("NumWeightChar")]
        public int? NumWeightChar { get; set; }

        [Column("DataLength")]
        public int? DataLength { get; set; }

        [Column("MinusChar")]
        public byte? MinusChar { get; set; }

        [Column("StableTime")]
        public int? StableTime { get; set; }

        [Column("OscillatingWeight")]
        public float? OscillatingWeight { get; set; }

        [Column("MinWeight")]
        public float? MinWeight { get; set; }

        [Column("MaxWeight")]
        public float? MaxWeight { get; set; }

        [Column("DefaultConfig")]
        public bool? DefaultConfig { get; set; }

        [Column("isDelete")]
        public bool? isDelete { get; set; }
    }
}
