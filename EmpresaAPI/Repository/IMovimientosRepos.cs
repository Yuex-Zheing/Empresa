using EmpresaAPI.Models;

namespace EmpresaAPI.Repository
{
    public interface IMovimientosRepos
    {

        public Movimiento? CrearMovimiento(Movimiento cl);
        public List<Movimiento> ConsultaMovimientos(int? IdMovimiento);

        public List<MovimientoReporte> ConsultaMovimientosPorRango(DateTime? Inicio, DateTime? Fin, int? idcliente);

        public bool EliminarMovimientos(int IdMovimiento);

        public Movimiento? ActualizarMovimiento(int? IdMovimiento, Movimiento mv);
    }
}
