using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Microsoft.AspNetCore.Mvc;
using log4net;

namespace Api.Rifamos.BackEnd.Controllers{

    [ApiController]
    public class VentaController : ControllerBase{

        private readonly IVentaService _ventaService;
        private static readonly ILog log = LogManager.GetLogger(typeof(RifaController));

        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;

            log4net.GlobalContext.Properties["fDirectory"] = AppContext.BaseDirectory;
            Logger.InicializarLog();
        }

        //GET: api/venta/obtener-venta
        ///<summary>
        ///Obtener una venta de la opción comprada
        ///</summary>
        ///<param name="oVentaId">Específica el id de la Rifa seleccionada.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/venta/obtener-venta/{VentaId}")]
        public async Task<ActionResult> GetVenta(Int32 oVentaId)
        {
            try
            {
                log.Info("Inicio api/venta/obtener-venta");

                VentaFrontDTO oVentaFrontDTO = await _ventaService.GetVentaOpcion(oVentaId);

                if (oVentaFrontDTO == null)
                {
                    return NoContent();
                }

                log.Info("Fin api/venta/obtener-venta");

                return Ok(oVentaFrontDTO);
            }
            catch(Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }        

        //POST: api/venta/registro-venta
        /// <summary>
        /// Crear una nueva Venta.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPost("api/venta/registro-venta")]
        public async Task<ActionResult> InsertVenta(VentaDTO oVentaDTO)
        {
            try
            {        
                log.Info("Inicio api/venta/registro-venta");

                VentaFrontDTO oVentaFrontDTO = await _ventaService.InsertVentaOpcion(oVentaDTO);

                log.Info("Fin api/venta/registro-venta");

                return Ok(oVentaFrontDTO); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

    }

}