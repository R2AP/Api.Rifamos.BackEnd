using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using log4net;

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

        //GET: api/obtener-lista-opcion
        ///<summary>
        ///Obtener una lista de opciones compradas por un clientes en una rifa especifica
        ///</summary>
        ///<param name="RifaId">Específica el id de la rifa.</param>
        ///<param name="UsuarioId">Específica el id del usuario.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/opcion/obtener-lista-opcion/{RifaId}/{UsuarioId}")]
        public async Task<ActionResult> GetListOpcion(Int32 RifaId, Int32 UsuarioId)
        {
            try
            {
                log.Info("Inicio opcion/obtener-lista-opcion");

                var listaOpcion = await _opcionService.GetListOpcion(RifaId, UsuarioId);

                if (listaOpcion == null)
                {
                    return NoContent();
                }

                log.Info("Fin opcion/obtener-lista-opcion");
                return Ok(listaOpcion);
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
        [HttpPost("api/opcion/registro-opcion")]
        public async Task<ActionResult> InsertOpcion(Opcion Opcion)
        {
            try
            {        
                log.Info("Inicio opcion/registro-opcion");

                var respuesta = await _opcionService.InsertOpcion(Opcion);

                log.Info("Fin opcion/registro-opcion");

                return Ok(respuesta); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //PUT: api/opcion/actualizar-opcion
        /// <summary>
        /// Actualizar una opción en la Rifa .
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPut("api/opcion/actualizar-opcion")]
        public async Task<ActionResult> UpdateOpcion(Opcion Opcion)
        {
            try
            {        
                log.Info("Inicio EndosoController/registrarEndosoApoderadoPago");

                var respuesta = await _opcionService.UpdateOpcion(Opcion);

                log.Info("Fin EndosoController/registrarEndosoApoderadoPago");

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