using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Microsoft.AspNetCore.Mvc;
//using log4net;

namespace Api.Rifamos.BackEnd.Controllers{

    [ApiController]
    public class RifaController : ControllerBase{

        private readonly IRifaService _rifaService;
        //private static readonly ILog log = LogManager.GetLogger(typeof(RifaController));

        public RifaController(IRifaService rifaService)
        {
            _rifaService = rifaService;

            //log4net.GlobalContext.Properties["fDirectory"] = AppContext.BaseDirectory;
            //Logger.InicializarLog();
        }

        //GET: api/obtener-rifa
        ///<summary>
        ///Obtener una lista de un registro con la rifa seleccionada.
        ///</summary>
        ///<param name="RifaId">Específica el id de la Rifa seleccionada.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/rifa/obtener-rifa/{RifaId}")]
        public async Task<ActionResult> GetRifa(Int32 RifaId)
        {
            try
            {
                //log.Info("Inicio EndosoController/GetListaEndosos");

                var listaRifa = await _rifaService.GetRifa(RifaId);

                if (listaRifa == null)
                {
                    return NoContent();
                }

                //log.Info("Fin EndosoController/GetListaEndosos");
                return Ok(listaRifa);
            }
            catch(Exception ex)
            {
                //log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }        

        //GET: api/obtener-lista-rifa
        ///<summary>
        ///Obtener una lista de rifas de un usuario especifico
        ///</summary>
        ///<param name="UsuarioId">Específica el id del usuario.</param>
        ///<returns>Devuelve una respuesta HTTP y su estado.</returns>
        [HttpGet]
        [Route("api/rifa/obtener-lista-rifa/{UsuarioId}")]
        public async Task<ActionResult> GetListRifa(Int32 UsuarioId)
        {
            try
            {
                //log.Info("Inicio EndosoController/GetListaEndosos");

                var listaRifa = await _rifaService.GetListRifa(UsuarioId);

                if (listaRifa == null)
                {
                    return NoContent();
                }

                //log.Info("Fin EndosoController/GetListaEndosos");
                return Ok(listaRifa);
            }
            catch(Exception ex)
            {
                //log.Error(String.Format("Se ha producido el siguiente error: [{0}]", ex.Message), ex);
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Se ha producido un error interno en el servidor, póngase en contacto con el administrador del sistema"});
            }
        }  
    }

}

// [ApiController]
// [Route("[controller]")]
// public class RifaController : ControllerBase
// {
//     // private static readonly string[] Summaries = new[]
//     // {
//     //     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//     // };

//     private readonly ILogger<RifaController> _logger;

//     public RifaController(ILogger<RifaController> logger)
//     {
//          _logger = logger;
//     }

//     [HttpGet(Name = "GetRifa")]
//     public IEnumerable<Rifa> Get()
//     {
//         return Enumerable.Range(1, 5).Select(index => new Rifa
//         {
//             RifaDescripcion = "Rifa Ejemplo",
//             FechaSorteo = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             AuditoriaUsuarioIngreso = "ralegre"
//             //FechaSorteo = DateTime.Today,
//             //HoraSorteo = DateTime.Now
//         })
//         .ToArray();
//     }
// }
