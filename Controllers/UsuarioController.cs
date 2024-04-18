using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Microsoft.AspNetCore.Mvc;
using log4net;

namespace Api.Rifamos.BackEnd.Controllers{

    [ApiController]
    public class UsuarioController : ControllerBase{

        private readonly IUsuarioService _usuarioService;
        private static readonly ILog log = LogManager.GetLogger(typeof(UsuarioController));

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

        //POST: api/usuario/registro-usuario
        /// <summary>
        /// Crear una nueva Usuario.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPost("api/usuario/registro-usuario")]
        public async Task<ActionResult> InsertUsuario(Usuario Usuario, string Password)
        {
            try
            {        
                log.Info("Inicio api/usuario/registro-usuario");

                var respuesta = await _usuarioService.InsertUsuario(Usuario, Password);
//var token = _usuarioService.GenerarToken(UsuarioDTO);

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
        public async Task<ActionResult> UpdateUsuario(Usuario Usuario)
        {
            try
            {        
                log.Info("Inicio api/usuario/actualizar-usuario");

                var respuesta = await _usuarioService.UpdateUsuario(Usuario);

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

        //GET: api/usuario/login-usuario
        ///<summary>
        ///Logear un usuario
        ///</summary>
        ///<param name="Usuario">Nombre de usuario.</param>
        ///<param name="Password">Password.</param>///
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/usuario/login-usuario/{Usuario}/{Password}")]
        public async Task<ActionResult> LoginUsuario(string Usuario, string Password)
        {
            try
            {
                log.Info("Inicio api/usuario/login-usuario");

                var oUsuario = await _usuarioService.LoginUsuario(Usuario, Password);

                if (oUsuario == null)
                {
                    return NoContent();
                }

                log.Info("Fin api/usuario/login-usuario");

                return Ok(oUsuario);
            }
            catch(Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }             
    }
}