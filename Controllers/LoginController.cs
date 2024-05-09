using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Api.Rifamos.BackEnd.Adapter;
using Microsoft.AspNetCore.Mvc;
using log4net;
using Microsoft.AspNetCore.Authorization;

namespace Api.Rifamos.BackEnd.Controllers{

    [ApiController]
    public class LoginController : ControllerBase{

        private readonly ILoginService _loginService;
        private static readonly ILog log = LogManager.GetLogger(typeof(LoginController));

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;

            log4net.GlobalContext.Properties["fDirectory"] = AppContext.BaseDirectory;
            Logger.InicializarLog();
        }

        //POST: api/Login/loguearse
        ///<summary>
        ///Logear a un usuario
        ///</summary>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpPost]
        [Route("api/login/loguearse")]
        public async Task<ActionResult> Login(LoginDTO LoginDTO)
        {
            try
            {
                //log.Info("Inicio api/login/loguearse");

                UsuarioFrontDTO oUsuarioFrontDTO = await _loginService.LoginUsuario(LoginDTO);

                if (oUsuarioFrontDTO == null)
                {
                    return NoContent();
                }

                if (oUsuarioFrontDTO.Error == true)
                {
                    return BadRequest(oUsuarioFrontDTO.Mensaje);
                }

                //log.Info("Fin api/login/loguearse");

                return Ok(oUsuarioFrontDTO);
            }
            catch(Exception ex)
            {
                log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }
    }
}