using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Microsoft.AspNetCore.Mvc;
using log4net;
using Microsoft.AspNetCore.Authorization;

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

        //POST: api/usuario/registro-usuario
        /// <summary>
        /// Crear un nuevo Usuario.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPost("api/usuario/registro-usuario")]
        //public async Task<ActionResult> InsertUsuario(Usuario Usuario, string Password)
        public async Task<ActionResult> InsertUsuario(UsuarioDTO oUsuarioDTO)
        {
            try
            {        
                //log.Info("Inicio api/usuario/registro-usuario");

                UsuarioFrontDTO oUsuarioFrontDTO = await _usuarioService.InsertUsuario(oUsuarioDTO);
                if (oUsuarioFrontDTO.Error)
                {
                    return BadRequest(oUsuarioFrontDTO);
                }

                //log.Info("Fin api/usuario/registro-usuario");

                return Ok(oUsuarioFrontDTO); 
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
        public async Task<ActionResult> UpdateUsuario(UsuarioDTO oUsuarioDTO)
        {
            try
            {        
                //log.Info("Inicio api/usuario/actualizar-usuario");

                var respuesta = await _usuarioService.UpdateUsuario(oUsuarioDTO);

                //log.Info("Fin api/usuario/actualizar-usuario");

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
        public async Task<ActionResult> DeleteUsuario(Int32 oUsuarioId)
        {
            try
            {        
                //log.Info("Inicio api/usuario/eliminar-usuario");

                var respuesta = await _usuarioService.DeleteUsuario(oUsuarioId);

                //log.Info("Fin api/usuario/eliminar-usuario");

                return Ok(respuesta); 
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //PUT: actualizar-password-usuario
        /// <summary>
        /// Actualizar el password del usuario.
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPut("api/usuario/actualizar-password-usuario")]
        public async Task<ActionResult> UpdatePasswordUsuario(UsuarioPasswordDTO oUsuarioPasswordDTO)
        {
            try
            {        
                //log.Info("Inicio api/usuario/actualizar-password-usuario");

                var respuesta = await _usuarioService.UpdatePasswordUsuario(oUsuarioPasswordDTO);

                //log.Info("Fin api/usuario/actualizar-password-usuario");

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }

        //PUT: recuperar-password
        /// <summary>
        /// Recuperar password
        /// </summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPut("api/usuario/recuperar-password")]
        public async Task<ActionResult> RecuperarPassword(string oEmail)
        {
            try
            {        
                //log.Info("Inicio api/usuario/recuperar-password");

                UsuarioFrontDTO oUsuarioFrontDTO  = await _usuarioService.RecuperarPassword(oEmail);

                if (oUsuarioFrontDTO.Error)
                {
                    return BadRequest(oUsuarioFrontDTO);
                }

                //log.Info("Fin api/usuario/recuperar-password");

                return Ok(oUsuarioFrontDTO);
            }
            catch (Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }        

    }
}