using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glocation.Dominio.Entidades
{
    [Table("Roles")]
    public class Roles :BaseEntity
    {
        public Roles()
        {
            Accounts = new HashSet<Accounts>();
        }

        [Key]
        public int RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public Boolean status { get; set; }

        public virtual ICollection<Accounts> Accounts { get; set; }
    }
}
