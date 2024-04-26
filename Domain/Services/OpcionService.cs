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

        //Métodos Básicos
        public async Task<Opcion> GetOpcion(Int32 oOpcionId)
        {

            return await _opcionRepository.Get(oOpcionId);
        } 

        private async Task<Opcion> InsertOpcion(Opcion oOpcion)
        {
     
            await _opcionRepository.Post(oOpcion);

            return await GetOpcion(oOpcion.OpcionId);

        }

        private async Task<Opcion> UpdateOpcion(Opcion oOpcion)
        {

            await _opcionRepository.Put(oOpcion);

            return await GetOpcion(oOpcion.OpcionId);

        }

        private async Task<Opcion> DeleteOpcion(Int32 oOpcionId)
        {

            Opcion oOpcion = await GetOpcion(oOpcionId);

            await _opcionRepository.Delete(oOpcion);

            return oOpcion;

        }

		//Métodos Complementarios
       public async Task<OpcionFrontDTO> IGetOpcionToken(string TokenOpcion)
        {

            string sError = "";
        
            // Buscamos la opción por token
            Opcion oOpcion = await _opcionRepository.GetOpcionToken(TokenOpcion); 

            List<string> oListToken = [];
       
            oListToken.Add(oOpcion.TokenOpcion);
            oListToken.Add(oOpcion.TokenKey1);
            oListToken.Add(oOpcion.TokenKey2);
            
            string sDecryptedPassword = _cryptoService.IDecrypt(oListToken);

            if (oOpcion.OpcionId.ToString() != sDecryptedPassword){
                sError = "OpcionService.GetOpcionToken: No se encontró la opción ingresada " + oOpcion.TokenOpcion;
                sError = "No se encontró la opción ingresada: " + oOpcion.TokenOpcion;
                log.Error(sError);
                return null;
            }

            OpcionFrontDTO oOpcionFrontDTO = new()
            {
                OpcionId = oOpcion.OpcionId,
                RifaId = oOpcion.RifaId,
                UsuarioId = oOpcion.UsuarioId,
                TokenOpcion = oOpcion.TokenOpcion,
                CantidadOpciones = oOpcion.CantidadOpciones,
                EstadoOpcion = oOpcion.EstadoOpcion
            };

            return oOpcionFrontDTO;

        } 

        public async Task<List<OpcionFrontDTO>> IGetListOpcion(Int32 RifaId, Int32 UsuarioId)
        {

            List<Opcion> oListOpcion = [];       
            List<OpcionFrontDTO> oListOpcionFrontDTO =  [];

            oListOpcion = await _opcionRepository.GetListOpcion(RifaId, UsuarioId);

            foreach( var item in oListOpcion) {

                OpcionFrontDTO oOpcionFrontDTO = new()
                {
                    OpcionId = item.OpcionId,
                    RifaId = item.RifaId,
                    UsuarioId = item.UsuarioId,
                    CantidadOpciones = item.CantidadOpciones,
                    TokenOpcion = item.TokenOpcion,
                    EstadoOpcion = item.EstadoOpcion
                };

                oListOpcionFrontDTO.Add(oOpcionFrontDTO);

            }

            return oListOpcionFrontDTO;
        } 

        public async Task<Opcion> IInsertOpcion(OpcionDTO OpcionDTO)
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
            await InsertOpcion(oOpcion);

            //Encrypt the OpcionId con el ID devuelto
            //List<TokenDTO> oListToken = [];
            List<string> oListToken = [];
            oListToken = _cryptoService.IEncrypt(oOpcion.RifaId.ToString() + "-" + oOpcion.OpcionId.ToString());
       
            oOpcion.TokenOpcion = oListToken[0];//.Key;
            oOpcion.TokenKey1 = oListToken[1];
            oOpcion.TokenKey2 = oListToken[2];

            //Actualizamos el token y las keys
            await UpdateOpcion(oOpcion);

            oOpcion = await GetOpcion(oOpcion.OpcionId);

            return oOpcion;

        }

        // public async Task<Opcion> UpdateOpcion(OpcionDTO OpcionDTO)
        // {

        //     Opcion oOpcion = await _opcionRepository.Get(OpcionDTO.OpcionId);

        //     oOpcion.RifaId = OpcionDTO.RifaId;
        //     oOpcion.UsuarioId = OpcionDTO.UsuarioId;
        //     oOpcion.CantidadOpciones = OpcionDTO.CantidadOpciones;
        //     oOpcion.TokenOpcion = OpcionDTO.TokenOpcion;
        //     oOpcion.EstadoOpcion = OpcionDTO.EstadoOpcion;
        //     oOpcion.AuditoriaUsuarioModificacion = OpcionDTO.AuditoriaUsuarioModificacion;
        //     oOpcion.AuditoriaFechaModificacion = DateTime.Now;

        //     await _opcionRepository.Put(oOpcion);

        //     return oOpcion;

        // }

    }

}