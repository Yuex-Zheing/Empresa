using EFCtx = EmpresaAPI.EntityModels;
using EmpresaAPI.Models;
using AutoMapper;

namespace EmpresaAPI.Perfiles
{
    public class CuentasMapper: Profile
    {
        public CuentasMapper()
        {
            CreateMap<EFCtx.Cuenta, Cuenta>()
                .ForMember(o => o.IdCliente, b => b.MapFrom(z => z.Cliente))
                .ReverseMap(); ;
        }
    }
}
