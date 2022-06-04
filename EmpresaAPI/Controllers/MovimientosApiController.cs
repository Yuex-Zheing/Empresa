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
    public class MovimientosApiController : ControllerBase
    {

        private readonly Repository.IMovimientosRepos repos;
        public MovimientosApiController(Repository.IMovimientosRepos repos)
        {
            this.repos = repos;
        }

        /// <summary>
        /// Actualizar Movimiento
        /// </summary>
        /// <remarks>Actualizar movimiento</remarks>
        /// <param name="idmovimiento"></param>
        /// <param name="body">SOlicitud de movimiento</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPut]
        [Route("/movimientos/{idmovimiento}")]
        [ValidateModelState]
        [SwaggerOperation("Actualizarmovimiento")]
        [SwaggerResponse(statusCode: 200, type: typeof(Movimiento), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(List<Error>), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(List<Error>), description: "Internal Server Error")]
        public virtual IActionResult Actualizarmovimiento([FromRoute][Required] int? idmovimiento, [FromBody] Movimiento body)
        {
            List<Error> errs = new List<Error>();

            /*Validando errores de ingreso de informacion del usuario*/
            if (idmovimiento == null)
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

                Movimiento? cl = repos.ActualizarMovimiento(idmovimiento, body);

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
        /// Crear Movimiento
        /// </summary>
        /// <remarks>Creacion de Movimientos</remarks>
        /// <param name="body">Devuelve el objeto creado con su ID</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [Route("/movimientos")]
        [ValidateModelState]
        [SwaggerOperation("Crearmovimientos")]
        [SwaggerResponse(statusCode: 200, type: typeof(Movimiento), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(List<Error>), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(List<Error>), description: "Internal Server Error")]
        public virtual IActionResult Crearmovimientos([FromBody] Movimiento body)
        {
            List<Error> errs = new List<Error>();

            /*Validando errores de ingreso de informacion del usuario*/
            if (body.IdCuenta == null)
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

                Movimiento? cl = repos.CrearMovimiento(body);

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
        /// Eliminar Movimiento
        /// </summary>
        /// <remarks>Elimina un movimiento con IdMovimiento</remarks>
        /// <param name="idmovimiento"></param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpDelete]
        [Route("/movimientos/{idmovimiento}")]
        [ValidateModelState]
        [SwaggerOperation("Eliminarmovimiento")]
        [SwaggerResponse(statusCode: 400, type: typeof(List<Error>), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(List<Error>), description: "Internal Server Error")]
        public virtual IActionResult Eliminarmovimiento([FromRoute][Required] int? idmovimiento)
        {
            List<Error> errs = new List<Error>();

            if (idmovimiento == null && idmovimiento <= 0)
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


                if (repos.EliminarMovimientos(idmovimiento ?? 0))
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
        /// Consultar Movimientos
        /// </summary>
        /// <remarks>Consulta de Movimientos</remarks>
        /// <param name="idmovimiento">Identificador de movimiento</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("/movimientos")]
        [ValidateModelState]
        [SwaggerOperation("Obtenermovimientos")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Movimiento>), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(List<Error>), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(List<Error>), description: "Internal Server Error")]
        public virtual IActionResult Obtenermovimientos([FromQuery] int? idmovimiento)
        {
            List<Error> errs = new List<Error>();

            try
            {

                List<Movimiento> cl = repos.ConsultaMovimientos(idmovimiento);

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

        /// <summary>
        /// Consulta Movimientos Por Fecha
        /// </summary>
        /// <remarks>Consulta de Movimientos por fechas</remarks>
        /// <param name="inicio">Fecha de inicio</param>
        /// <param name="fin">Fecha de fin</param>
        /// <param name="idcliente">Identificador del cliente</param>
        /// <response code="200">OK</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet]
        [Route("/movimientos/reporte")]
        [ValidateModelState]
        [SwaggerOperation("Obtenermovimientosreporte")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<MovimientoReporte>), description: "OK")]
        [SwaggerResponse(statusCode: 400, type: typeof(List<Error>), description: "Bad Request")]
        [SwaggerResponse(statusCode: 500, type: typeof(List<Error>), description: "Internal Server Error")]
        public virtual IActionResult Obtenermovimientosreporte([FromQuery][Required()] DateTime? inicio, [FromQuery][Required()] DateTime? fin, [FromQuery][Required()] int? idcliente)
        {
            List<Error> errs = new List<Error>();

            try
            {

                List<MovimientoReporte> cl = repos.ConsultaMovimientosPorRango(inicio, fin, idcliente);

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
