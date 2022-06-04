using EFCtx = EmpresaAPI.EntityModels;
using EmpresaAPI.Models;
using AutoMapper;

namespace EmpresaAPI.Perfiles
{
    public class ClientesMapper: Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public ClientesMapper()
        {
           
            CreateMap<EFCtx.Cliente, Cliente>()
                //.ForMember(o => o.Persona, b => b.MapFrom(z => z.Persona))
                .ForMember(o => o.Nombres, b => b.MapFrom(z => z.PersonaNavigation.Nombre))
                .ForMember(o => o.Genero, b => b.MapFrom(z => z.PersonaNavigation.Genero))
                .ForMember(o => o.Edad, b => b.MapFrom(z => z.PersonaNavigation.Edad))
                .ForMember(o => o.Identificacion, b => b.MapFrom(z => z.PersonaNavigation.Identificacion))
                .ForMember(o => o.Direccion, b => b.MapFrom(z => z.PersonaNavigation.Direccion))
                .ForMember(o => o.Telefono, b => b.MapFrom(z => z.PersonaNavigation.Telefono))
                .ForMember(o => o.Clave, b => b.MapFrom(z => z.Clave))
                .ForMember(o => o.Estado, b => b.MapFrom(z => z.Estado))
                .ReverseMap();
        }
    }
}
