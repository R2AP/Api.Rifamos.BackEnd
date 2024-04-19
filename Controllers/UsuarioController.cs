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

        //POST: api/usuario/obtener-usuario
        ///<summary>
        ///Obtener un usuario por su id
        ///</summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPost]
        [Route("api/usuario/obtener-usuario")]
        public async Task<ActionResult> GetUsuario(UsuarioDTO UsuarioDTO)
        {
            try
            {
                log.Info("Inicio api/usuario/obtener-usuario");

                var oUsuario = await _usuarioService.GetUsuario(UsuarioDTO);
                
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
        /// Crear un nuevo Usuario.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPost("api/usuario/registro-usuario")]
        //public async Task<ActionResult> InsertUsuario(Usuario Usuario, string Password)
        public async Task<ActionResult> InsertUsuario(UsuarioDTO UsuarioDTO)
        {
            try
            {        
                log.Info("Inicio api/usuario/registro-usuario");

                //var respuesta = await _usuarioService.InsertUsuario(Usuario, Password);
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
        public async Task<ActionResult> DeleteUsuario(UsuarioDTO UsuarioDTO)
        {
            try
            {        
                log.Info("Inicio api/usuario/eliminar-usuario");

                var respuesta = await _usuarioService.DeleteUsuario(UsuarioDTO);

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