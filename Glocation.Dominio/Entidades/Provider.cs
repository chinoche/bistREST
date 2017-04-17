using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIST.Dominio.Entidades
{
    [Table("Providers")]
    public class Provider : BaseEntity
    {
        public Provider()
        {

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProviderId { get; set; }


        [Required]
        [StringLength(150)]
        public string Activity { get; set; }

        [Required]
        [StringLength(50)]
        public string Bank { get; set; }

        [Required]
        public int CommercialAddressId { get; set; }

        [Required]
        public int FiscalAddressId { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string Currency { get; set; }

        [Required]
        [StringLength(150)]
        public string CorporateName { get; set; }

        [Required]
        [StringLength(150)]
        public string LegalName { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(18)]
        public string RFC { get; set; }

        [ForeignKey("CommercialAddressId")]
        public virtual Address CommercialAddress { get; set; }

        [ForeignKey("FiscalAddressId")]
        public virtual Address FiscalAddress { get; set; }

    }


}
