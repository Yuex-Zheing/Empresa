namespace EmpresaWebTest.Repository
{
    public interface IEMpresaRepos
    {

        public Task<List<Empresa.Services.Cliente>> ObtenerclienteAsync();
        public Task<List<Empresa.Services.Cuenta>> ObtenercuentaAsync();

        public  Task<List<Empresa.Services.Movimiento>> ObtenermovimientosAsync();

    }
}
