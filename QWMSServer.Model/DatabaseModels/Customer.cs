using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QWMSServer.Model.DatabaseModels
{
	[Table("t_customer")]
	public class Customer
	{
		public Customer ()
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
        [Column("nameVi")]
        public string nameVi { get; set; }

        [StringLength(255)]
        [Column("nameEn")]
        public string nameEn { get; set; }

        [StringLength(50)]
        [Column("shortName")]
        public string shortName { get; set; }

        [StringLength(255)]
        [Column("invoiceAddressVi")]
        public string invoiceAddressVi { get; set; }

        [StringLength(255)]
        [Column("invoiceAddressEn")]
        public string invoiceAddressEn { get; set; }

        [StringLength(50)]
        [Column("taxCode")]
        public string taxCode { get; set; }

        [StringLength(50)]
        [Column("contactPerson")]
        public string contactPerson { get; set; }

        [StringLength(50)]
        [Column("telNo")]
        public string telNo { get; set; }

        [StringLength(50)]
        [Column("faxNo")]
        public string faxNo { get; set; }

        [StringLength(50)]
        [Column("email")]
        public string email { get; set; }

        [Column("isDelete")]
        public bool isDelete { get; set; }

        [InverseProperty("Customer")]
        public virtual ICollection<CustomerWarehouse> customerWarehouses { get; set; }

    }
}
