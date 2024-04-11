using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Microsoft.AspNetCore.Mvc;
//using log4net;

namespace Api.Rifamos.BackEnd.Controllers{

    [ApiController]
    public class PagoController : ControllerBase{

        private readonly IPagoService _pagoService;
        //private static readonly ILog log = LogManager.GetLogger(typeof(RifaController));

        public PagoController(IPagoService pagoService)
        {
            _pagoService = pagoService;

            //log4net.GlobalContext.Properties["fDirectory"] = AppContext.BaseDirectory;
            //Logger.InicializarLog();
        }

        //GET: api/obtener-pago
        ///<summary>
        ///Obtener una pago de la opción comprada
        ///</summary>
        ///<param name="PagoId">Específica el id de la Rifa seleccionada.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/pago/obtener-pago/{PagoId}")]
        public async Task<ActionResult> GetPago(Int32 PagoId)
        {
            try
            {
                //log.Info("Inicio EndosoController/GetListaEndosos");

                var pago = await _pagoService.GetPago(PagoId);

                if (pago == null)
                {
                    return NoContent();
                }

                //log.Info("Fin EndosoController/GetListaEndosos");
                return Ok(pago);
            }
            catch(Exception ex)
            {
                //log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }        

        //GET: api/obtener-lista-rifa-estado
        ///<summary>
        ///Obtener una lista de rifas por estado
        ///</summary>
        ///<param name="EstadoId">Específica el id del estado selecconado.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        // [HttpGet]
        // [Route("api/rifa/obtener-lista-rifa-estado/{EstadoId}")]
        // public async Task<ActionResult> GetListRifa(Int32 EstadoId)
        // {
        //     try
        //     {
        //         //log.Info("Inicio EndosoController/GetListaEndosos");

        //         var listaRifa = await _rifaService.GetListRifaEstado(EstadoId);

        //         if (listaRifa == null)
        //         {
        //             return NoContent();
        //         }

        //         //log.Info("Fin EndosoController/GetListaEndosos");
        //         return Ok(listaRifa);
        //     }
        //     catch(Exception ex)
        //     {
        //         //log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
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
                //log.Info("Inicio EndosoController/registrarEndosoApoderadoPago");

                var respuesta = await _pagoService.InsertPago(PagoDTO);

                //log.Info("Fin EndosoController/registrarEndosoApoderadoPago");

                return Ok(respuesta); 
            }
            catch (Exception ex)
            {
                //log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //PUT: api/pago/actualizar-pago
        /// <summary>
        /// Actualizar un registro de pago.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPut("api/pago/actualizar-pago")]
        public async Task<ActionResult> UpdatePago(PagoDTO PagoDTO)
        {
            try
            {        
                //log.Info("Inicio EndosoController/registrarEndosoApoderadoPago");

                var respuesta = await _pagoService.UpdatePago(PagoDTO);

                //log.Info("Fin EndosoController/registrarEndosoApoderadoPago");

                return Ok(respuesta); 
            }
            catch (Exception ex)
            {
                //log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //DELETE: api/pago/eliminar-pago
        /// <summary>
        /// Eliminar un registro de pago.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpDelete("api/pago/eliminar-pago")]
        public async Task<ActionResult> DeletePago(Int32 PagoId)
        {
            try
            {        
                //log.Info("Inicio EndosoController/registrarEndosoApoderadoPago");

                var respuesta = await _pagoService.DeletePago(PagoId);

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