using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIST.Dominio.Entidades
{
    [Table("Projects")]
    public class Projects : BaseEntity
    {
        public Projects()
        {
            Accounts = new HashSet<Accounts>();
        }

        [Key]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProjectName { get; set; }

        [Required]
        [StringLength(150)]
        public string ShippingAddress { get; set; }            

        public virtual ICollection<Accounts> Accounts { get; set; }
        
    }
}
