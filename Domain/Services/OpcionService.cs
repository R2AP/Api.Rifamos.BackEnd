using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;


namespace Api.Rifamos.BackEnd.Domain.Services{
    public class OpcionService : IOpcionService
    {
        private readonly IOpcionRepository _opcionRepository;
        private readonly ICryptoService _cryptoService;

        // public IConfiguration _configuration { get; }
        // private IHostingEnvironment _environment;
        //private static readonly ILog log = LogManager.GetLogger(typeof(UltimusService));

        public OpcionService(IOpcionRepository opcionRepository,
                            ICryptoService cryptoService,        
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _opcionRepository = opcionRepository;
            _cryptoService = cryptoService;
            // _configuration = configuration;
            // _environment = environment;
        }

        public async Task<List<Opcion>> GetListOpcion(Int32 RifaId, Int32 UsuarioId)
        {
            return await _opcionRepository.GetListOpcion(RifaId, UsuarioId);
        } 

        public async Task<Opcion> GetOpcion(Int32 OpcionId)
        {
            return await _opcionRepository.Get(OpcionId);
        } 

        public async Task<OpcionDTO> GetOpcionToken(string TokenOpcion)
        {
            return await _opcionRepository.GetOpcionToken(TokenOpcion);
        } 

        public async Task<Opcion> InsertOpcion(OpcionDTO OpcionDTO)
        {

            byte[] oKey = new byte[16];
            byte[] oIV = new byte[16];

            using(RandomNumberGenerator rng = RandomNumberGenerator.Create()) {
            rng.GetBytes(oKey);
            rng.GetBytes(oIV);
            }
     
            Opcion oOpcion = new()
            {
                RifaId = OpcionDTO.RifaId,
                UsuarioId = OpcionDTO.UsuarioId,
                CantidadOpciones = OpcionDTO.CantidadOpciones,
                TokenOpcion = "0",
                TokenKey1 =  "0",
                TokenKey2 = "0",
                EstadoOpcion = OpcionDTO.EstadoOpcion, 
                AuditoriaUsuarioIngreso = OpcionDTO.AuditoriaUsuarioIngreso,
                AuditoriaFechaIngreso = DateTime.Now
            };

            await _opcionRepository.Post(oOpcion);

            //Encrypt the OpcionId
            byte[] oEncryptedOpcionId = _cryptoService.Encrypt(oOpcion.OpcionId.ToString(), oKey, oIV);
       
            oOpcion.TokenOpcion = Convert.ToBase64String(oEncryptedOpcionId);
            oOpcion.TokenKey1 = Convert.ToBase64String(oKey);
            oOpcion.TokenKey2 = Convert.ToBase64String(oIV);

            await _opcionRepository.Put(oOpcion);

            return oOpcion;

        }

        public async Task<Opcion> UpdateOpcion(OpcionDTO OpcionDTO)
        {

            Opcion oOpcion = await _opcionRepository.Get(OpcionDTO.OpcionId);

            oOpcion.RifaId = OpcionDTO.RifaId;
            oOpcion.UsuarioId = OpcionDTO.UsuarioId;
            oOpcion.CantidadOpciones = OpcionDTO.CantidadOpciones;
            oOpcion.TokenOpcion = OpcionDTO.TokenOpcion;
            oOpcion.EstadoOpcion = OpcionDTO.EstadoOpcion;
            oOpcion.AuditoriaUsuarioModificacion = OpcionDTO.AuditoriaUsuarioModificacion;
            oOpcion.AuditoriaFechaModificacion = DateTime.Now;

            await _opcionRepository.Put(oOpcion);

            return oOpcion;

        }

        public async Task<Opcion> DeleteOpcion(Int32 OpcionId)
        {

            Opcion oOpcion = new()
            {
                OpcionId =  OpcionId       
            };

            await _opcionRepository.Delete(oOpcion);

            return oOpcion;

        }
    }

}