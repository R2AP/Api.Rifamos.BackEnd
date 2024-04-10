using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Microsoft.AspNetCore.Mvc;
//using log4net;

namespace Api.Rifamos.BackEnd.Controllers{

    [ApiController]
    public class PrecioController : ControllerBase{

        private readonly IPrecioService _precioService;
        //private static readonly ILog log = LogManager.GetLogger(typeof(PrecioController));

        public PrecioController(IPrecioService PrecioService)
        {
            _precioService = PrecioService;

            //log4net.GlobalContext.Properties["fDirectory"] = AppContext.BaseDirectory;
            //Logger.InicializarLog();
        }

        //GET: api/obtener-precio
        ///<summary>
        ///Obtener una lista de un registro con la Precio seleccionada.
        ///</summary>
        ///<param name="PrecioId">Específica el id de la Precio seleccionada.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/precio/obtener-precio/{PrecioId}")]
        public async Task<ActionResult> GetPrecio(Int32 PrecioId)
        {
            try
            {
                //log.Info("Inicio EndosoController/GetListaEndosos");

                var listaPrecio = await _precioService.GetPrecio(PrecioId);

                if (listaPrecio == null)
                {
                    return NoContent();
                }

                //log.Info("Fin EndosoController/GetListaEndosos");
                return Ok(listaPrecio);
            }
            catch(Exception ex)
            {
                //log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }        

        //GET: api/listar-precio
        ///<summary>
        ///Obtener una lista de un registro con el Precio por Rifa.
        ///</summary>
        ///<param name="RifaId">Específica el id de la Rifa seleccionada.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/precio/listar-precio/{RifaId}")]
        public async Task<ActionResult> GetListPrecio(Int32 RifaId)
        {
            try
            {
                //log.Info("Inicio EndosoController/GetListaEndosos");

                var listPrecio = await _precioService.GetListPrecio(RifaId);

                if (listPrecio == null)
                {
                    return NoContent();
                }

                //log.Info("Fin EndosoController/GetListaEndosos");
                return Ok(listPrecio);
            }
            catch(Exception ex)
            {
                //log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //POST: api/precio/registro-precio
        /// <summary>
        /// Crear los precios de la Rifa .
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPost("api/precio/registro-precio")]
        public async Task<ActionResult> InsertPrecio(PrecioDTO PrecioDTO)
        {
            try
            {        
                //log.Info("Inicio EndosoController/registrarEndosoApoderadoPago");

                var respuesta = await _precioService.InsertPrecio(PrecioDTO);

                //log.Info("Fin EndosoController/registrarEndosoApoderadoPago");

                return Ok(respuesta); 
            }
            catch (Exception ex)
            {
                //log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //PUT: api/precio/actualizar-precio
        /// <summary>
        /// Actualizar el precio de la Rifa .
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPut("api/precio/actualizar-precio")]
        public async Task<ActionResult> UpdatePrecio(PrecioDTO PrecioDTO)
        {
            try
            {        
                //log.Info("Inicio EndosoController/registrarEndosoApoderadoPago");

                var respuesta = await _precioService.UpdatePrecio(PrecioDTO);

                //log.Info("Fin EndosoController/registrarEndosoApoderadoPago");

                return Ok(respuesta); 
            }
            catch (Exception ex)
            {
                //log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //DELETE: api/precio/eliminar-precio
        /// <summary>
        /// Elimina el precio de la Rifa .
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpDelete("api/precio/eliminar-precio")]
        public async Task<ActionResult> DeletePrecio(Int32 PrecioId)
        {
            try
            {        

                var respuesta = await _precioService.DeletePrecio(PrecioId);

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