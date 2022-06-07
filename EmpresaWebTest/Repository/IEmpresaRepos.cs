using Empresa.Services;

namespace EmpresaWebTest.Repository
{
    public interface IEmpresaRepos
    {

        public Task<List<Empresa.Services.Cliente>> ObtenerclienteAsync();
        public Task<List<Empresa.Services.Cuenta>> ObtenercuentaAsync();

        public Task<List<Empresa.Services.Movimiento>> ObtenermovimientosAsync();

        public Task<List<Empresa.Services.MovimientoReporte>> ObtenermovimientosreporteAsync(DateTime? Inicio, DateTime? Fin, int IdCliente);

        public Task<Models.ProcessReturn> CrearclienteAsync(Cliente cli);
        public Task<Models.ProcessReturn> CrearcuentaAsync(Cuenta objInput);
        public Task<Models.ProcessReturn> CrearmovimientosAsync(Movimiento objInput);
    }
}
