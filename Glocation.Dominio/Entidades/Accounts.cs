using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIST.Dominio.Entidades
{
    [Table("Accounts")]
    public class Accounts: BaseEntity
    {
        public Accounts()
        {
            Roles = new HashSet<Roles>();
            Projects = new HashSet<Projects>();
        }

        [Key]
        public string AccountId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Email{ get; set; }

        public virtual ICollection<Roles> Roles { get; set; }

        public virtual ICollection<Projects> Projects { get; set; }
    }
}