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
        readonly string sServicio = "OpcionService: ";

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
        public async Task<Opcion> Get(Int32 oOpcionId) => await _opcionRepository.Get(oOpcionId);

        public async Task<Opcion> Insert(Opcion oOpcion)
        {
     
            await _opcionRepository.Post(oOpcion);

            return await Get(oOpcion.OpcionId);

        }

        public async Task<Opcion> Update(Opcion oOpcion)
        {

            await _opcionRepository.Put(oOpcion);

            return await Get(oOpcion.OpcionId);

        }

        public async Task<Opcion> Delete(Int32 oOpcionId)
        {

            Opcion oOpcion = await Get(oOpcionId);

            await _opcionRepository.Delete(oOpcion);

            return oOpcion;

        }

		//Métodos Complementarios
       public async Task<OpcionFrontDTO> GetOpcionToken(string oTokenOpcion)
        {
        
            // Buscamos la opción por token
            Opcion oOpcion = await _opcionRepository.GetOpcionToken(oTokenOpcion); 

            OpcionFrontDTO oOpcionFrontDTO = new();

            List<string> oListToken = [];
       
            oListToken.Add(oOpcion.TokenOpcion);
            oListToken.Add(oOpcion.TokenKey1);
            oListToken.Add(oOpcion.TokenKey2);
            
            string sDecryptedPassword = _cryptoService.IDecrypt(oListToken);

            if (oOpcion.OpcionId.ToString() != sDecryptedPassword){
                oOpcionFrontDTO.Error = true;
                oOpcionFrontDTO.Mensaje =  "No se encontró la opción ingresada [" + oOpcion.TokenOpcion + "]";
                log.Error(sServicio + oOpcionFrontDTO.Mensaje);
                return oOpcionFrontDTO;
            }

            oOpcionFrontDTO.OpcionId = oOpcion.OpcionId;
            oOpcionFrontDTO.RifaId = oOpcion.RifaId;
            oOpcionFrontDTO.UsuarioId = oOpcion.UsuarioId;
            oOpcionFrontDTO.TokenOpcion = oOpcion.TokenOpcion;
            oOpcionFrontDTO.CantidadOpciones = oOpcion.CantidadOpciones;
            oOpcionFrontDTO.EstadoOpcion = oOpcion.EstadoOpcion;

            return oOpcionFrontDTO;

        } 

        public async Task<List<OpcionFrontDTO>> GetListOpcion(Int32 oRifaId, Int32 oUsuarioId)
        {

            List<Opcion> oListOpcion = [];       
            List<OpcionFrontDTO> oListOpcionFrontDTO =  [];

            oListOpcion = await _opcionRepository.GetListOpcion(oRifaId, oUsuarioId);

            if (oListOpcion.Count == 0){
                OpcionFrontDTO oOpcionFrontDTO = new()
                {
                    Error = true,
                    Mensaje = "No se encontraron opciones para la selección."
                };
                oListOpcionFrontDTO.Add(oOpcionFrontDTO);
                log.Error(sServicio + oOpcionFrontDTO.Mensaje);
                return oListOpcionFrontDTO;
            }

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

        public async Task<Opcion> InsertOpcion(OpcionDTO oOpcionDTO)
        {
     
            Opcion oOpcion = new()
            {
                RifaId = oOpcionDTO.RifaId,
                UsuarioId = oOpcionDTO.UsuarioId,
                CantidadOpciones = oOpcionDTO.CantidadOpciones,
                TokenOpcion = "0",
                TokenKey1 =  "0",
                TokenKey2 = "0",
                EstadoOpcion = oOpcionDTO.EstadoOpcion, 
                AuditoriaUsuarioIngreso = oOpcionDTO.AuditoriaUsuarioIngreso,
                AuditoriaFechaIngreso = DateTime.Now
            };

            //Insertamos la Opción
            oOpcion = await Insert(oOpcion);

            //Encrypt the OpcionId con el ID devuelto
            //List<TokenDTO> oListToken = [];
            List<string> oListToken = [];
            oListToken = _cryptoService.IEncrypt(oOpcion.RifaId.ToString() + "-" + oOpcion.OpcionId.ToString());
       
            oOpcion.TokenOpcion = oListToken[0];//.Key;
            oOpcion.TokenKey1 = oListToken[1];
            oOpcion.TokenKey2 = oListToken[2];

            //Actualizamos el token y las keys
            oOpcion = await Update(oOpcion);

            return oOpcion;

        }

    }

}