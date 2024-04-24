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

        string sError = "";

        public async Task<Usuario> GetUsuario(Int32 UsuarioId)
        {
            Usuario oUsuario = new();

            oUsuario = await _usuarioRepository.Get(UsuarioId);
            
            return oUsuario;
        }

        public async Task<Usuario> GetUsuarioPorEmail(string Email)
        {
            Usuario oUsuario = new();

            oUsuario = await _usuarioRepository.GetUsuarioPorEmail(Email);
            
            return oUsuario;
        }


        public async Task<UsuarioFrontDTO> InsertUsuario(UsuarioDTO UsuarioDTO)
        {

            Usuario oUsuarioActual = new();

            oUsuarioActual = await GetUsuarioPorEmail(UsuarioDTO.Email);

            //Verificamos si la cuenta, en caso exista se da por termionado el proceso
            if (oUsuarioActual != null){
                sError = "UsuarioService.InsertUsuario: La cuenta " + UsuarioDTO.Email + " ya existe" ;
                log.Info(string.Format("UsuarioService.GetUsuarioPorEmail: La cuenta {0} ya existe", UsuarioDTO.Email));
                return null;
            }

            //Encrypt the OpcionId con el ID devuelto
            //List<TokenDTO> oListToken = [];
            List<string> oListToken = [];
            oListToken = _cryptoService.IEncrypt(UsuarioDTO.Password);

            Usuario oUsuario = new(){

                UsuarioId = UsuarioDTO.UsuarioId,
                Nombres = UsuarioDTO.Nombres, 
                ApellidoPaterno = UsuarioDTO.ApellidoPaterno, 
                ApellidoMaterno = UsuarioDTO.ApellidoMaterno,
                Email = UsuarioDTO.Email,
                Password = oListToken[0],
                Key1 = oListToken[1],
                Key2 = oListToken[2],
                TipoDocumento = UsuarioDTO.TipoDocumento,
                NumeroDocumento = UsuarioDTO.NumeroDocumento,
                Telefono = UsuarioDTO.Telefono,
                AuditoriaUsuarioIngreso = UsuarioDTO.AuditoriaUsuario, 
                AuditoriaFechaIngreso = DateTime.Now 
                
            };

            await _usuarioRepository.Post(oUsuario);

            UsuarioDTO.UsuarioId = oUsuario.UsuarioId;

            UsuarioFrontDTO oUsuarioFrontDTO = new();

            oUsuario = await GetUsuario(oUsuario.UsuarioId);

            oUsuarioFrontDTO.UsuarioId = UsuarioDTO.UsuarioId;
            oUsuarioFrontDTO.Nombres = UsuarioDTO.Nombres;
            oUsuarioFrontDTO.ApellidoPaterno = UsuarioDTO.ApellidoPaterno; 
            oUsuarioFrontDTO.ApellidoMaterno = UsuarioDTO.ApellidoMaterno;
            oUsuarioFrontDTO.Email = UsuarioDTO.Email;
            oUsuarioFrontDTO.TipoDocumento = UsuarioDTO.TipoDocumento;
            oUsuarioFrontDTO.NumeroDocumento = UsuarioDTO.NumeroDocumento;
            oUsuarioFrontDTO.Telefono = UsuarioDTO.Telefono;

            return oUsuarioFrontDTO;

        }

        public async Task<UsuarioFrontDTO> UpdateUsuario(UsuarioDTO UsuarioDTO)
        {

            Usuario oUsuario = await _usuarioRepository.Get(UsuarioDTO.UsuarioId);

            oUsuario.UsuarioId = UsuarioDTO.UsuarioId;
            oUsuario.Nombres = UsuarioDTO.Nombres;
            oUsuario.ApellidoPaterno = UsuarioDTO.ApellidoPaterno;
            oUsuario.ApellidoMaterno = UsuarioDTO.ApellidoMaterno;
            oUsuario.Email = UsuarioDTO.Email;
            //El password no se cambia por este medio
            oUsuario.Password = oUsuario.Password;
            oUsuario.TipoDocumento = UsuarioDTO.TipoDocumento;
            oUsuario.NumeroDocumento = UsuarioDTO.NumeroDocumento;
            oUsuario.Telefono = UsuarioDTO.Telefono;
            oUsuario.AuditoriaUsuarioModificacion = UsuarioDTO.AuditoriaUsuario; 
            oUsuario.AuditoriaFechaModificacion = DateTime.Now;

            await _usuarioRepository.Put(oUsuario);

            UsuarioFrontDTO oUsuarioFromtDTO = new();

            oUsuario = await GetUsuario(oUsuario.UsuarioId);

            oUsuarioFromtDTO.UsuarioId = UsuarioDTO.UsuarioId;
            oUsuarioFromtDTO.Nombres = UsuarioDTO.Nombres;
            oUsuarioFromtDTO.ApellidoPaterno = UsuarioDTO.ApellidoPaterno; 
            oUsuarioFromtDTO.ApellidoMaterno = UsuarioDTO.ApellidoMaterno;
            oUsuarioFromtDTO.Email = UsuarioDTO.Email;
            oUsuarioFromtDTO.TipoDocumento = UsuarioDTO.TipoDocumento;
            oUsuarioFromtDTO.NumeroDocumento = UsuarioDTO.NumeroDocumento;
            oUsuarioFromtDTO.Telefono = UsuarioDTO.Telefono;

            return oUsuarioFromtDTO;

        }

        public async Task<UsuarioFrontDTO> DeleteUsuario(UsuarioDTO UsuarioDTO)
        {
            Usuario oUsuario = new(){
                UsuarioId = UsuarioDTO.UsuarioId
            };
            
            UsuarioFrontDTO oUsuarioFromtDTO = new();

            oUsuario = await GetUsuario(oUsuario.UsuarioId);

            oUsuarioFromtDTO.UsuarioId = UsuarioDTO.UsuarioId;
            oUsuarioFromtDTO.Nombres = UsuarioDTO.Nombres;
            oUsuarioFromtDTO.ApellidoPaterno = UsuarioDTO.ApellidoPaterno; 
            oUsuarioFromtDTO.ApellidoMaterno = UsuarioDTO.ApellidoMaterno;
            oUsuarioFromtDTO.Email = UsuarioDTO.Email;
            oUsuarioFromtDTO.TipoDocumento = UsuarioDTO.TipoDocumento;
            oUsuarioFromtDTO.NumeroDocumento = UsuarioDTO.NumeroDocumento;
            oUsuarioFromtDTO.Telefono = UsuarioDTO.Telefono;

            await _usuarioRepository.Delete(oUsuario);

            return oUsuarioFromtDTO;

        }

        public async Task<UsuarioFrontDTO> UpdatePasswordUsuario(UsuarioPasswordDTO UsuarioPasswordDTO)
        {
            string sError = "";

            Usuario oUsuarioActual = new(){
                Email = UsuarioPasswordDTO.Email,
            };

            UsuarioFrontDTO oUsuarioFrontDTO = new(); 

            oUsuarioActual = await GetUsuarioPorEmail(UsuarioPasswordDTO.Email);

            List<string> oListToken = [];
       
            oListToken.Add(oUsuarioActual.Password);
            oListToken.Add(oUsuarioActual.Key1);
            oListToken.Add(oUsuarioActual.Key2);
            
            //Decrypt the password
            string sDecryptedPassword = _cryptoService.IDecrypt(oListToken);

            if (UsuarioPasswordDTO.Password != sDecryptedPassword){
                sError = "UsuarioService.UpdatePasswordUsuario: La contraseña actual no coincide" ;
                log.Error(sError);
                return null;
            }

            if (UsuarioPasswordDTO.PasswordNuevo != UsuarioPasswordDTO.PasswordNuevoConfirmado){
                sError = "UsuarioService.UpdatePasswordUsuario: La contraseña a actualizar no coincide con su confirmación." ;
                log.Error(sError);
                return null;
            };

            //Encrypt the OpcionId con el ID devuelto
            //List<TokenDTO> oListToken = [];
            oListToken = _cryptoService.IEncrypt(UsuarioPasswordDTO.PasswordNuevo);

            oUsuarioActual.Password = oListToken[0];
            oUsuarioActual.Key1 = oListToken[1];
            oUsuarioActual.Key2 = oListToken[2];
            oUsuarioActual.AuditoriaUsuarioModificacion = UsuarioPasswordDTO.AuditoriaUsuario; 
            oUsuarioActual.AuditoriaFechaModificacion = DateTime.Now;

            await _usuarioRepository.Put(oUsuarioActual);
            
            oUsuarioActual = await GetUsuario(oUsuarioActual.UsuarioId);

            oUsuarioFrontDTO.UsuarioId = oUsuarioActual.UsuarioId;
            oUsuarioFrontDTO.Nombres = oUsuarioActual.Nombres;
            oUsuarioFrontDTO.ApellidoPaterno = oUsuarioActual.ApellidoPaterno; 
            oUsuarioFrontDTO.ApellidoMaterno = oUsuarioActual.ApellidoMaterno;
            oUsuarioFrontDTO.Email = oUsuarioActual.Email;
            oUsuarioFrontDTO.TipoDocumento = oUsuarioActual.TipoDocumento;
            oUsuarioFrontDTO.NumeroDocumento = oUsuarioActual.NumeroDocumento;
            oUsuarioFrontDTO.Telefono = oUsuarioActual.Telefono;            

            return oUsuarioFrontDTO;

        }
    }
}
