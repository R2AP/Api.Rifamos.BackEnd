using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Microsoft.AspNetCore.Mvc;
using log4net;

namespace Api.Rifamos.BackEnd.Controllers{

    [ApiController]
    public class UsuarioController : ControllerBase{

        private readonly IUsuarioService _usuarioService;
        private static readonly ILog log = LogManager.GetLogger(typeof(RifaController));

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;

            log4net.GlobalContext.Properties["fDirectory"] = AppContext.BaseDirectory;
            Logger.InicializarLog();
        }

        //GET: api/usuario/obtener-usuario
        ///<summary>
        ///Obtener una usuario de la opción comprada
        ///</summary>
        ///<param name="UsuarioId">Específica el id de la Rifa seleccionada.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/usuario/obtener-usuario/{UsuarioId}")]
        public async Task<ActionResult> GetUsuario(Int32 UsuarioId)
        {
            try
            {
                log.Info("Inicio api/usuario/obtener-usuario");

                var oUsuario = await _usuarioService.GetUsuario(UsuarioId);

                if (oUsuario == null)
                {
                    return NoContent();
                }

                log.Info("Fin api/usuario/obtener-usuario");

                return Ok(oUsuario);
            }
            catch(Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
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

        //POST: api/usuario/registro-usuario
        /// <summary>
        /// Crear una nueva Usuario.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPost("api/usuario/registro-usuario")]
        public async Task<ActionResult> InsertUsuario(UsuarioDTO UsuarioDTO)
        {
            try
            {        
                log.Info("Inicio api/usuario/registro-usuario");

                var respuesta = await _usuarioService.InsertUsuario(UsuarioDTO);

                log.Info("Fin api/usuario/registro-usuario");

                return Ok(respuesta); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //PUT: api/usuario/actualizar-usuario
        /// <summary>
        /// Actualizar un registro de usuario.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPut("api/usuario/actualizar-usuario")]
        public async Task<ActionResult> UpdateUsuario(UsuarioDTO UsuarioDTO)
        {
            try
            {        
                log.Info("Inicio api/usuario/actualizar-usuario");

                var respuesta = await _usuarioService.UpdateUsuario(UsuarioDTO);

                log.Info("Fin api/usuario/actualizar-usuario");

                return Ok(respuesta); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //DELETE: api/usuario/eliminar-usuario
        /// <summary>
        /// Eliminar un registro de usuario.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpDelete("api/usuario/eliminar-usuario")]
        public async Task<ActionResult> DeleteUsuario(Int32 UsuarioId)
        {
            try
            {        
                log.Info("Inicio api/usuario/eliminar-usuario");

                var respuesta = await _usuarioService.DeleteUsuario(UsuarioId);

                log.Info("Fin api/usuario/eliminar-usuario");

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