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
        ///<param name="oRifaId">Específica el id de la rifa.</param>
        ///<param name="oUsuarioId">Específica el id del usuario.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Authorize]
        [Route("api/opcion/obtener-lista-opcion/{oRifaId}/{oUsuarioId}")]
        public async Task<ActionResult> GetListOpcion(Int32 oRifaId, Int32 oUsuarioId)
        {
            try
            {
                //log.Info("Inicio api/opcion/obtener-lista-opcion");

                var oListaOpcion = await _opcionService.GetListOpcion(oRifaId, oUsuarioId);

                if (oListaOpcion == null)
                {
                    return NoContent();
                }

                if (oListaOpcion[0].Error == true)
                {
                    return BadRequest(oListaOpcion);
                }                

                //log.Info("Fin api/opcion/obtener-lista-opcion");
                
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
        ///<param name="oTokenOpcion">Específica el token de una opción.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Authorize]
        [Route("api/opcion/obtener-opcion/{oTokenOpcion}")]
        public async Task<ActionResult> GetOpcion(string oTokenOpcion)
        {
            try
            {
                //log.Info("Inicio api/opcion/obtener-opcion");

                var oOpcion = await _opcionService.GetOpcionToken(oTokenOpcion);

                if (oOpcion == null)
                {
                    return NoContent();
                }

                if (oOpcion.Error == true)
                {
                    return BadRequest(oOpcion);
                }

                //log.Info("Fin api/opcion/obtener-opcion");

                return Ok(oOpcion);
            }
            catch(Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }  

    }
}