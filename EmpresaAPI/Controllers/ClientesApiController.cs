using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using EmpresaAPI.Attributes;
using EmpresaAPI.Models;

namespace EmpresaAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class ClientesApiController : ControllerBase
    {
        private readonly Repository.IClientesRepos repos;
        public ClientesApiController(Repository.IClientesRepos repos)
        {
           this.repos = repos;
        }


        /// <summary>
        /// Crear Cliente
        /// </summary>
        /// <remarks>Creacion de Clientes</remarks>
        /// <param name="body">Devuelve el objeto creado con su ID</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("/clientes")]
        [ValidateModelState]
        [SwaggerOperation("Crearcliente")]
        [SwaggerResponse(statusCode: 200, type: typeof(Cliente), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(List<Error>), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(List<Error>), description: "Internal Server Error")]
        public virtual IActionResult Crearcliente([FromBody] Cliente body)
        {
            List<Error> errs = new List<Error>();

            /*Validando errores de ingreso de informacion del usuario*/
            if (String.IsNullOrEmpty(body.Nombres))
            {
                errs.Add(new Error()
                {
                    IdError = 1,
                    MensajeTecnico = "Debe Ingresar los nombres completps",
                    MensajeUsuario = "Debe Ingresar los nombres completps",
                    NivelError = "401"
                });
            }

            if (String.IsNullOrEmpty(body.Direccion)) {
                errs.Add(new Error()
                {
                    IdError = 1,
                    MensajeTecnico = "Debe Ingresar la direccion completps",
                    MensajeUsuario = "Debe Ingresar la direccion completps",
                    NivelError = "402"
                });
            }

            if( errs.Count > 0 )
                return StatusCode(400, errs);

            try
            {

                Cliente? cl = repos.CrearCliente(body);
           
                if (cl != null)
                    return StatusCode(200, cl);
                else
                {
                    errs.Add(new Error()
                    {
                        IdError = 1,
                        MensajeTecnico = "No existen datos que consultar!!",
                        MensajeUsuario = "No existen datos que consultar!!",
                        NivelError = "400"
                    });
                    return StatusCode(400, errs);
                }
            }
            catch (Exception ex) {
                //Si hay errores es un 500
                errs.Add(new Error()
                {
                    IdError = 1,
                    MensajeTecnico = ex.Message,
                    MensajeUsuario = "Error al procesar su informacion!!, Contactese con el administrador!!",
                    NivelError = "500"
                });
                return StatusCode(500, errs);
            }
          
        }

        /// <summary>
        /// Actualizar Cliente
        /// </summary>
        /// <remarks>Actualizacion de clientes</remarks>
        /// <param name="idcliente"></param>
        /// <param name="body">Devuelve el cliente actualizado</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("/clientes/{idcliente}")]
        [ValidateModelState]
        [SwaggerOperation("Actualizarcliente")]
        [SwaggerResponse(statusCode: 200, type: typeof(Cliente), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(List<Error>), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(List<Error>), description: "Internal Server Error")]
        public virtual IActionResult Actualizarcliente([FromRoute][Required] int? idcliente, [FromBody] Cliente body)
        {

            List<Error> errs = new List<Error>();

            /*Validando errores de ingreso de informacion del usuario*/
            if (String.IsNullOrEmpty(body.Nombres))
            {
                errs.Add(new Error()
                {
                    IdError = 1,
                    MensajeTecnico = "Debe Ingresar los nombres completps",
                    MensajeUsuario = "Debe Ingresar los nombres completps",
                    NivelError = "401"
                });
            }

            if (String.IsNullOrEmpty(body.Direccion))
            {
                errs.Add(new Error()
                {
                    IdError = 1,
                    MensajeTecnico = "Debe Ingresar la direccion completps",
                    MensajeUsuario = "Debe Ingresar la direccion completps",
                    NivelError = "402"
                });
            }

            if (errs.Count > 0)
                return StatusCode(400, errs);

            try
            {

                Cliente? cl = repos.ActualizarCliente(idcliente, body);

                if (cl != null)
                    return StatusCode(200, cl);
                else
                {
                    errs.Add(new Error()
                    {
                        IdError = 1,
                        MensajeTecnico = "No existen datos que actualizar!!",
                        MensajeUsuario = "No existen datos que actualizar!!",
                        NivelError = "400"
                    });
                    return StatusCode(400, errs);
                }
            }
            catch (Exception ex)
            {
                //Si hay errores es un 500
                errs.Add(new Error()
                {
                    IdError = 1,
                    MensajeTecnico = ex.Message,
                    MensajeUsuario = "Error al procesar su informacion!!, Contactese con el administrador!!",
                    NivelError = "500"
                });
                return StatusCode(500, errs);
            }
        }

        /// <summary>
        /// Eliminar Cliente
        /// </summary>
        /// <remarks>Elimina cliente</remarks>
        /// <param name="idcliente"></param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("/clientes/{idcliente}")]
        [ValidateModelState]
        [SwaggerOperation("Eleminarcliente")]
        [SwaggerResponse(statusCode: 400, type: typeof(List<Error>), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(List<Error>), description: "Internal Server Error")]
        public virtual IActionResult Eleminarcliente([FromRoute][Required] int? idcliente)
        {
            List<Error> errs = new List<Error>();           

            if (idcliente <= 0)
            {
                errs.Add(new Error()
                {
                    IdError = 1,
                    MensajeTecnico = "Debe Ingresar un id valido",
                    MensajeUsuario = "Debe Ingresar un id valido",
                    NivelError = "402"
                });
            }

            if (errs.Count > 0)
                return StatusCode(400, errs);

            try
            {
               

                if (repos.EliminarCliente(idcliente ?? 0))
                    return StatusCode(200);
                else
                {
                    errs.Add(new Error()
                    {
                        IdError = 1,
                        MensajeTecnico = "No existen datos que eliminar!!",
                        MensajeUsuario = "No existen datos que eliminar!!",
                        NivelError = "400"
                    });
                    return StatusCode(400, errs);
                }
            }
            catch (Exception ex)
            {
                //Si hay errores es un 500
                errs.Add(new Error()
                {
                    IdError = 1,
                    MensajeTecnico = ex.Message,
                    MensajeUsuario = "Error al procesar su informacion!!, Contactese con el administrador!!",
                    NivelError = "500"
                });
                return StatusCode(500, errs);
            }
        }

        /// <summary>
        /// Consultar Clientes
        /// </summary>
        /// <remarks>Consulta de Clientes</remarks>
        /// <param name="idcliente">Identificador de cliente</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("/clientes")]
        [ValidateModelState]
        [SwaggerOperation("Obtenercliente")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Cliente>), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(List<Error>), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(List<Error>), description: "Internal Server Error")]
        public virtual IActionResult Obtenercliente([FromQuery] int? idcliente)
        {
            List<Error> errs = new List<Error>();

            try
            {

                List<Cliente> cl = repos.ConsultaCliente(idcliente);

                if (cl.Count > 0)
                    return StatusCode(200, cl);
                else
                {
                    errs.Add(new Error()
                    {
                        IdError = 1,
                        MensajeTecnico = "No existen datos que consultar!!",
                        MensajeUsuario = "No existen datos que consultar!!",
                        NivelError = "400"
                    });
                    return StatusCode(400, errs);
                }
            }
            catch (Exception ex)
            {
                //Si hay errores es un 500
                errs.Add(new Error()
                {
                    IdError = 1,
                    MensajeTecnico = ex.Message,
                    MensajeUsuario = "Error al procesar su informacion!!, Contactese con el administrador!!",
                    NivelError = "500"
                });
                return StatusCode(500, errs);
            }
        }

    }
}
