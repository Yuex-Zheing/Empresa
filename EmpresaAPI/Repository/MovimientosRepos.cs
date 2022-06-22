using EmpresaAPI.Models;
using EMCtx = EmpresaAPI.EntityModels;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EmpresaAPI.Repository
{
    public class MovimientosRepos : IMovimientosRepos
    {
        public readonly EMCtx.EmpresaContext EFCtx;
        private readonly IMapper _mapper;
        private const decimal MaxRetiro = 1000;

        public MovimientosRepos(IMapper mapper, EMCtx.EmpresaContext dbCtx)
        {
            this.EFCtx = dbCtx;
            _mapper = mapper;
        }
        public Movimiento? ActualizarMovimiento(int? IdMovimiento, Movimiento mv)
        {
            Movimiento rm = null;

            EMCtx.Movimiento mvt = (from x in EFCtx.Movimientos
                                    .Include(b => b.CuentaNavigation)
                                    .Where(o => o.IdMovimiento == IdMovimiento)
                                 select x).First();

            if (mvt != null)
            {

                //mvt.IdMovimiento
                //mvt.IdCuenta = mv.IdCuenta ?? 0;
                //mvt.Fecha = mv.Fecha ?? DateTime.Now;
                //mvt.TipoMovimiento = mv.TipoMovimiento;
                //mvt.Valor = Convert.ToDecimal( mv.Valor );
                //mvt.Saldo = Convert.ToDecimal( mv.Saldo );
                //mvt.MovDescripcion = mv.MovDescripcion;
                mvt.Estado = mv.Estado;

                EFCtx.SaveChanges();

                rm = _mapper.Map<Movimiento>(mvt);
            }

            return rm;
        }

        public List<Movimiento> ConsultaMovimientos(int? IdMovimiento)
        {
            List<EMCtx.Movimiento> clis;

            if (IdMovimiento == null)
            {
                clis = (from x in EFCtx.Movimientos
                       .Include(b => b.CuentaNavigation)
                       .OrderByDescending(o => o.CuentaNavigation.NumeroCuenta)
                       .ThenByDescending(z => z.IdMovimiento)
                        select x).ToList();
            }
            else
            {
                clis = (from x in EFCtx.Movimientos
                       .Include(b => b.CuentaNavigation)
                       .Where(z => z.IdMovimiento == IdMovimiento)
                        select x).ToList();
            }

            return _mapper.Map<List<Movimiento>>(clis);
        }

        public List<MovimientoReporte> ConsultaMovimientosPorRango(DateTime? Inicio, DateTime? Fin, int? idcliente)
        {
            List<MovimientoReporte> rMR = new List<MovimientoReporte>();          

            var Movs = EFCtx.Movimientos
                               .Include( c=> c.CuentaNavigation)
                               .Include(a => a.CuentaNavigation.ClienteNavigation)
                               .Include(p => p.CuentaNavigation.ClienteNavigation.PersonaNavigation)
                               .Where( x=> x.Fecha >= Inicio && x.Fecha <= Fin  && x.CuentaNavigation.ClienteNavigation.IdCliente == idcliente)
                               .OrderByDescending(z => z.CuentaNavigation.NumeroCuenta )
                               .ThenByDescending(z => z.IdMovimiento)
                               .Select( s=> new {
                                   s.Fecha,
                                   s.CuentaNavigation.ClienteNavigation.PersonaNavigation.Nombre,
                                   s.CuentaNavigation.NumeroCuenta,
                                   TipoCuenta = s.CuentaNavigation.TipoCuenta.Equals("A") ? "AHORRO" : "CORRIENTE",
                                   s.CuentaNavigation.SaldoInicial,
                                   MovimientoEstado = s.Estado.Equals("A") ? "APROBADO" : "RECHAZADO",
                                   Movimiento =  s.TipoMovimiento.Equals("D") ? s.Valor*-1: s.Valor,
                                   Saldo = s.Saldo
                               })                               
                               .ToList();

            foreach (var dato in Movs)
            {
                rMR.Add(new MovimientoReporte()
                {
                    Cliente = dato.Nombre,
                    EstadoMovimiento = dato.MovimientoEstado,
                    Fecha = dato.Fecha,
                    NumeroCuenta = dato.NumeroCuenta,
                    TipoCuenta = dato.TipoCuenta,
                    SaldoDisponible = Convert.ToDouble(dato.Saldo),
                    SaldoInicial = Convert.ToDouble(dato.SaldoInicial),
                    Movimiento = Convert.ToDouble( dato.Movimiento)
                });
            }

            return rMR;
        }

        public bool LimiteRetiroXDia(int IdCuenta) {
            bool limite = true;


            return limite;
        }

        public Movimiento? CrearMovimiento(Movimiento cl)
        {
            // EL cliente esta activo y tiene cuentas activas
            EMCtx.Cuenta? cta = EFCtx.Cuentas
                                   .Include(a => a.ClienteNavigation)
                                   .Where(b => b.Estado == "A" && b.ClienteNavigation.Estado == "A" && b.IdCuenta == cl.IdCuenta)
                                   .FirstOrDefault();

            if (cta == null) throw new Exception("Cliente o Cuenta no existen o estan inhabilitada");

            decimal valorMov = Convert.ToDecimal( cl.Valor );
            decimal saldo = 0;
            string Description = "";
            decimal sumRetDiario = 0;

            //Validando registra movimientos anteriores en la cuenta
            List<EMCtx.Movimiento> mov = EFCtx.Movimientos
                                            .Include(a => a.CuentaNavigation)
                                            .Where(x => x.Cuenta == cl.IdCuenta)
                                            .OrderByDescending(z => z.IdMovimiento)
                                            .Take(50)
                                            .ToList();
                                    

            if (mov.Count <= 0) {
                //No tiene movimientos anteriores
                saldo = cta.SaldoInicial;

            }else{
                //Registra movmientos anteriores

                //Validar maximo de retiro por dia
                sumRetDiario = mov.Where(x => x.TipoMovimiento == "D" && x.Fecha >= DateTime.Today).Sum(z => z.Valor);             
                saldo = mov.First().Saldo;
            }

            if (cl.TipoMovimiento.Equals("C") && valorMov > 0)
            {
                // Por credito
                saldo += valorMov;
                Description = "Deposito de " + valorMov;

            }
            else if (cl.TipoMovimiento.Equals("D") && valorMov > 0)
            {                
                // por debito
                saldo -= valorMov;
                Description = "Retiro de " + valorMov;

                if ((sumRetDiario + valorMov) > MaxRetiro) throw new Exception("Cupo diario excedido");

                if (saldo < 0) throw new Exception("Saldo no disponible");
                
            }
            else {
                throw new Exception("Operacion no valida");
            }

            EMCtx.Movimiento mv = new EMCtx.Movimiento()
            {
                //mv.IdMovimiento
                Cuenta = cl.IdCuenta ?? 0,
                Fecha = cl.Fecha ?? DateTime.Now,
                TipoMovimiento = cl.TipoMovimiento,
                Valor = valorMov,
                Saldo = saldo,
                Estado = cl.Estado,
                MovDescripcion = Description
            };

            EFCtx.Movimientos.Add(mv);
            EFCtx.SaveChanges();          

            return _mapper.Map<Movimiento>(mv);
        }

        public bool EliminarMovimientos(int IdMovimiento)
        {

            bool isOK = false;

            EMCtx.Movimiento cli = (from x in EFCtx.Movimientos
                                     .Include(b => b.CuentaNavigation)
                                     .Where(o => o.Estado.Equals("A") && o.IdMovimiento == IdMovimiento)
                                 select x).First();

            if (cli != null)
            {
                cli.Estado = "R";             
                EFCtx.SaveChanges();
                isOK = true;
            }

            return isOK;
        }
    }
}
