using EFCtx = EmpresaAPI.EntityModels;
using EmpresaAPI.Models;
using AutoMapper;

namespace EmpresaAPI.Perfiles
{
    public class MovimientosMapper: Profile
    {
        public MovimientosMapper()
        {
            CreateMap<EFCtx.Movimiento, Movimiento>()
                .ForMember(o => o.IdCuenta, b => b.MapFrom(z => z.Cuenta))
                    .ReverseMap(); ;
        }

    }
}
