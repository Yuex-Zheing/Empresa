namespace EmpresaWebTest.Models
{
    public class InfoClientes: Empresa.Services.Cliente
    {
        //public string Cliente { get; set; }
        //public string Identificacion { get; set; }
        //public string EstadoCliente { get; set; }
        //public int IdCliente { get; set; }
        public List<Empresa.Services.Cuenta> infoCuentas { get; set; }
        public InfoClientes(Empresa.Services.Cliente cl)
        {
            Nombres = cl.Nombres;
            Identificacion = cl.Identificacion;
            Estado = cl.Estado; 
            IdCliente  = cl.IdCliente;
            infoCuentas = new List<Empresa.Services.Cuenta>();
        }
    }
}
