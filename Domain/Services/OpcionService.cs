using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using log4net;

namespace Api.Rifamos.BackEnd.Domain.Services{
    public class OpcionService : IOpcionService
    {
        private readonly IOpcionRepository _opcionRepository;
        private readonly ICryptoService _cryptoService;
        private readonly IEmailService _emailService;
        private static readonly ILog log = LogManager.GetLogger(typeof(UsuarioService));
        // public IConfiguration _configuration { get; }
        // private IHostingEnvironment _environment;
        //private static readonly ILog log = LogManager.GetLogger(typeof(UltimusService));

        public OpcionService(IOpcionRepository opcionRepository,
                            ICryptoService cryptoService,
                            IEmailService emailService,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _opcionRepository = opcionRepository;
            _cryptoService = cryptoService;
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

        public async Task<OpcionFrontDTO> GetOpcionToken(string TokenOpcion)
        {

            string sError = "";
            OpcionFrontDTO oOpcionFrontDTO = new();

            // Buscamos la opción por token
            OpcionDTO oOpcionDTO = await _opcionRepository.GetOpcionToken(TokenOpcion); 

            // Validamos que el token sea el correcto 
            byte[] oKey = Convert.FromBase64String(oOpcionDTO.TokenKey1);
            byte[] oIV = Convert.FromBase64String(oOpcionDTO.TokenKey2);
            byte[] oEncryptedPassword = Convert.FromBase64String(oOpcionDTO.TokenOpcion);

            string sDecryptedPassword = _cryptoService.Decrypt(oEncryptedPassword, oKey, oIV);

            if (oOpcionDTO.OpcionId.ToString() != sDecryptedPassword){
                sError = "No se encontró la opción ingresada: " + oOpcionDTO.TokenOpcion;
                log.Error(sError);
                return null;
            }

            oOpcionFrontDTO.OpcionId = oOpcionDTO.OpcionId;
            oOpcionFrontDTO.RifaId = oOpcionDTO.RifaId;
            oOpcionFrontDTO.UsuarioId = oOpcionDTO.UsuarioId;
            oOpcionFrontDTO.TokenOpcion = oOpcionDTO.TokenOpcion;
            oOpcionFrontDTO.CantidadOpciones = oOpcionDTO.CantidadOpciones;
            oOpcionFrontDTO.EstadoOpcion = oOpcionDTO.EstadoOpcion;
            oOpcionFrontDTO.AuditoriaUsuarioIngreso = oOpcionDTO.AuditoriaUsuarioIngreso;
            oOpcionFrontDTO.AuditoriaFechaIngreso = oOpcionDTO.AuditoriaFechaIngreso;
            oOpcionFrontDTO.AuditoriaUsuarioModificacion = oOpcionDTO.AuditoriaUsuarioModificacion;
            oOpcionFrontDTO.AuditoriaFechaModificacion = oOpcionDTO.AuditoriaFechaModificacion;

            return oOpcionFrontDTO;

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

            Boolean oSendEmailGmail = _emailService.SendEmailGmail();

            if(oSendEmailGmail=false)
            {
                
            }

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