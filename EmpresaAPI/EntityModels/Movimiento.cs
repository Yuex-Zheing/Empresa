using System;
using System.Collections.Generic;

namespace EmpresaAPI.EntityModels
{
    public partial class Movimiento
    {
        public long IdMovimiento { get; set; }
        public int Cuenta { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; } = null!;
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public string Estado { get; set; } = null!;
        public string? MovDescripcion { get; set; }

        public virtual Cuenta CuentaNavigation { get; set; } = null!;
    }
}
