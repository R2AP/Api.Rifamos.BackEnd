using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Microsoft.AspNetCore.Mvc;
using log4net;
using Microsoft.AspNetCore.Authorization;

namespace Api.Rifamos.BackEnd.Controllers{

    [ApiController]
    // [Authorize]
    public class RifaController : ControllerBase{

        private readonly IRifaService _rifaService;
        private static readonly ILog log = LogManager.GetLogger(typeof(RifaController));

        public RifaController(IRifaService rifaService)
        {
            _rifaService = rifaService;

            log4net.GlobalContext.Properties["fDirectory"] = AppContext.BaseDirectory;
            Logger.InicializarLog();
        }

        //GET: api/rifa/obtener-rifa
        ///<summary>
        ///Obtener un registro con la rifa seleccionada.
        ///</summary>
        ///<param name="oRifaId">Específica el id de la Rifa seleccionada.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/rifa/obtener-rifa/{oRifaId}")]
        public async Task<ActionResult> GetRifa(Int32 oRifaId)
        {
            try
            {
                log.Info("Inicio api/rifa/obtener-rifa");

                RifaFrontDTO oRifaFrontDTO = await _rifaService.GetRifa(oRifaId);

                if (oRifaFrontDTO == null)
                {
                    return NotFound();
                }

                log.Info("Fin api/rifa/obtener-rifa");
                
                return Ok(oRifaFrontDTO);
            }
            catch(Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }        

        //GET: api/rifa/obtener-lista-rifa-usuario
        ///<summary>
        ///Obtener una lista de rifas de un usuario especifico
        ///</summary>
        ///<param name="oUsuarioId">Específica el id del usuario.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/rifa/obtener-lista-rifa-usuario/{oUsuarioId}")]
        public async Task<ActionResult> GetListRifaUsuario(Int32 oUsuarioId)
        {
            try
            {
                log.Info("Inicio api/rifa/obtener-lista-rifa-usuario");

                List<RifaFrontDTO> oListaRifa = await _rifaService.GetListRifaUsuario(oUsuarioId);

                if (oListaRifa == null)
                {
                    return NoContent();
                }

                log.Info("Fin api/rifa/obtener-lista-rifa-usuario");
                
                return Ok(oListaRifa);
            }
            catch(Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //GET: api/rifa/obtener-lista-rifa-estado
        ///<summary>
        ///Obtener una lista de rifas por estado
        ///</summary>
        ///<param name="oEstadoId">Específica el id del estado selecconado.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/rifa/obtener-lista-rifa-estado/{oEstadoId}")]
        public async Task<ActionResult> GetListRifa(Int32 oEstadoId)
        {
            try
            {
                log.Info("Inicio api/rifa/obtener-lista-rifa-estado");

                List<RifaFrontDTO> oListaRifa = await _rifaService.GetListRifaEstado(oEstadoId);

                if (oListaRifa == null)
                {
                    return NoContent();
                }

                log.Info("Fin api/rifa/obtener-lista-rifa-estado");

                return Ok(oListaRifa);
            }
            catch(Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //POST: api/rifa/registro-rifa
        /// <summary>
        /// Crear una nueva Rifa.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPost("api/rifa/registro-rifa")]
        public async Task<ActionResult> InsertRifa(RifaDTO oRifaDTO)
        {
            try
            {        
                log.Info("Inicio api/rifa/registro-rifa");

                RifaFrontDTO oRifaFrontDTO = await _rifaService.InsertRifa(oRifaDTO);

                log.Info("Fin api/rifa/registro-rifa");

                return Ok(oRifaFrontDTO); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //PUT: api/rifa/actualizar-rifa
        /// <summary>
        /// Actualizar un registro Rifa.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPut("api/rifa/actualizar-rifa")]
        public async Task<ActionResult> UpdateRifa(RifaDTO oRifaDTO)
        {
            try
            {        
                log.Info("Inicio api/rifa/actualizar-rifa");

                RifaFrontDTO oRifaFrontDTO = await _rifaService.UpdateRifa(oRifaDTO);

                log.Info("Fin api/rifa/actualizar-rifa");

                return Ok(oRifaFrontDTO); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //DELETE: api/rifa/eliminar-rifa
        /// <summary>
        /// Eliminar un registro de Rifa.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpDelete("api/rifa/eliminar-rifa")]
        public async Task<ActionResult> DeleteRifa(Int32 oRifaId)
        {
            try
            {        
                log.Info("Inicio api/rifa/eliminar-rifa");

                RifaFrontDTO oRifaFrontDTO = await _rifaService.DeleteRifa(oRifaId);

                log.Info("Fin api/rifa/eliminar-rifa");

                return Ok(oRifaFrontDTO); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }
    }

}
