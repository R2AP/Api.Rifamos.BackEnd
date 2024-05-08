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
        private readonly IEmailService _emailService;
        private readonly ICryptoService _cryptoService;
        private readonly IConfiguration _configuration;
        private static readonly ILog log = LogManager.GetLogger(typeof(UsuarioService));

        public UsuarioService(
                            IUsuarioRepository usuarioRepository,
                            ICryptoService cryptoService,
                            IEmailService emailService,                            
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _usuarioRepository = usuarioRepository;
            _cryptoService = cryptoService;
            _emailService = emailService;
            _configuration = configuration;
            // _environment = environment;
        }

        readonly string sServicio = "UsuarioService.GetUsuarioPorEmail: ";

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
        private static bool CheckPassword (string oPassword)
        {
            
            bool oUpper = false, oLower = false, oDigit = false, oSpecial = false, oCheck = false;

            for(int i = 0; i < oPassword.Length; i++ ){
                if(Char.IsUpper(oPassword, i))
                {
                    oUpper = true;
                }
                else if(Char.IsLower(oPassword,i))
                {
                    oLower = true;
                }
                else if(Char.IsDigit(oPassword,i))
                {
                    oDigit = true;
                }
                else{
                    oSpecial = true;
                }

            }

            if (oUpper && oLower && oDigit && oSpecial && oPassword.Length >= 8)
            {
                oCheck = true;    
            } 

            return oCheck;

        }

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

            //Verificamos si el password cumple con las siguientes caraterísticas:
            //Tenga Mayúsculas, Minusculas, Digitos, Caracteres Especiales y Más de 8 caracteres.
            bool oCheckPassword = CheckPassword(oUsuarioDTO.Password);

            if (!oCheckPassword)
            {
                oUsuarioFrontDTO.Error = true;
                oUsuarioFrontDTO.Mensaje = "El password ingresado no cumple con los criterios: [Al menos un mayúscula][Al menos una minúscula][Al menos un dígito][Al menos un caracter especial][Más de 8 caracteres de longitud]";
                log.Info(sServicio + oUsuarioFrontDTO.Mensaje);
                return oUsuarioFrontDTO;
            }

            oUsuarioActual = await GetUsuarioPorEmail(oUsuarioDTO.Email);

            //Verificamos si la cuenta existe, en caso exista se da por terminado el proceso
            if (oUsuarioActual != null){
                oUsuarioFrontDTO.Error = true;
                oUsuarioFrontDTO.Mensaje = "La cuenta " + oUsuarioDTO.Email + " ya existe" ;
                log.Info(sServicio + oUsuarioFrontDTO.Mensaje);
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
                log.Error(sServicio + oUsuarioFrontDTO.Mensaje);
                return oUsuarioFrontDTO;
            }

            if (UsuarioPasswordDTO.PasswordNuevo != UsuarioPasswordDTO.PasswordNuevoConfirmado){
                oUsuarioFrontDTO.Error = true;
                oUsuarioFrontDTO.Mensaje = "La nueva contraseña no coincide con la contraseña de confirmación.";
                log.Error(sServicio + oUsuarioFrontDTO.Mensaje);
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

        public async Task<UsuarioFrontDTO> RecuperarPassword(string oEmail)
        {

            Usuario oUsuarioActual = await GetUsuarioPorEmail(oEmail);
            UsuarioFrontDTO oUsuarioFrontDTO = new();
            List<string> oListToken = [];
            string sPasswordNuevo = string.Empty; 

            if (oUsuarioActual == null)
            {
                oUsuarioFrontDTO.Error = true;
                oUsuarioFrontDTO.Mensaje = "Verifique, la cuenta de correo no se encuentra registrada.";
                log.Error(sServicio + oUsuarioFrontDTO.Mensaje);
                return oUsuarioFrontDTO;
            }

            byte[] oPasswordNuevo = new byte[16];

            using(RandomNumberGenerator rng = RandomNumberGenerator.Create()) {
            rng.GetBytes(oPasswordNuevo);
            }

            sPasswordNuevo = Convert.ToBase64String(oPasswordNuevo);

            //Encrypt the OpcionId con el ID devuelto
            oListToken = _cryptoService.IEncrypt(sPasswordNuevo);

            oUsuarioActual.Password = oListToken[0];
            oUsuarioActual.Key1 = oListToken[1];
            oUsuarioActual.Key2 = oListToken[2];
            oUsuarioActual.AuditoriaUsuarioModificacion = oUsuarioActual.UsuarioId.ToString(); 
            oUsuarioActual.AuditoriaFechaModificacion = DateTime.Now;

            oUsuarioActual = await Update(oUsuarioActual);

            //Obtenemos la plantilla de envío de email 
            string path = Directory.GetCurrentDirectory();
            StreamReader oEmailBody = new($"{path}\\template\\EmailRecuperarPassword.html");

            string oText = oEmailBody.ReadToEnd();
            oEmailBody.Close();

            //Reemplazamos los valores dinámicos
            oText = oText.Replace("!#Nombre#!", oUsuarioActual.Nombres);
            oText = oText.Replace("!#Email#!", oUsuarioActual.Email);
            oText = oText.Replace("!#Password#!", sPasswordNuevo);
        
            EmailDTO oEmailDTO = new()
            {
                EmailFrom = _configuration["Email:EmailFrom"],
                EmailTo = oUsuarioActual.Email,
                EmailPassword = _configuration["Email:EmailPassword"],
                EmailSubject = "RifamosTodo.online | Recuperar Contraseña",
                EmailBody = oText
            };

            //Invocamos el método de envío de correo.
            bool oSendEmailGmail = _emailService.SendEmailGmail(oEmailDTO);

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
