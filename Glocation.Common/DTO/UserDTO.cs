using System;
using System.Collections;
using System.Collections.Generic;
using Glocation.Dominio.Entidades;

namespace Glocation.Common.DTO
{
    public class UserDTO
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Roles> Roles { get; set; }
    }
}
