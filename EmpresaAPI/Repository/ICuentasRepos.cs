using EmpresaAPI.Models;

namespace EmpresaAPI.Repository
{
    public interface ICuentasRepos
    {
        public Cuenta? CrearCuenta(Cuenta ct);
        public List<Cuenta> ConsultaCuenta(int? IdCuenta);

        public bool EliminarCuenta(int IdCuenta);

        public Cuenta? ActualizarCuenta(int? IdCuenta, Cuenta ct);
    }
}
