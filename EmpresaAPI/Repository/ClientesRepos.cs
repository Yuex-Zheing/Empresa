using EmpresaAPI.Models;
using EMCtx = EmpresaAPI.EntityModels;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EmpresaAPI.Repository
{
    public class ClientesRepos: IClientesRepos
    {

        public readonly EMCtx.EmpresaContext EFCtx;
        private readonly IMapper _mapper;

        public ClientesRepos(IMapper mapper, EMCtx.EmpresaContext dbCtx)
        {
            this.EFCtx = dbCtx;
            _mapper = mapper;
        }

        public Cliente? ActualizarCliente(int? idCliente, Cliente cl)
        {
            Cliente rt = null;

            EMCtx.Cliente cli = (from x in EFCtx.Clientes
                                    .Include(b => b.PersonaNavigation)
                                    .Where(o => o.IdCliente == idCliente)
                                 select x).First();

            if (cli != null)
            {

                cli.Clave = cl.Clave;
                cli.Estado = cl.Estado;
                cli.PersonaNavigation.Nombre = cl.Nombres;
                cli.PersonaNavigation.Genero = cl.Genero;
                cli.PersonaNavigation.Edad = cl.Edad ?? 0;
                cli.PersonaNavigation.Identificacion = cl.Identificacion;
                cli.PersonaNavigation.Direccion = cl.Direccion;
                cli.PersonaNavigation.Telefono = cl.Telefono;

                EFCtx.SaveChanges();

                rt = _mapper.Map<Cliente>(cli);
            }

            return rt;
        }

        public List<Cliente> ConsultaCliente(int? IdCliente)
        {

            List<EMCtx.Cliente> clis;

            if (IdCliente == null)
            {
                clis = (from x in EFCtx.Clientes
                       .Include(b=>b.PersonaNavigation)
                       .Where(o => o.Estado.Equals("A"))
                       .OrderByDescending( z=>z.IdCliente)
                        select x).ToList();
            }
            else {
                clis = (from x in EFCtx.Clientes
                        .Include(b => b.PersonaNavigation)
                        .Where(o => o.Estado.Equals("A") && o.IdCliente == IdCliente)
                        select x).ToList();
            }

            return _mapper.Map<List<Cliente>>(clis);
        }

        public Cliente? CrearCliente(Cliente cl)
        {
            Cliente cli;

            EMCtx.Persona pn = new EMCtx.Persona()
            {
                Nombre = cl.Nombres,
                Direccion = cl.Direccion,
                Edad = cl.Edad ?? 0,
                Genero = cl.Genero,
                Identificacion = cl.Identificacion,
                Telefono = cl.Telefono,
                //IdPersona
            };

            EMCtx.Cliente EFcli = new EMCtx.Cliente()
            {
                // IdCliente
                PersonaNavigation = pn,
                //IdPersona

                Estado = cl.Estado,
                Clave = cl.Clave,
                //Cuenta = "",                   
            };

            EFCtx.Clientes.Add(EFcli);
            EFCtx.SaveChanges();
            EFCtx.Dispose();

            cli = _mapper.Map<Cliente>(EFcli);

            return cli;
        }

        public bool EliminarCliente(int IdCliente)
        {
            bool isOK = false;

            EMCtx.Cliente cli = (from x in EFCtx.Clientes
                                     .Include(b => b.PersonaNavigation)
                                     .Where(o => o.Estado.Equals("A") && o.IdCliente == IdCliente)
                                 select x).First();

            if (cli != null)
            {
                cli.Estado = "I";
                //EFCtx.Personas.Remove(cli.PersonaNavigation);
                //EFCtx.Clientes.Remove(cli);                
                EFCtx.SaveChanges();  
                isOK = true;
            }

            return isOK;
        }
    }
}
