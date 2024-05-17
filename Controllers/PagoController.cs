using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Microsoft.AspNetCore.Mvc;
using log4net;

namespace Api.Rifamos.BackEnd.Controllers{

    [ApiController]
    public class PagoController : ControllerBase{

        private readonly IPagoService _pagoService;
        private static readonly ILog log = LogManager.GetLogger(typeof(RifaController));

        public PagoController(IPagoService pagoService)
        {
            _pagoService = pagoService;

            log4net.GlobalContext.Properties["fDirectory"] = AppContext.BaseDirectory;
            Logger.InicializarLog();
        }

        //POST: api/pago/registro-pago
        /// <summary>
        /// Crear un nuevo Pago.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPost("api/pago/registro-pago")]
        public async Task<ActionResult> InsertPago(PagoDTO PagoDTO)
        {
            try
            {        
                //log.Info("Inicio api/pago/registro-pago");

                var oPago = await _pagoService.InsertPago(PagoDTO);

                //log.Info("Fin api/pago/registro-pago");

                return Ok(oPago); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }
    }
}
