using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using System.Security.Cryptography;
using log4net;

namespace Api.Rifamos.BackEnd.Domain.Services{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICryptoService _cryptoService;
        private static readonly ILog log = LogManager.GetLogger(typeof(UsuarioService));

        public UsuarioService(
                            IUsuarioRepository usuarioRepository,
                            ICryptoService cryptoService,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _usuarioRepository = usuarioRepository;
            _cryptoService = cryptoService;
            // _configuration = configuration;
            // _environment = environment;
        }

        public async Task<UsuarioDTO> GetUsuario(UsuarioDTO UsuarioDTO)
        {
            Usuario oUsuario = new();
            UsuarioDTO oUsuarioDTO = new();

            oUsuario = await _usuarioRepository.Get(UsuarioDTO.UsuarioId);

            oUsuarioDTO.UsuarioId = oUsuario.UsuarioId;
            oUsuarioDTO.Nombres = oUsuario.Nombres;
            oUsuarioDTO.ApellidoPaterno = oUsuario.ApellidoPaterno; 
            oUsuarioDTO.ApellidoMaterno = oUsuario.ApellidoMaterno;
            oUsuarioDTO.Email = oUsuario.Email;
            oUsuarioDTO.Password = "****************";
            oUsuarioDTO.TipoDocumento = oUsuario.TipoDocumento;
            oUsuarioDTO.NumeroDocumento = oUsuario.NumeroDocumento;
            oUsuarioDTO.Telefono = oUsuario.Telefono;
            oUsuarioDTO.AuditoriaUsuarioIngreso = oUsuario.AuditoriaUsuarioIngreso; 
            oUsuarioDTO.AuditoriaFechaIngreso = oUsuario.AuditoriaFechaIngreso;
            oUsuarioDTO.AuditoriaUsuarioModificacion = oUsuario.AuditoriaUsuarioModificacion;
            oUsuarioDTO.AuditoriaFechaModificacion = oUsuario.AuditoriaFechaModificacion;
            
            //return await _usuarioRepository.Get(UsuarioId);
            return oUsuarioDTO;
        }

        //public async Task<Usuario> InsertUsuario(Usuario Usuario, string Password)
        public async Task<UsuarioDTO> InsertUsuario(UsuarioDTO UsuarioDTO)
        {

            //Encrypt the OpcionId con el ID devuelto
            //List<TokenDTO> oListToken = [];
            List<string> oListToken = [];
            oListToken = _cryptoService.IEncrypt(UsuarioDTO.Password);

            UsuarioDTO.Password = oListToken[0];
            UsuarioDTO.Key1 = oListToken[1];
            UsuarioDTO.Key2 = oListToken[2];

            Usuario oUsuario = new(){

                UsuarioId = UsuarioDTO.UsuarioId,
                Nombres = UsuarioDTO.Nombres, 
                ApellidoPaterno = UsuarioDTO.ApellidoPaterno, 
                ApellidoMaterno = UsuarioDTO.ApellidoMaterno,
                Email = UsuarioDTO.Email,
                Password = UsuarioDTO.Password,
                Key1 = UsuarioDTO.Key1,
                Key2 = UsuarioDTO.Key2,
                TipoDocumento = UsuarioDTO.TipoDocumento,
                NumeroDocumento = UsuarioDTO.NumeroDocumento,
                Telefono = UsuarioDTO.Telefono,
                AuditoriaUsuarioIngreso = UsuarioDTO.AuditoriaUsuarioIngreso, 
                AuditoriaFechaIngreso = DateTime.Now 
                
            };

            await _usuarioRepository.Post(oUsuario);

            UsuarioDTO.UsuarioId = oUsuario.UsuarioId;

            UsuarioDTO = await GetUsuario(UsuarioDTO);

            return UsuarioDTO;

        }

        public async Task<UsuarioDTO> UpdateUsuario(UsuarioDTO UsuarioDTO)
        {

            Usuario oUsuario = await _usuarioRepository.Get(UsuarioDTO.UsuarioId);

            oUsuario.UsuarioId = UsuarioDTO.UsuarioId;
            oUsuario.Nombres = UsuarioDTO.Nombres;
            oUsuario.ApellidoPaterno = UsuarioDTO.ApellidoPaterno;
            oUsuario.ApellidoMaterno = UsuarioDTO.ApellidoMaterno;
            oUsuario.Email = UsuarioDTO.Email;
            oUsuario.TipoDocumento = UsuarioDTO.TipoDocumento;
            oUsuario.NumeroDocumento = UsuarioDTO.NumeroDocumento;
            oUsuario.Telefono = UsuarioDTO.Telefono;
            oUsuario.AuditoriaUsuarioModificacion = UsuarioDTO.AuditoriaUsuarioModificacion; 
            oUsuario.AuditoriaFechaModificacion = DateTime.Now;

            await _usuarioRepository.Put(oUsuario);
            
            UsuarioDTO = await GetUsuario(UsuarioDTO);

            return UsuarioDTO;

        }

        public async Task<UsuarioDTO> DeleteUsuario(UsuarioDTO UsuarioDTO)
        {

            Usuario oUsuario = await _usuarioRepository.Get(UsuarioDTO.UsuarioId);

            await _usuarioRepository.Delete(oUsuario);

            return UsuarioDTO;

        }
    }
}
