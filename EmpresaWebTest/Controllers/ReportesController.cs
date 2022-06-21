using Empresa.Services;
using EmpresaWebTest.Models;
using EmpresaWebTest.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmpresaWebTest.Controllers
{
    public class ReportesController : Controller
    {
        private readonly IEmpresaRepos rp;

        public ReportesController(IEmpresaRepos repos)
        {

            rp = repos;
        }
        public IActionResult Index()
        {

            InfoEmpresa info = new InfoEmpresa();


            DateTime Init = new DateTime(2022, 6, 1, 0, 0, 0, 0);
            DateTime Fin = new DateTime(2022, 12, 6, 0, 0, 0, 0);
            int IdCliente = 1;

            List<Empresa.Services.MovimientoReporte> clis = rp.ObtenermovimientosreporteAsync(Init, Fin, IdCliente).Result;


            return View(clis);
        }

        public IActionResult ReportePorRangoFechas(DateTime dtInicio, DateTime dtFin, int IdCliente )
        {
            InfoEmpresa info = new InfoEmpresa();
            Task<List<Empresa.Services.MovimientoReporte>> rpt = null;
            List<Empresa.Services.MovimientoReporte> clis = new List<Empresa.Services.MovimientoReporte>();

                rpt = rp.ObtenermovimientosreporteAsync(dtInicio, dtFin, IdCliente);
                clis = rpt.Result;
            
            return PartialView("_ReportePorRangoFechas",clis);
        }


    }
}
