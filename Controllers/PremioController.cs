using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Microsoft.AspNetCore.Mvc;
//using log4net;

namespace Api.Rifamos.BackEnd.Controllers{
    [ApiController]
    public class PremioController : ControllerBase{

        private readonly IPremioService _premioService;
        //private static readonly ILog log = LogManager.GetLogger(typeof(RifaController));

        public PremioController(IPremioService premioService)
        {
            _premioService = premioService;

            //log4net.GlobalContext.Properties["fDirectory"] = AppContext.BaseDirectory;
            //Logger.InicializarLog();
        }

        //GET: api/obtener-lista-premio
        ///<summary>
        ///Obtener una lista de premios de una rifa especifica
        ///</summary>
        ///<param name="RifaId">Específica el id de la rifa.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/premio/obtener-lista-premio/{RifaId}")]
        public async Task<ActionResult> GetListPremio(Int32 RifaId)
        {
            try
            {
                //log.Info("Inicio EndosoController/GetListaEndosos");

                var listaPremio = await _premioService.GetListPremio(RifaId);

                if (listaPremio == null)
                {
                    return NoContent();
                }

                //log.Info("Fin EndosoController/GetListaEndosos");
                return Ok(listaPremio);
            }
            catch(Exception ex)
            {
                //log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //POST: api/premio/registro-premio
        /// <summary>
        /// Crear una los premios de la Rifa .
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPost("api/premio/registro-premio")]
        public async Task<ActionResult> InsertPremio(Premio Premio)
        {
            try
            {        
                //log.Info("Inicio EndosoController/registrarEndosoApoderadoPago");

                var respuesta = await _premioService.InsertPremio(Premio);

                //log.Info("Fin EndosoController/registrarEndosoApoderadoPago");

                return Ok(respuesta); 
            }
            catch (Exception ex)
            {
                //log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }        

    }
}