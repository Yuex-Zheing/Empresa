using System;
using System.Collections.Generic;

namespace EmpresaAPI.EntityModels
{
    public partial class Cliente
    {
        public Cliente()
        {
            Cuenta = new HashSet<Cuenta>();
        }

        public int IdCliente { get; set; }
        public int Persona { get; set; }
        public string Clave { get; set; } = null!;
        public string Estado { get; set; } = null!;

        public virtual Persona PersonaNavigation { get; set; } = null!;
        public virtual ICollection<Cuenta> Cuenta { get; set; }
    }
}
