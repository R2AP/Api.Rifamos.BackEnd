using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Microsoft.AspNetCore.Mvc;
using log4net;

namespace Api.Rifamos.BackEnd.Controllers{

    [ApiController]
    public class PrecioController : ControllerBase{

        private readonly IPrecioService _precioService;
        private static readonly ILog log = LogManager.GetLogger(typeof(PrecioController));

        public PrecioController(IPrecioService PrecioService)
        {
            _precioService = PrecioService;

            log4net.GlobalContext.Properties["fDirectory"] = AppContext.BaseDirectory;
            Logger.InicializarLog();
        }

        //GET: api/precio/obtener-precio
        ///<summary>
        ///Obtener una lista de un registro con la Precio seleccionada.
        ///</summary>
        ///<param name="PrecioId">Específica el id de la Precio seleccionada.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/precio/obtener-precio/{oPrecioId}")]
        public async Task<ActionResult> GetPrecio(Int32 oPrecioId)
        {
            try
            {
                log.Info("Inicio api/precio/obtener-precio");

                var oListaPrecio = await _precioService.GetPrecio(oPrecioId);

                if (oListaPrecio == null)
                {
                    return NoContent();
                }

                log.Info("Fin api/precio/obtener-precio");

                return Ok(oListaPrecio);
            }
            catch(Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }        

        //GET: api/precio/listar-precio
        ///<summary>
        ///Obtener una lista de un registro con el Precio por Rifa.
        ///</summary>
        ///<param name="RifaId">Específica el id de la Rifa seleccionada.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/precio/listar-precio/{oRifaId}")]
        public async Task<ActionResult> GetListPrecio(Int32 oRifaId)
        {
            try
            {
                log.Info("Inicio api/precio/listar-precio");

                var oListPrecio = await _precioService.GetPrecioUnitario(oRifaId);

                if (oListPrecio == null)
                {
                    return NoContent();
                }

                log.Info("Fin api/precio/listar-precio");

                return Ok(oListPrecio);
            }
            catch(Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //POST: api/precio/registro-precio
        /// <summary>
        /// Crear los precios de la Rifa .
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPost("api/precio/registro-precio")]
        public async Task<ActionResult> InsertPrecio(PrecioDTO oPrecioDTO)
        {
            try
            {        
                log.Info("Inicio api/precio/registro-precio");

                var oPrecio = await _precioService.InsertPrecio(oPrecioDTO);

                log.Info("Fin api/precio/registro-precio");

                return Ok(oPrecio); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //PUT: api/precio/actualizar-precio
        /// <summary>
        /// Actualizar el precio de la Rifa .
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPut("api/precio/actualizar-precio")]
        public async Task<ActionResult> UpdatePrecio(PrecioDTO oPrecioDTO)
        {
            try
            {        
                log.Info("Inicio api/precio/actualizar-precio");

                var oPrecio = await _precioService.UpdatePrecio(oPrecioDTO);

                log.Info("Fin api/precio/actualizar-precio");

                return Ok(oPrecio); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //DELETE: api/precio/eliminar-precio
        /// <summary>
        /// Elimina el precio de la Rifa .
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpDelete("api/precio/eliminar-precio")]
        public async Task<ActionResult> DeletePrecio(Int32 oPrecioId)
        {
            try
            {        

                log.Info("Inicio api/precio/eliminar-precio");

                var oPrecio = await _precioService.DeletePrecio(oPrecioId);

                log.Info("Fin api/precio/eliminar-precio");

                return Ok(oPrecio); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }        
    }

}
