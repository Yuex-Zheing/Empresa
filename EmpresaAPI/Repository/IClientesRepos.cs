using EmpresaAPI.Models;

namespace EmpresaAPI.Repository
{
    public interface IClientesRepos
    {

        public Cliente? CrearCliente(Cliente cl);
        public List<Cliente> ConsultaCliente(int? IdCliente);

        public bool EliminarCliente(int IdCliente);

        public Cliente? ActualizarCliente(int? idCliente, Cliente cl);
    }
}
