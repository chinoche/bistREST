using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIST.Dominio.Entidades
{
    [Table("Address")]
    public class Address : BaseEntity
    {

        public Address()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }

        [Required]
        [StringLength(150)]
        public string Street { get; set; }

        [Required]
        [StringLength(150)]
        public string City { get; set; }

        [Required]
        [StringLength(10)]
        public string PostalCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Neighborhood { get; set; }

        [Required]
        [StringLength(100)]
        public string State { get; set; }
        [Required]
        [StringLength(200)]
        public string Location { get; set; }

        [Required]
        [StringLength(100)]
        public string Town { get; set; }

        [Required]
        [StringLength(100)]
        public string Country { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

    }
}