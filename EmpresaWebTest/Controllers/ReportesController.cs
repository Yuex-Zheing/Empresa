using EmpresaWebTest.Models;
using EmpresaWebTest.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmpresaWebTest.Controllers
{
    public class ReportesController : Controller
    {
        private readonly IEMpresaRepos rp;

        public ReportesController(IEMpresaRepos repos)
        {
           
            rp = repos;
        }
        public IActionResult Index()
        {

            InfoEmpresa info = new InfoEmpresa();


            DateTime Init = new DateTime(2022, 6, 1, 0, 0, 0, 0);
            DateTime Fin = new DateTime(2022, 6, 6, 0, 0, 0, 0);
            int IdCliente = 1;

            List<Empresa.Services.MovimientoReporte> clis = rp.ObtenermovimientosreporteAsync(Init, Fin, IdCliente).Result;
           

            return View(clis);
        }
    }
}
