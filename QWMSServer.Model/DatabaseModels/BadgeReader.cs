﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace QWMSServer.Model.DatabaseModels
{
    [Table("t_badge_reader")]
    public class BadgeReader
    {
        public BadgeReader()
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

        [StringLength(255)]
        [Column("name")]
        public string name { get; set; }

        [StringLength(255)]
        [Column("description")]
        public string description { get; set; }

        [Column("isDelete")]
        public bool? isDelete { get; set; }

        [StringLength(255)]
        [Column("ipAddress")]
        public string ipAddress { get; set; }

        [Column("port")]
        public int? port { get; set; }

        [StringLength(255)]
        [Column("location")]
        public string pcIPAddress { get; set; }

        [Column("UserPCID")]
        public int? UserPCID { get; set; }

        [ForeignKey("UserPCID")]
        public UserPC userPC { get; set; }
    }
}
