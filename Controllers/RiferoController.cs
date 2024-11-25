using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Microsoft.AspNetCore.Mvc;
using log4net;
using Microsoft.AspNetCore.Authorization;

namespace Api.Rifamos.BackEnd.Controllers{

    [ApiController]
    public class RiferoController : ControllerBase{

        private readonly IRiferoService _RiferoService;
        private static readonly ILog log = LogManager.GetLogger(typeof(RiferoController));

        public RiferoController(IRiferoService RiferoService)
        {
            _RiferoService = RiferoService;

            log4net.GlobalContext.Properties["fDirectory"] = AppContext.BaseDirectory;
            Logger.InicializarLog();
        }

        //POST: api/Rifero/registro-Rifero
        /// <summary>
        /// Crear un nuevo Rifero.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPost("api/Rifero/registro-Rifero")]
        //public async Task<ActionResult> InsertRifero(Rifero Rifero, string Password)
        public async Task<ActionResult> InsertRifero(RiferoDTO oRiferoDTO)
        {
            try
            {        
                //log.Info("Inicio api/Rifero/registro-Rifero");

                RiferoFrontDTO oRiferoFrontDTO = await _RiferoService.InsertRifero(oRiferoDTO);
                if (oRiferoFrontDTO.Error)
                {
                    return BadRequest(oRiferoFrontDTO);
                }

                //log.Info("Fin api/Rifero/registro-Rifero");

                return Ok(oRiferoFrontDTO); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //PUT: api/Rifero/actualizar-Rifero
        /// <summary>
        /// Actualizar un registro de Rifero.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPut("api/Rifero/actualizar-Rifero")]
        public async Task<ActionResult> UpdateRifero(RiferoDTO oRiferoDTO)
        {
            try
            {        
                //log.Info("Inicio api/Rifero/actualizar-Rifero");

                var respuesta = await _RiferoService.UpdateRifero(oRiferoDTO);

                //log.Info("Fin api/Rifero/actualizar-Rifero");

                return Ok(respuesta); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //DELETE: api/Rifero/eliminar-Rifero
        /// <summary>
        /// Eliminar un registro de Rifero.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpDelete("api/Rifero/eliminar-Rifero")]
        public async Task<ActionResult> DeleteRifero(Int32 oRiferoId)
        {
            try
            {        
                //log.Info("Inicio api/Rifero/eliminar-Rifero");

                var respuesta = await _RiferoService.DeleteRifero(oRiferoId);

                //log.Info("Fin api/Rifero/eliminar-Rifero");

                return Ok(respuesta); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //PUT: actualizar-password-Rifero
        /// <summary>
        /// Actualizar el password del Rifero.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPut("api/Rifero/actualizar-password-Rifero")]
        public async Task<ActionResult> UpdatePasswordRifero(RiferoPasswordDTO oRiferoPasswordDTO)
        {
            try
            {        
                //log.Info("Inicio api/Rifero/actualizar-password-Rifero");

                var respuesta = await _RiferoService.UpdatePasswordRifero(oRiferoPasswordDTO);

                //log.Info("Fin api/Rifero/actualizar-password-Rifero");

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //PUT: recuperar-password
        /// <summary>
        /// Recuperar password
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPut("api/Rifero/recuperar-password")]
        public async Task<ActionResult> RecuperarPassword(string oEmail)
        {
            try
            {        
                //log.Info("Inicio api/Rifero/recuperar-password");

                RiferoFrontDTO oRiferoFrontDTO  = await _RiferoService.RecuperarPassword(oEmail);

                if (oRiferoFrontDTO.Error)
                {
                    return BadRequest(oRiferoFrontDTO);
                }

                //log.Info("Fin api/Rifero/recuperar-password");

                return Ok(oRiferoFrontDTO);
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }        

    }
}