using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Microsoft.AspNetCore.Mvc;
//using log4net;

namespace Api.Rifamos.BackEnd.Controllers{

    [ApiController]
    public class VentaController : ControllerBase{

        private readonly IVentaService _ventaService;
        //private static readonly ILog log = LogManager.GetLogger(typeof(RifaController));

        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;

            //log4net.GlobalContext.Properties["fDirectory"] = AppContext.BaseDirectory;
            //Logger.InicializarLog();
        }

        //GET: api/obtener-venta
        ///<summary>
        ///Obtener una venta de la opción comprada
        ///</summary>
        ///<param name="VentaId">Específica el id de la Rifa seleccionada.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/venta/obtener-venta/{VentaId}")]
        public async Task<ActionResult> GetVenta(Int32 VentaId)
        {
            try
            {
                //log.Info("Inicio EndosoController/GetListaEndosos");

                var venta = await _ventaService.GetVenta(VentaId);

                if (venta == null)
                {
                    return NoContent();
                }

                //log.Info("Fin EndosoController/GetListaEndosos");
                return Ok(venta);
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

        //POST: api/venta/registro-venta
        /// <summary>
        /// Crear una nueva Venta.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPost("api/venta/registro-venta")]
        public async Task<ActionResult> InsertVenta(VentaDTO VentaDTO)
        {
            try
            {        
                //log.Info("Inicio EndosoController/registrarEndosoApoderadoPago");

                var respuesta = await _ventaService.InsertVenta(VentaDTO);

                //log.Info("Fin EndosoController/registrarEndosoApoderadoPago");

                return Ok(respuesta); 
            }
            catch (Exception ex)
            {
                //log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //PUT: api/venta/actualizar-venta
        /// <summary>
        /// Actualizar un registro de venta.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPut("api/venta/actualizar-venta")]
        public async Task<ActionResult> UpdateVenta(VentaDTO VentaDTO)
        {
            try
            {        
                //log.Info("Inicio EndosoController/registrarEndosoApoderadoPago");

                var respuesta = await _ventaService.UpdateVenta(VentaDTO);

                //log.Info("Fin EndosoController/registrarEndosoApoderadoPago");

                return Ok(respuesta); 
            }
            catch (Exception ex)
            {
                //log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //DELETE: api/venta/eliminar-venta
        /// <summary>
        /// Eliminar un registro de venta.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpDelete("api/venta/eliminar-venta")]
        public async Task<ActionResult> DeleteVenta(Int32 VentaId)
        {
            try
            {        
                //log.Info("Inicio EndosoController/registrarEndosoApoderadoPago");

                var respuesta = await _ventaService.DeleteVenta(VentaId);

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

