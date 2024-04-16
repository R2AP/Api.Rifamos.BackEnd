using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Microsoft.AspNetCore.Mvc;
using log4net;

namespace Api.Rifamos.BackEnd.Controllers{

    [ApiController]
    public class QRController : ControllerBase{

        private readonly IQRService _QRService;
        private static readonly ILog log = LogManager.GetLogger(typeof(RifaController));

        public QRController(IQRService QRService)
        {
            _QRService = QRService;

            log4net.GlobalContext.Properties["fDirectory"] = AppContext.BaseDirectory;
            Logger.InicializarLog();
        }

        //GET: api/QR/obtener-QR
        ///<summary>
        ///Obtener QR de un texto específico
        ///</summary>
        ///<param name="QRId">Específica el id de la Rifa seleccionada.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/QR/obtener-QR/{QRId}")]
        public ActionResult GetQR(string QRId)
        {
            try
            {
                log.Info("Inicio api/QR/obtener-QR");

                var oQR = _QRService.GetQR(QRId);

                if (oQR == null)
                {
                    return NoContent();
                }

                log.Info("Fin api/venta/obtener-venta");

                return Ok(oQR);

            }
            catch(Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }
    }
}