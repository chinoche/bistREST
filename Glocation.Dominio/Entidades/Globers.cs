using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glocation.Dominio.Entidades
{
    [Table("Globers")]
    public class Globers : BaseEntity
    {
        public Globers() { }

        [Key]
        public int GloberId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        public int PositionId { get; set; }

        public int RoleId { get; set; }

        public int ProjectId { get; set; }
        public virtual Position Position { get; set;}

        public virtual Projects Projects { get; set; }

        public virtual Roles Roles { get; set; }
    }
}
