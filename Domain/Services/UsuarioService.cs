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

        //Métodos Básicos
        public async Task<Usuario> Get(Int32 oUsuarioId) => await _usuarioRepository.Get(oUsuarioId);

        public async Task<Usuario> Insert(Usuario oUsuario)
        {
     
            await _usuarioRepository.Post(oUsuario);

            return await Get(oUsuario.UsuarioId);

        }

        public async Task<Usuario> Update(Usuario oUsuario)
        {

            await _usuarioRepository.Put(oUsuario);

            return await Get(oUsuario.UsuarioId);

        }

        public async Task<Usuario> Delete(Int32 oUsuarioId) 
        {

            Usuario oUsuario = await Get(oUsuarioId);

            await _usuarioRepository.Delete(oUsuario);

            return oUsuario;

        }

        //Métodos Complementarios
        public async Task<Usuario> GetUsuario(Int32 oUsuarioId)
        {
            return await Get(oUsuarioId);
        }

        public async Task<Usuario> GetUsuarioPorEmail(string Email)
        {
            return await _usuarioRepository.GetUsuarioPorEmail(Email);
        }

        public async Task<UsuarioFrontDTO> InsertUsuario(UsuarioDTO oUsuarioDTO)
        {

            Usuario oUsuarioActual = new();
            UsuarioFrontDTO oUsuarioFrontDTO = new();

            oUsuarioActual = await GetUsuarioPorEmail(oUsuarioDTO.Email);

            //Verificamos si la cuenta, en caso exista se da por termionado el proceso
            if (oUsuarioActual != null){
                oUsuarioFrontDTO.Error = true;
                oUsuarioFrontDTO.Mensaje = "La cuenta [" + oUsuarioDTO.Email + "] ya existe";
                sError = "UsuarioService.InsertUsuario: La cuenta " + oUsuarioDTO.Email + " ya existe" ;
                log.Info(string.Format("UsuarioService.GetUsuarioPorEmail: La cuenta {0} ya existe", oUsuarioDTO.Email));
                return oUsuarioFrontDTO;
            }

            //Encrypt the OpcionId con el ID devuelto
            //List<TokenDTO> oListToken = [];
            List<string> oListToken = [];
            oListToken = _cryptoService.IEncrypt(oUsuarioDTO.Password);

            Usuario oUsuario = new(){

                UsuarioId = oUsuarioDTO.UsuarioId,
                Nombres = oUsuarioDTO.Nombres, 
                ApellidoPaterno = oUsuarioDTO.ApellidoPaterno, 
                ApellidoMaterno = oUsuarioDTO.ApellidoMaterno,
                Email = oUsuarioDTO.Email,
                Password = oListToken[0],
                Key1 = oListToken[1],
                Key2 = oListToken[2],
                TipoDocumento = oUsuarioDTO.TipoDocumento,
                NumeroDocumento = oUsuarioDTO.NumeroDocumento,
                Telefono = oUsuarioDTO.Telefono,
                AuditoriaUsuarioIngreso = oUsuarioDTO.AuditoriaUsuario, 
                AuditoriaFechaIngreso = DateTime.Now 
                
            };

            oUsuario = await Insert(oUsuario);

            oUsuarioFrontDTO.UsuarioId = oUsuario.UsuarioId;
            oUsuarioFrontDTO.Nombres = oUsuario.Nombres;
            oUsuarioFrontDTO.ApellidoPaterno = oUsuario.ApellidoPaterno; 
            oUsuarioFrontDTO.ApellidoMaterno = oUsuario.ApellidoMaterno;
            oUsuarioFrontDTO.Email = oUsuario.Email;
            oUsuarioFrontDTO.TipoDocumento = oUsuario.TipoDocumento;
            oUsuarioFrontDTO.NumeroDocumento = oUsuario.NumeroDocumento;
            oUsuarioFrontDTO.Telefono = oUsuario.Telefono;

            return oUsuarioFrontDTO;

        }

        public async Task<UsuarioFrontDTO> UpdateUsuario(UsuarioDTO oUsuarioDTO)
        {

            Usuario oUsuario = await Get(oUsuarioDTO.UsuarioId);

            //El ID no se modifica
            oUsuario.UsuarioId = oUsuario.UsuarioId;
            oUsuario.Nombres = oUsuarioDTO.Nombres;
            oUsuario.ApellidoPaterno = oUsuarioDTO.ApellidoPaterno;
            oUsuario.ApellidoMaterno = oUsuarioDTO.ApellidoMaterno;
            //El correo no se cambia actualiza o modifica
            oUsuario.Email = oUsuario.Email;
            //El password no se cambia por este medio
            oUsuario.Password = oUsuario.Password;
            oUsuario.TipoDocumento = oUsuarioDTO.TipoDocumento;
            oUsuario.NumeroDocumento = oUsuarioDTO.NumeroDocumento;
            oUsuario.Telefono = oUsuarioDTO.Telefono;
            oUsuario.AuditoriaUsuarioModificacion = oUsuarioDTO.AuditoriaUsuario; 
            oUsuario.AuditoriaFechaModificacion = DateTime.Now;

            oUsuario = await Update(oUsuario);

            UsuarioFrontDTO oUsuarioFrontDTO = new()
            {
                UsuarioId = oUsuario.UsuarioId,
                Nombres = oUsuario.Nombres,
                ApellidoPaterno = oUsuario.ApellidoPaterno,
                ApellidoMaterno = oUsuario.ApellidoMaterno,
                Email = oUsuario.Email,
                TipoDocumento = oUsuario.TipoDocumento,
                NumeroDocumento = oUsuario.NumeroDocumento,
                Telefono = oUsuario.Telefono
            };

            return oUsuarioFrontDTO;

        }

        public async Task<UsuarioFrontDTO> DeleteUsuario(Int32 UsuarioId)
        {

            Usuario oUsuario = await Get(UsuarioId); 

            await Delete(UsuarioId);

            UsuarioFrontDTO oUsuarioFrontDTO = new()
            {
                UsuarioId = oUsuario.UsuarioId,
                Nombres = oUsuario.Nombres,
                ApellidoPaterno = oUsuario.ApellidoPaterno,
                ApellidoMaterno = oUsuario.ApellidoMaterno,
                Email = oUsuario.Email,
                TipoDocumento = oUsuario.TipoDocumento,
                NumeroDocumento = oUsuario.NumeroDocumento,
                Telefono = oUsuario.Telefono
            };

            return oUsuarioFrontDTO;

        }

        public async Task<UsuarioFrontDTO> UpdatePasswordUsuario(UsuarioPasswordDTO UsuarioPasswordDTO)
        {

            Usuario oUsuarioActual = await GetUsuarioPorEmail(UsuarioPasswordDTO.Email);

            List<string> oListToken = [];
       
            oListToken.Add(oUsuarioActual.Password);
            oListToken.Add(oUsuarioActual.Key1);
            oListToken.Add(oUsuarioActual.Key2);
            
            //Decrypt the password
            string sDecryptedPassword = _cryptoService.IDecrypt(oListToken);

            UsuarioFrontDTO oUsuarioFrontDTO = new(); 

            if (UsuarioPasswordDTO.Password != sDecryptedPassword){
                oUsuarioFrontDTO.Error = true;
                oUsuarioFrontDTO.Mensaje = "La contraseña actual no coincide";
                sError = "UsuarioService.UpdatePasswordUsuario: La contraseña actual no coincide" ;
                log.Error(sError);
                return oUsuarioFrontDTO;
            }

            if (UsuarioPasswordDTO.PasswordNuevo != UsuarioPasswordDTO.PasswordNuevoConfirmado){
                oUsuarioFrontDTO.Error = true;
                oUsuarioFrontDTO.Mensaje = "La nueva contraseña no coincide con la contraseña de confirmación.";
                sError = "UsuarioService.UpdatePasswordUsuario: La nueva contraseña no coincide con la contraseña de confirmación." ;
                log.Error(sError);
                return oUsuarioFrontDTO;
            };

            //Encrypt the OpcionId con el ID devuelto
            oListToken = _cryptoService.IEncrypt(UsuarioPasswordDTO.PasswordNuevo);

            oUsuarioActual.Password = oListToken[0];
            oUsuarioActual.Key1 = oListToken[1];
            oUsuarioActual.Key2 = oListToken[2];
            oUsuarioActual.AuditoriaUsuarioModificacion = UsuarioPasswordDTO.AuditoriaUsuario; 
            oUsuarioActual.AuditoriaFechaModificacion = DateTime.Now;

            oUsuarioActual = await Update(oUsuarioActual);

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
