using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_lane")]
	public class Lane
	{
		public Lane ()
		{

		}

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Index(IsUnique = true)]
        [StringLength(255)]
        [Column("code")]
        public string code { get; set; }

        [StringLength(255)]
        [Column("nameVi")]
        public string nameVi { get; set; }

        [StringLength(255)]
        [Column("nameEn")]
        public string nameEn { get; set; }

        [Column("loadingBayID")]
        public int? loadingBayID { get; set; }

        [Column("truckTypeID")]
        public int? truckTypeID { get; set; }

        [Column("loadingTypeID")]
        public int? loadingTypeID { get; set; }

        [Column("minCapacity")]
        public int? minCapacity { get; set; }

        [Column("maxCapactity")]
        public int? maxCapactity { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [Column("status")]
        public int status { get; set; }

        [Column("usingStatus")]
        public int usingStatus { get; set; }


        [ForeignKey("loadingBayID")]
        public LoadingBay loadingBay { get; set; }

        [ForeignKey("truckTypeID")]
        public TruckType truckType { get; set; }

        [ForeignKey("loadingTypeID")]
        public LoadingType loadingType { get; set; }
    }
}
