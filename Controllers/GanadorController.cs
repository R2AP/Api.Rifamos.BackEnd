using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Microsoft.AspNetCore.Mvc;
using log4net;
using Microsoft.AspNetCore.Authorization;

namespace Api.Rifamos.BackEnd.Controllers{

    [ApiController]
    public class GanadorController : ControllerBase{

        private readonly IGanadorService _GanadorService;
        private static readonly ILog log = LogManager.GetLogger(typeof(GanadorController));

        public GanadorController(IGanadorService GanadorService)
        {
            _GanadorService = GanadorService;

            log4net.GlobalContext.Properties["fDirectory"] = AppContext.BaseDirectory;
            Logger.InicializarLog();
        }

        //POST: api/Ganador/registro-Ganador
        /// <summary>
        /// Crear un nuevo Ganador.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPost("api/Ganador/registro-Ganador")]
        //public async Task<ActionResult> InsertGanador(Ganador Ganador, string Password)
        public async Task<ActionResult> InsertGanador(GanadorDTO oGanadorDTO)
        {
            try
            {        
                //log.Info("Inicio api/Ganador/registro-Ganador");

                GanadorFrontDTO oGanadorFrontDTO = await _GanadorService.InsertGanador(oGanadorDTO);
                if (oGanadorFrontDTO.Error)
                {
                    return BadRequest(oGanadorFrontDTO);
                }

                //log.Info("Fin api/Ganador/registro-Ganador");

                return Ok(oGanadorFrontDTO); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //PUT: api/Ganador/actualizar-Ganador
        /// <summary>
        /// Actualizar un registro de Ganador.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPut("api/Ganador/actualizar-Ganador")]
        public async Task<ActionResult> UpdateGanador(GanadorDTO oGanadorDTO)
        {
            try
            {        
                //log.Info("Inicio api/Ganador/actualizar-Ganador");

                var respuesta = await _GanadorService.UpdateGanador(oGanadorDTO);

                //log.Info("Fin api/Ganador/actualizar-Ganador");

                return Ok(respuesta); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //DELETE: api/Ganador/eliminar-Ganador
        /// <summary>
        /// Eliminar un registro de Ganador.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpDelete("api/Ganador/eliminar-Ganador")]
        public async Task<ActionResult> DeleteGanador(Int32 oGanadorId)
        {
            try
            {        
                //log.Info("Inicio api/Ganador/eliminar-Ganador");

                var respuesta = await _GanadorService.DeleteGanador(oGanadorId);

                //log.Info("Fin api/Ganador/eliminar-Ganador");

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