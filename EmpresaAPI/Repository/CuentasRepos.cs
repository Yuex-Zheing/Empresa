using EmpresaAPI.Models;
using EMCtx = EmpresaAPI.EntityModels;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EmpresaAPI.Repository
{
    public class CuentasRepos : ICuentasRepos
    {
        public readonly EMCtx.EmpresaContext EFCtx;
        private readonly IMapper _mapper;

        public CuentasRepos(IMapper mapper, EMCtx.EmpresaContext dbCtx)
        {
            this.EFCtx = dbCtx;
            _mapper = mapper;
        }

        public Cuenta? ActualizarCuenta(int? IdCuenta, Cuenta ct)
        {
            Cuenta rc = null;

            EMCtx.Cuenta cta = (from x in EFCtx.Cuentas
                                    .Include(b => b.ClienteNavigation)
                                    //.Include(b => b.ClienteNavigation.PersonaNavigation)
                                    .Where(o => o.IdCuenta == IdCuenta)
                                 select x).First();

            if (cta != null)
            {

                //cta.NumeroCuenta = ct.NumeroCuenta;
                //cta.TipoCuenta = ct.TipoCuenta;
                cta.SaldoInicial = Convert.ToDecimal(ct.SaldoInicial);
                cta.Estado = ct.Estado;

                EFCtx.SaveChanges();

                rc = _mapper.Map<Cuenta>(cta);
            }

            return rc;
        }

        public List<Cuenta> ConsultaCuenta(int? IdCuenta)
        {
            List<EMCtx.Cuenta> ctas;

            if (IdCuenta == null)
            {
                ctas = (from x in EFCtx.Cuentas
                       .Include(b => b.ClienteNavigation)
                       .Where(o => o.Estado.Equals("A"))
                       .OrderByDescending(b => b.NumeroCuenta)
                        select x).ToList();
            }
            else
            {
                ctas = (from x in EFCtx.Cuentas
                       .Include(b => b.ClienteNavigation )
                       .Where(o => o.Estado.Equals("A") && o.IdCuenta == IdCuenta)
                        select x).ToList();
            }

            return _mapper.Map<List<Cuenta>>(ctas);
        }

        public Cuenta? CrearCuenta(Cuenta ct)
        {
            EMCtx.Cuenta cta = null;

            if (ct.IdCliente != 0)
            {
                cta = new EMCtx.Cuenta()
                {
                    //IdCuenta
                    Cliente = ct.IdCliente ?? 0,
                    NumeroCuenta = ct.NumeroCuenta,
                    TipoCuenta = ct.TipoCuenta,
                    SaldoInicial = Convert.ToDecimal(ct.SaldoInicial),
                    Estado = "A"
                };

                EFCtx.Cuentas.Add(cta);
                EFCtx.SaveChanges();
            }

            return _mapper.Map<Cuenta>(cta);
        }

        public bool EliminarCuenta(int IdCuenta)
        {
            bool isOK = false;

            EMCtx.Cuenta cta = (from x in EFCtx.Cuentas
                                     .Include(b => b.ClienteNavigation)
                                     .Where(o => o.Estado.Equals("A") && o.IdCuenta == IdCuenta)
                                 select x).First();

            if (cta != null)
            {
                cta.Estado = "I";
                //EFCtx.Cuentas.Remove(cta);
                EFCtx.SaveChanges();
                isOK = true;
            }

            return isOK;
        }
    }
}
