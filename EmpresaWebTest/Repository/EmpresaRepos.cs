using Empresa.Services;
using System.Net.Http.Headers;

namespace EmpresaWebTest.Repository
{
    public class EmpresaRepos: IEmpresaRepos
    {
        static HttpClient client = new HttpClient();
        private readonly string _url;

        public EmpresaRepos(string Uri)
        {
            _url = Uri;
        }

        #region Manteminiemtos de Clientes
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

        public async Task<Models.ProcessReturn> CrearclienteAsync(Cliente cli)
        {
            Models.ProcessReturn rt = new Models.ProcessReturn();

            try
            {
                Empresa.Services.RestApi rp = new Empresa.Services.RestApi(client);
                rp.BaseUrl = _url;
                rt.ObjectReturn = (await rp.CrearclienteAsync(cli));
            }
            catch (ApiException<ICollection<Error>> ex)
            {
                rt.error = ex.Result.ToList();
            }
            
            return rt;
        }

        #endregion

        #region Mantenimiento Cuentas
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
        public async Task<Models.ProcessReturn> CrearcuentaAsync(Cuenta objInput)
        {
            Models.ProcessReturn rt = new Models.ProcessReturn();

            try
            {
                Empresa.Services.RestApi rp = new Empresa.Services.RestApi(client);
                rp.BaseUrl = _url;
                rt.ObjectReturn = (await rp.CrearcuentaAsync(objInput));
            }
            catch (ApiException<ICollection<Error>> ex)
            {
                rt.error = ex.Result.ToList();
            }

            return rt;
        }

        #endregion

        #region Mantenimiento Movimientos
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

        public async Task<Models.ProcessReturn> CrearmovimientosAsync(Movimiento objInput)
        {
            Models.ProcessReturn rt = new Models.ProcessReturn();

            try
            {
                Empresa.Services.RestApi rp = new Empresa.Services.RestApi(client);
                rp.BaseUrl = _url;
                rt.ObjectReturn = (await rp.CrearmovimientosAsync(objInput));
            }
            catch (ApiException<ICollection<Error>> ex)
            {
                rt.error = ex.Result.ToList();
            }

            return rt;
        }
        #endregion

        public async  Task<List<Empresa.Services.MovimientoReporte>> ObtenermovimientosreporteAsync( DateTime? Inicio, DateTime? Fin, int IdCliente )
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
