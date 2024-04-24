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

        //GET: api/pago/obtener-pago
        ///<summary>
        ///Obtener una pago de la opción comprada
        ///</summary>
        ///<param name="PagoId">Específica el id de la Rifa seleccionada.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        // [HttpGet]
        // [Route("api/pago/obtener-pago/{PagoId}")]
        // public async Task<ActionResult> GetPago(Int32 PagoId)
        // {
        //     try
        //     {
        //         log.Info("Inicio api/pago/obtener-pago");

        //         var oPago = await _pagoService.GetPago(PagoId);

        //         if (oPago == null)
        //         {
        //             return NoContent();
        //         }

        //         log.Info("Fin api/pago/obtener-pago");
        //         return Ok(oPago);
        //     }
        //     catch(Exception ex)
        //     {
        //         log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
        //         return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
        //     }
        // }

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
                log.Info("Inicio api/pago/registro-pago");

                var oPago = await _pagoService.InsertPago(PagoDTO);

                log.Info("Fin api/pago/registro-pago");

                return Ok(oPago); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //PUT: api/pago/actualizar-pago
        /// <summary>
        /// Actualizar un registro de pago.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        // [HttpPut("api/pago/actualizar-pago")]
        // public async Task<ActionResult> UpdatePago(PagoDTO PagoDTO)
        // {
        //     try
        //     {        
        //         log.Info("Inicio api/pago/actualizar-pago");

        //         var oPago = await _pagoService.UpdatePago(PagoDTO);

        //         log.Info("Fin api/pago/actualizar-pago");

        //         return Ok(oPago); 
        //     }
        //     catch (Exception ex)
        //     {
        //         log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
        //         return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
        //     }
        // }

        //DELETE: api/pago/eliminar-pago
        /// <summary>
        /// Eliminar un registro de pago.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        // [HttpDelete("api/pago/eliminar-pago")]
        // public async Task<ActionResult> DeletePago(Int32 PagoId)
        // {
        //     try
        //     {        
        //         log.Info("Inicio api/pago/eliminar-pago");

        //         var oPago = await _pagoService.DeletePago(PagoId);

        //         log.Info("Fin api/pago/eliminar-pago");

        //         return Ok(oPago); 
        //     }
        //     catch (Exception ex)
        //     {
        //         log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
        //         return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
        //     }
        // }
    }

}
