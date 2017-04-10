using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Glocation.Dominio.Entidades
{
    [Table("User")]
    public class User : BaseEntity
    {        

        public User() { }

        public User (int userId, String name)
        {
            UserId = userId;
            Name = name;
        }
        [Key]
        public int UserId { get; set; }

        
        public String Name { get; set; }
    }
}
