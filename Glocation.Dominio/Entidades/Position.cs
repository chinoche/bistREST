using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glocation.Dominio.Entidades
{
    [Table("Position")]
    public class Position : BaseEntity
    {
        public Position () { }

        [Key]
        public int PositionId { get; set; }

        [Required]
        [StringLength(10)]
        public string coordinates { get; set; }

    }
}
