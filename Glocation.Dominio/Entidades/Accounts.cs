using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glocation.Dominio.Entidades
{
    [Table("Accounts")]
    public class Accounts: BaseEntity
    {
        public Accounts()
        {
            Roles = new HashSet<Roles>();
        }

        [Key]
        public string UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Email{ get; set; }

        public virtual ICollection<Roles> Roles { get; set; }


    }
}