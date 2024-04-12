using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Microsoft.AspNetCore.Mvc;
using log4net;

namespace Api.Rifamos.BackEnd.Controllers{

    [ApiController]
    public class SesionController : ControllerBase{

        private readonly ISesionService _sesionService;
        private static readonly ILog log = LogManager.GetLogger(typeof(RifaController));

        public SesionController(ISesionService sesionService)
        {
            _sesionService = sesionService;

            log4net.GlobalContext.Properties["fDirectory"] = AppContext.BaseDirectory;
            Logger.InicializarLog();
        }

        //GET: api/sesion/obtener-sesion
        ///<summary>
        ///Obtener una sesion de la opción comprada
        ///</summary>
        ///<param name="SesionId">Específica el id de la Rifa seleccionada.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/sesion/obtener-sesion/{SesionId}")]
        public async Task<ActionResult> GetSesion(Int32 SesionId)
        {
            try
            {
                log.Info("Inicio api/sesion/obtener-sesion");

                var oSesion = await _sesionService.GetSesion(SesionId);

                if (oSesion == null)
                {
                    return NoContent();
                }

                log.Info("Fin api/sesion/obtener-sesion");

                return Ok(oSesion);
            }
            catch(Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }        

        //GET: api/obtener-lista-rifa-estado
        ///<summary>
        ///Obtener una lista de rifas por estado
        ///</summary>
        ///<param name="EstadoId">Específica el id del estado selecconado.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        // [HttpGet]
        // [Route("api/rifa/obtener-lista-rifa-estado/{EstadoId}")]
        // public async Task<ActionResult> GetListRifa(Int32 EstadoId)
        // {
        //     try
        //     {
        //         //log.Info("Inicio EndosoController/GetListaEndosos");

        //         var listaRifa = await _rifaService.GetListRifaEstado(EstadoId);

        //         if (listaRifa == null)
        //         {
        //             return NoContent();
        //         }

        //         //log.Info("Fin EndosoController/GetListaEndosos");
        //         return Ok(listaRifa);
        //     }
        //     catch(Exception ex)
        //     {
        //         //log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
        //         return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
        //     }
        // }

        //POST: api/sesion/registro-sesion
        /// <summary>
        /// Crear una nueva Sesion.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPost("api/sesion/registro-sesion")]
        public async Task<ActionResult> InsertSesion(SesionDTO SesionDTO)
        {
            try
            {        
                log.Info("Inicio api/sesion/registro-sesion");

                var respuesta = await _sesionService.InsertSesion(SesionDTO);

                log.Info("Fin api/sesion/registro-sesion");

                return Ok(respuesta); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //PUT: api/sesion/actualizar-sesion
        /// <summary>
        /// Actualizar un registro de sesion.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPut("api/sesion/actualizar-sesion")]
        public async Task<ActionResult> UpdateSesion(SesionDTO SesionDTO)
        {
            try
            {        
                log.Info("Inicio api/sesion/actualizar-sesion");

                var respuesta = await _sesionService.UpdateSesion(SesionDTO);

                log.Info("Fin api/sesion/actualizar-sesion");

                return Ok(respuesta); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //DELETE: api/sesion/eliminar-sesion
        /// <summary>
        /// Eliminar un registro de sesion.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpDelete("api/sesion/eliminar-sesion")]
        public async Task<ActionResult> DeleteSesion(Int32 SesionId)
        {
            try
            {        
                log.Info("Inicio api/sesion/eliminar-sesion");

                var respuesta = await _sesionService.DeleteSesion(SesionId);

                log.Info("Fin api/sesion/eliminar-sesion");

                return Ok(respuesta); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }
    }

}