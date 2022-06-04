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
    public class CuentasApiController : ControllerBase
    {
        private readonly Repository.ICuentasRepos repos;
        public CuentasApiController(Repository.ICuentasRepos repos)
        {
            this.repos = repos;
        }

        /// <summary>
        /// Actualizar Cuenta
        /// </summary>
        /// <remarks>Actualizar Cuenta</remarks>
        /// <param name="idcuenta">Identificador de la cuenta</param>
        /// <param name="body">Objeto a actualizar</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("/cuentas/{idcuenta}")]
        [ValidateModelState]
        [SwaggerOperation("Actualizarcuenta")]
        [SwaggerResponse(statusCode: 200, type: typeof(Cuenta), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(List<Error>), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(List<Error>), description: "Internal Server Error")]
        public virtual IActionResult Actualizarcuenta([FromRoute][Required] int? idcuenta, [FromBody] Cuenta body)
        {
            List<Error> errs = new List<Error>();

            /*Validando errores de ingreso de informacion del usuario*/
            if (idcuenta == null)
            {
                errs.Add(new Error()
                {
                    IdError = 1,
                    MensajeTecnico = "Debe Ingresar un identificador",
                    MensajeUsuario = "Debe Ingresar un identificador",
                    NivelError = "401"
                });
            }            

            if (errs.Count > 0)
                return StatusCode(400, errs);

            try
            {

                Cuenta? cl = repos.ActualizarCuenta(idcuenta, body);

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
        /// Crear Cuenta
        /// </summary>
        /// <remarks>Creacion de cuentas</remarks>
        /// <param name="body">Devuelve el objeto creado con su ID</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("/cuentas")]
        [ValidateModelState]
        [SwaggerOperation("Crearcuenta")]
        [SwaggerResponse(statusCode: 200, type: typeof(Cuenta), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(List<Error>), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(List<Error>), description: "Internal Server Error")]
        public virtual IActionResult Crearcuenta([FromBody] Cuenta body)
        {
            List<Error> errs = new List<Error>();

            /*Validando errores de ingreso de informacion del usuario*/
            if ( body.IdCliente == null)
            {
                errs.Add(new Error()
                {
                    IdError = 1,
                    MensajeTecnico = "Debe ingresar una cuenta valida!!",
                    MensajeUsuario = "Debe ingresar una cuenta valida!!",
                    NivelError = "401"
                });
            }

            

            if (errs.Count > 0)
                return StatusCode(400, errs);

            try
            {

                Cuenta? cl = repos.CrearCuenta(body);

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
        /// Eliminar Cuenta
        /// </summary>
        /// <remarks>Elimina cuenta</remarks>
        /// <param name="idcuenta">Identificador de la cuenta</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("/cuentas/{idcuenta}")]
        [ValidateModelState]
        [SwaggerOperation("Eliminarcuenta")]
        [SwaggerResponse(statusCode: 400, type: typeof(List<Error>), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(List<Error>), description: "Internal Server Error")]
        public virtual IActionResult Eliminarcuenta([FromRoute][Required] int? idcuenta)
        {
            List<Error> errs = new List<Error>();

            if (idcuenta == null && idcuenta <= 0)
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


                if (repos.EliminarCuenta(idcuenta ?? 0))
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
        /// Consultar Cuentas
        /// </summary>
        /// <remarks>Consulta de cuentas</remarks>
        /// <param name="idcuenta">Identificador de cuenta</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("/cuentas")]
        [ValidateModelState]
        [SwaggerOperation("Obtenercuenta")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Cuenta>), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(List<Error>), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(List<Error>), description: "Internal Server Error")]
        public virtual IActionResult Obtenercuenta([FromQuery] int? idcuenta)
        {
            List<Error> errs = new List<Error>();

            try
            {

                List<Cuenta> cl = repos.ConsultaCuenta(idcuenta);

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
