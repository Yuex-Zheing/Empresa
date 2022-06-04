namespace EmpresaWebTest.Repository
{
    public interface IEMpresaRepos
    {

        public Task<List<Empresa.Services.Cliente>> ObtenerclienteAsync();
        public Task<List<Empresa.Services.Cuenta>> ObtenercuentaAsync();

        public Task<List<Empresa.Services.Movimiento>> ObtenermovimientosAsync();

        public Task<List<Empresa.Services.MovimientoReporte>> ObtenermovimientosreporteAsync(DateTime? Inicio, DateTime? Fin, int IdCliente);


    }
}
