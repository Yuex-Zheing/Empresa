﻿using System.Net.Http.Headers;

namespace EmpresaWebTest.Repository
{
    public class EmpresaRepos: IEMpresaRepos
    {
        static HttpClient client = new HttpClient();
        private readonly string _url;

        public EmpresaRepos()
        {
            _url = "http://localhost:8000/";
        }

        public async Task<List<Empresa.Services.Cliente>> ObtenerclienteAsync()
        {

            Empresa.Services.RestApi rp = new Empresa.Services.RestApi(client); //.ObtenerclienteAsync(3);
            rp.BaseUrl = _url;

            return (await rp.ObtenerclienteAsync(null)).ToList();
        }

        public async Task<List<Empresa.Services.Cuenta>> ObtenercuentaAsync()
        {

            Empresa.Services.RestApi rp = new Empresa.Services.RestApi(client); //.ObtenerclienteAsync(3);
            rp.BaseUrl = _url;

            return (await rp.ObtenercuentaAsync(null)).ToList();
        }

        public async Task<List<Empresa.Services.Movimiento>> ObtenermovimientosAsync()
        {

            Empresa.Services.RestApi rp = new Empresa.Services.RestApi(client); //.ObtenerclienteAsync(3);
            rp.BaseUrl = _url;

            return (await rp.ObtenermovimientosAsync(null)).ToList();
        }

        public async Task<List<Empresa.Services.MovimientoReporte>> ObtenermovimientosreporteAsync( DateTime? Inicio, DateTime? Fin, int IdCliente )
        {
          

            Empresa.Services.RestApi rp = new Empresa.Services.RestApi(client); //.ObtenerclienteAsync(3);
            rp.BaseUrl = _url;

            return (await rp.ObtenermovimientosreporteAsync((DateTimeOffset)Inicio, (DateTimeOffset)Fin, IdCliente)).ToList();
        }


    }
}
