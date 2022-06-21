using EmpresaWebTest.Models;
using EmpresaWebTest.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmpresaWebTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmpresaRepos rp;

        public HomeController(ILogger<HomeController> logger, IEmpresaRepos repos)
        {
            _logger = logger;
            rp = repos;
        }

        public IActionResult Index()

        {

            InfoEmpresa info = new InfoEmpresa();

            List<Empresa.Services.Cliente> clis = rp.ObtenerclienteAsync().Result;
            info._Clientes = clis;

            List<Empresa.Services.Cuenta> cta = rp.ObtenercuentaAsync().Result;


            List<InfoCuentas> InfCta = new List<InfoCuentas>();
            foreach (var c in cta)
            {
                InfCta.Add(new InfoCuentas()
                {
                    NumeroCuenta = c.NumeroCuenta,
                    TipoCuenta = (c.TipoCuenta == "A") ? "Ahorro" : "Corriente",
                    SaldoInicial = c.SaldoInicial,
                    Estado = (c.Estado == "A") ? "Activa" : "Inactiva",
                    Cliente = clis.Where(a => a.IdCliente == c.IdCliente).Select(x => x).First().Nombres ?? "N.A"
                });
            }

            info._Cuentas = InfCta;


            List<Empresa.Services.Movimiento> mov = rp.ObtenermovimientosAsync().Result;


            List<InfoMovimientos> InfMov = new List<InfoMovimientos>();
            foreach (var c in mov)
            {
                InfMov.Add(new InfoMovimientos()
                {
                    NumeroCuenta = cta.Where(a => a.IdCuenta == c.IdCuenta).First().NumeroCuenta,
                    TipoCuenta = (cta.Where(a => a.IdCuenta == c.IdCuenta).First().TipoCuenta == "A") ? "Ahorro" : "Corriente",
                    SaldoInicial = c.Saldo,
                    EstadoMovimiento = (c.Estado == "A") ? "Activa" : "Inactiva",
                    Movimiento = c.MovDescripcion
                });
            }

            info._Movimientos = InfMov;

            return View(info);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult MantenimientoClientes(IFormCollection cols)
        {
            Models.ProcessReturn taslist = new ProcessReturn();
            try
            {
                Empresa.Services.Cliente cl = new Empresa.Services.Cliente()
                {
                    Nombres = cols["txtnombre"],
                    Direccion = cols["txtdireccion"],
                    Telefono = cols["txtnombre"],
                    Clave = cols["txtnombre"],
                    Edad = int.Parse(cols["txtedad"].ToString()),
                    Estado = cols["ddlestado"],
                    Genero = cols["ddlgenero"],
                    //IdCliente = cols["txt"],
                    Identificacion = cols["txtidentificacion"],
                };

                taslist = rp.CrearclienteAsync(cl).Result;

            }
            catch (Exception ex) {
                taslist.error = new List<Empresa.Services.Error>() { 
                    new Empresa.Services.Error() { 
                        IdError = 100,
                        MensajeTecnico = ex.Message,
                        MensajeUsuario = "Ingrese todos los datos correctamente!"
                    }
                };
            }
            return new ObjectResult(taslist);

        }

        [HttpPost]
        public IActionResult MantenimientoCuentas(IFormCollection cols)
        {
            Models.ProcessReturn taslist = new ProcessReturn();
            try
            {
                Empresa.Services.Cuenta objProcess = new Empresa.Services.Cuenta()
                {
                    Estado = cols["ddlestado"],
                    NumeroCuenta = cols["txtnumerocuenta"],
                    SaldoInicial = Double.Parse(cols["txtsaldoinicial"].ToString()),
                    TipoCuenta = cols["ddltipocuenta"],
                    //IdCuenta
                    IdCliente = int.Parse(cols["ddlclientes"].ToString())
                };

                taslist = rp.CrearcuentaAsync(objProcess).Result;
            }
            catch (Exception ex)
            {
                taslist.error = new List<Empresa.Services.Error>() {
                    new Empresa.Services.Error() {
                        IdError = 100,
                        MensajeTecnico = ex.Message,
                        MensajeUsuario = "Ingrese todos los datos correctamente!"
                    }
};
            }
            return new ObjectResult(taslist);

        }

        [HttpPost]
        public IActionResult MantenimientoMovimientos(IFormCollection cols)
        {
            Models.ProcessReturn taslist = new ProcessReturn();
            try { 

            Empresa.Services.Movimiento objProcess = new Empresa.Services.Movimiento()
            {
                Estado = cols["ddlestado"],
                Fecha = (DateTimeOffset)DateTime.Now, //DateTimeOffset.Parse(cols["txtfecha"]),
                MovDescripcion = "N/A",//cols["txtdescripcion"],
                Saldo = 0, //Double.Parse(cols["txtsaldo"]),
                Valor = Double.Parse(cols["txtvalor"].ToString()),
                TipoMovimiento = cols["ddltipomovimiento"],
                IdCuenta = int.Parse(cols["ddlcuentas"])
                //IdMovimiento
            };

            taslist = rp.CrearmovimientosAsync(objProcess).Result;
        }
            catch (Exception ex)
            {
                taslist.error = new List<Empresa.Services.Error>() {
                    new Empresa.Services.Error() {
                        IdError = 100,
                        MensajeTecnico = "Ingrese todos los datos correctamente!",
                        MensajeUsuario = "Ingrese todos los datos correctamente!"
                    }
};
            }
            return new ObjectResult(taslist);

        }

        #region Utils

        public IActionResult consultaClientesCuentas() {

            List<Empresa.Services.Cliente> clis = rp.ObtenerclienteAsync().Result;
            List<Empresa.Services.Cuenta> cta = rp.ObtenercuentaAsync().Result;

            List<InfoClientes> cls = new List<InfoClientes>();

            if( clis.Count > 0 )//&& cta.Count > 0 )

            foreach (var cl in clis)
            {
                cls.Add(new InfoClientes(cl)
                {
                    infoCuentas = (cta.Count > 0) ? cta.Where(x => x.IdCliente == cl.IdCliente).ToList() : new List<Empresa.Services.Cuenta>()
                });
            }

            return new ObjectResult(cls);
        }

        #endregion
    }
}