using Empresa.Services;
using System.Net.Http.Headers;

namespace EmpresaWebTest.Repository
{
    public class EmpresaRepos: IEMpresaRepos
    {
        static HttpClient client = new HttpClient();
        private readonly string _url;

        public EmpresaRepos(string Uri)
        {
            _url = Uri;
        }

        public async Task<List<Empresa.Services.Cliente>> ObtenerclienteAsync()
        {
            List<Cliente> taskApi = new List<Cliente>();
            try
            {

                Empresa.Services.RestApi rp = new Empresa.Services.RestApi(client); //.ObtenerclienteAsync(3);
                rp.BaseUrl = _url;
                taskApi = (await rp.ObtenerclienteAsync(null)).ToList();

            }
            catch (ApiException<ICollection<Error>> ex)
            {

                if (ex.Result.Count > 0)
                    taskApi = new List<Cliente>();

            }
            return taskApi;
        }

        public async Task<List<Empresa.Services.Cuenta>> ObtenercuentaAsync()
        {
            List<Cuenta> taskApi = new List<Cuenta>();
            try
            {

                Empresa.Services.RestApi rp = new Empresa.Services.RestApi(client); //.ObtenerclienteAsync(3);
                rp.BaseUrl = _url;
                taskApi = (await rp.ObtenercuentaAsync(null)).ToList();

            }
            catch (ApiException<ICollection<Error>> ex)
            {

                if (ex.Result.Count > 0)
                    taskApi = new List<Cuenta>();

            }
            return taskApi;
        }

        public async Task<List<Empresa.Services.Movimiento>> ObtenermovimientosAsync()
        {
            List<Movimiento> taskApi = new List<Movimiento>();
            try
            {

                Empresa.Services.RestApi rp = new Empresa.Services.RestApi(client); //.ObtenerclienteAsync(3);
                rp.BaseUrl = _url;
                taskApi = (await rp.ObtenermovimientosAsync(null)).ToList();

            }
            catch (ApiException<ICollection<Error>> ex)
            {

                if (ex.Result.Count > 0)
                    taskApi = new List<Movimiento>();

            }
            return taskApi;

        }

        public async Task<List<Empresa.Services.MovimientoReporte>> ObtenermovimientosreporteAsync( DateTime? Inicio, DateTime? Fin, int IdCliente )
        {
          

            Empresa.Services.RestApi rp = new Empresa.Services.RestApi(client); //.ObtenerclienteAsync(3);
            rp.BaseUrl = _url;
            List<Empresa.Services.MovimientoReporte> taskApi = new List<MovimientoReporte>();

            try {
               var _taskApi = await rp.ObtenermovimientosreporteAsync((DateTimeOffset)Inicio, (DateTimeOffset)Fin, IdCliente);
                taskApi = _taskApi.ToList();
            }
            catch (ApiException<ICollection<Error>> ex)
            {

                if(ex.Result.Count > 0)
                    taskApi = new List<MovimientoReporte>();

            }

            return taskApi;
        }


    }
}
