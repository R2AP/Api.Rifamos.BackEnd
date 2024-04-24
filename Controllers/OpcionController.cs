using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Microsoft.AspNetCore.Mvc;
using log4net;
using Microsoft.AspNetCore.Authorization;

namespace Api.Rifamos.BackEnd.Controllers{
    [ApiController]
    public class OpcionController : ControllerBase{

        private readonly IOpcionService _opcionService;
        private static readonly ILog log = LogManager.GetLogger(typeof(RifaController));

        public OpcionController(IOpcionService opcionService)
        {
            _opcionService = opcionService;

            log4net.GlobalContext.Properties["fDirectory"] = AppContext.BaseDirectory;
            Logger.InicializarLog();
        }

        //GET: api/opcion/obtener-lista-opcion
        ///<summary>
        ///Obtener una lista de opciones compradas por un clientes en una rifa especifica
        ///</summary>
        ///<param name="RifaId">Específica el id de la rifa.</param>
        ///<param name="UsuarioId">Específica el id del usuario.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Authorize]
        [Route("api/opcion/obtener-lista-opcion/{RifaId}/{UsuarioId}")]
        public async Task<ActionResult> GetListOpcion(Int32 RifaId, Int32 UsuarioId)
        {
            try
            {
                log.Info("Inicio api/opcion/obtener-lista-opcion");

                var oListaOpcion = await _opcionService.GetListOpcion(RifaId, UsuarioId);

                if (oListaOpcion == null)
                {
                    return NoContent();
                }

                log.Info("Fin api/opcion/obtener-lista-opcion");
                
                return Ok(oListaOpcion);
            }
            catch(Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }  

        //GET: api/opcion/obtener-opcion
        ///<summary>
        ///Obtener una opción comprada por un cliente
        ///</summary>
        ///<param name="TokenOpcion">Específica el token de una opción.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Authorize]
        [Route("api/opcion/obtener-opcion/{TokenOpcion}")]
        public async Task<ActionResult> GetOpcion(string TokenOpcion)
        {
            try
            {
                log.Info("Inicio api/opcion/obtener-opcion");

                var oOpcion = await _opcionService.GetOpcionToken(TokenOpcion);

                if (oOpcion == null)
                {
                    return NoContent();
                }

                log.Info("Fin api/opcion/obtener-opcion");

                return Ok(oOpcion);
            }
            catch(Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }  

        //POST: api/opcion/registro-opcion
        /// <summary>
        /// Crear una opción en la Rifa .
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        //[Authorize]
        // [HttpPost("api/opcion/registro-opcion")]
        // public async Task<ActionResult> InsertOpcion(OpcionDTO OpcionDTO)
        // {
        //     try
        //     {        
        //         log.Info("Inicio api/opcion/registro-opcion");

        //         var oOpcion = await _opcionService.InsertOpcion(OpcionDTO);

        //         log.Info("Fin api/opcion/registro-opcion");

        //         return Ok(oOpcion); 
        //     }
        //     catch (Exception ex)
        //     {
        //         log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
        //         return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
        //     }
        // }

        //PUT: api/opcion/actualizar-opcion
        /// <summary>
        /// Actualizar una opción en la Rifa .
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        // [Authorize]
        // [HttpPut("api/opcion/actualizar-opcion")]
        // public async Task<ActionResult> UpdateOpcion(OpcionDTO OpcionDTO)
        // {
        //     try
        //     {        
        //         log.Info("Inicio api/opcion/actualizar-opcion");

        //         var respuesta = await _opcionService.UpdateOpcion(OpcionDTO);

        //         log.Info("Fin api/opcion/actualizar-opcion");

        //         return Ok(respuesta); 
        //     }
        //     catch (Exception ex)
        //     {
        //         log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
        //         return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
        //     }
        // }

        //DELETE: api/opcion/eliminar-opcion
        /// <summary>
        /// Elimina una opción de la Rifa .
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        // [Authorize]
        // [HttpDelete("api/opcion/eliminar-opcion")]
        // public async Task<ActionResult> DeleteOpcion(Int32 OpcionId)
        // {
        //     try
        //     {        
        //         log.Info("Inicio api/opcion/eliminar-opcion");

        //         var respuesta = await _opcionService.DeleteOpcion(OpcionId);

        //         log.Info("Fin api/opcion/eliminar-opcion");

        //         return Ok(respuesta); 
        //     }
        //     catch (Exception ex)
        //     {
        //         log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
        //         return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
        //     }
        // }

    }
}