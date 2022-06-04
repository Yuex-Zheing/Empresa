using EmpresaWebTest.Models;
using EmpresaWebTest.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmpresaWebTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEMpresaRepos rp;

        public HomeController(ILogger<HomeController> logger, IEMpresaRepos repos)
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
                    NumeroCuenta = cta.Where(a=>a.IdCuenta == c.IdCuenta).First().NumeroCuenta,
                    TipoCuenta = (cta.Where(a => a.IdCuenta == c.IdCuenta).First().TipoCuenta == "A") ? "Ahorro" : "Corriente",
                    SaldoInicial = c.Saldo,
                    EstadoMovimiento = (c.Estado == "A") ? "Activa" : "Inactiva",
                    Movimiento = c.MovDescripcion
                });
            }

            info._Movimientos = InfMov;

            return View(info);
        }

        //public IActionResult TablaClientes()
        //{
        //    ClientesRepos rp = new ClientesRepos();

        //    List<Empresa.Services.Cliente> clis = rp.ObtenerclienteAsync().Result;

        //    return PartialView("TablaClientes" ,clis);
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}