using System;
using System.Collections.Generic;

namespace EmpresaAPI.EntityModels
{
    public partial class Cuenta
    {
        public Cuenta()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        public int IdCuenta { get; set; }
        public int Cliente { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public string TipoCuenta { get; set; } = null!;
        public decimal SaldoInicial { get; set; }
        public string Estado { get; set; } = null!;

        public virtual Cliente ClienteNavigation { get; set; } = null!;
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
