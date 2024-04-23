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
        ///<param name="RifaId">Específica el id de la Rifa seleccionada.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Authorize]
        [Route("api/rifa/obtener-rifa/{RifaId}")]
        public async Task<ActionResult> GetRifa(Int32 RifaId)
        {
            try
            {
                log.Info("Inicio api/rifa/obtener-rifa");

                Rifa oRifa = await _rifaService.GetRifa(RifaId);

                if (oRifa == null)
                {
                    return NoContent();
                }

                log.Info("Fin api/rifa/obtener-rifa");
                
                return Ok(oRifa);
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
        ///<param name="UsuarioId">Específica el id del usuario.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/rifa/obtener-lista-rifa-usuario/{UsuarioId}")]
        public async Task<ActionResult> GetListRifaUsuario(Int32 UsuarioId)
        {
            try
            {
                log.Info("Inicio api/rifa/obtener-lista-rifa-usuario");

                var oListaRifa = await _rifaService.GetListRifaUsuario(UsuarioId);

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
        ///<param name="EstadoId">Específica el id del estado selecconado.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/rifa/obtener-lista-rifa-estado/{EstadoId}")]
        public async Task<ActionResult> GetListRifa(Int32 EstadoId)
        {
            try
            {
                log.Info("Inicio api/rifa/obtener-lista-rifa-estado");

                var oListaRifa = await _rifaService.GetListRifaEstado(EstadoId);

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
        public async Task<ActionResult> InsertRifa(RifaDTO RifaDTO)
        {
            try
            {        
                log.Info("Inicio api/rifa/registro-rifa");

                Rifa oRifa = await _rifaService.InsertRifa(RifaDTO);

                log.Info("Fin api/rifa/registro-rifa");

                return Ok(oRifa); 
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
        public async Task<ActionResult> UpdateRifa(RifaDTO RifaDTO)
        {
            try
            {        
                log.Info("Inicio api/rifa/actualizar-rifa");

                Rifa oRifa = await _rifaService.UpdateRifa(RifaDTO);

                log.Info("Fin api/rifa/actualizar-rifa");

                return Ok(oRifa); 
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
        public async Task<ActionResult> DeleteRifa(Int32 RifaId)
        {
            try
            {        
                log.Info("Inicio api/rifa/eliminar-rifa");

                var oRifa = await _rifaService.DeleteRifa(RifaId);

                log.Info("Fin api/rifa/eliminar-rifa");

                return Ok(oRifa); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }
    }

}
