using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Microsoft.AspNetCore.Mvc;
using log4net;

namespace Api.Rifamos.BackEnd.Controllers{
    [ApiController]
    public class PremioController : ControllerBase{

        private readonly IPremioService _premioService;
        private static readonly ILog log = LogManager.GetLogger(typeof(RifaController));

        public PremioController(IPremioService premioService)
        {
            _premioService = premioService;

            log4net.GlobalContext.Properties["fDirectory"] = AppContext.BaseDirectory;
            Logger.InicializarLog();
        }

        //GET: api/premio/obtener-lista-premio
        ///<summary>
        ///Obtener una lista de premios de una rifa especifica
        ///</summary>
        ///<param name="oRifaId">Específica el id de la rifa.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/premio/obtener-lista-premio/{oRifaId}")]
        public async Task<ActionResult> GetListPremio(Int32 oRifaId)
        {
            try
            {
                //log.Info("Inicio api/premio/obtener-lista-premio");

                var oListaPremio = await _premioService.GetListPremio(oRifaId);

                if (oListaPremio == null)
                {
                    return NoContent();
                }

                //log.Info("Fin api/premio/obtener-lista-premio");
                return Ok(oListaPremio);
            }
            catch(Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //POST: api/premio/registro-premio
        /// <summary>
        /// Crear una los premios de la Rifa .
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPost("api/premio/registro-premio")]
        public async Task<ActionResult> InsertPremio(PremioDTO oPremioDTO)
        {
            try
            {        
                //log.Info("Inicio api/premio/registro-premio");

                var oPremio = await _premioService.InsertPremio(oPremioDTO);

                //log.Info("Fin api/premio/registro-premio");

                return Ok(oPremio); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //PUT: api/premio/actualizar-premio
        /// <summary>
        /// Actualizar uno de los registros de premio de la Rifa .
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPut("api/premio/actualizar-premio")]
        public async Task<ActionResult> UpdatePremio(PremioDTO oPremioDTO)
        {
            try
            {        
                //log.Info("Inicio api/premio/actualizar-premio");

                var oPremio = await _premioService.UpdatePremio(oPremioDTO);

                //log.Info("Fin api/premio/actualizar-premio");

                return Ok(oPremio); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //DELETE: api/premio/eliminar-premio
        /// <summary>
        /// Eliminar uno de los premios de la Rifa .
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpDelete("api/premio/eliminar-premio")]
        public async Task<ActionResult> DeletePremio(Int32 oPremioId)
        {
            try
            {        
                //log.Info("Inicio api/premio/eliminar-premio");

                var oPremio = await _premioService.DeletePremio(oPremioId);

                //log.Info("Fin api/premio/eliminar-premio");

                return Ok(oPremio); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

    }
}