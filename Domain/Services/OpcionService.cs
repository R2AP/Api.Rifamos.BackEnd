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
            _emailService = emailService;
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

            List<string> oListToken = [];
       
            oListToken[0] = oOpcionDTO.TokenOpcion;
            oListToken[1] = oOpcionDTO.TokenKey1;
            oListToken[2] = oOpcionDTO.TokenKey2;
            
            string sDecryptedPassword = _cryptoService.IDecrypt(oListToken);

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

            //Insertamos la Opción
            await _opcionRepository.Post(oOpcion);

            //Encrypt the OpcionId con el ID devuelto
            //List<TokenDTO> oListToken = [];
            List<string> oListToken = [];
            oListToken = _cryptoService.IEncrypt(oOpcion.OpcionId.ToString());
       
            oOpcion.TokenOpcion = oListToken[0];//.Key;
            oOpcion.TokenKey1 = oListToken[1];
            oOpcion.TokenKey2 = oListToken[2];

            //Actualizamos el token y las keys
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