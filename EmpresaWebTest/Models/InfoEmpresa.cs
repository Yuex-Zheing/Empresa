namespace EmpresaWebTest.Models
{
    public class InfoEmpresa
    {
        public List<Empresa.Services.Cliente> _Clientes { get; set; }
        public List<InfoCuentas> _Cuentas { get; set; }
        public List<InfoMovimientos> _Movimientos { get; set; }

        public InfoEmpresa()
        {
            _Clientes = new List<Empresa.Services.Cliente>();
            _Cuentas = new List<InfoCuentas>();
            _Movimientos = new List<InfoMovimientos>();
        }
    }
}
