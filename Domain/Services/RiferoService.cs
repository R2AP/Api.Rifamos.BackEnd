using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using System.Security.Cryptography;
using log4net;

namespace Api.Rifamos.BackEnd.Domain.Services{
    public class RiferoService : IRiferoService
    {
        private readonly IRiferoRepository _RiferoRepository;
        private readonly IEmailService _emailService;
        private readonly ICryptoService _cryptoService;
        private readonly IConfiguration _configuration;
        private static readonly ILog log = LogManager.GetLogger(typeof(RiferoService));

        public RiferoService(
                            IRiferoRepository RiferoRepository,
                            ICryptoService cryptoService,
                            IEmailService emailService,                            
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _RiferoRepository = RiferoRepository;
            _cryptoService = cryptoService;
            _emailService = emailService;
            _configuration = configuration;
            // _environment = environment;
        }

        readonly string sServicio = "RiferoService.GetRiferoPorEmail: ";

        //Métodos Básicos
        public async Task<Rifero> Get(Int32 oRiferoId) => await _RiferoRepository.Get(oRiferoId);

        public async Task<Rifero> Insert(Rifero oRifero)
        {
     
            await _RiferoRepository.Post(oRifero);

            return await Get(oRifero.RiferoId);

        }

        public async Task<Rifero> Update(Rifero oRifero)
        {

            await _RiferoRepository.Put(oRifero);

            return await Get(oRifero.RiferoId);

        }

        public async Task<Rifero> Delete(Int32 oRiferoId) 
        {

            Rifero oRifero = await Get(oRiferoId);

            await _RiferoRepository.Delete(oRifero);

            return oRifero;

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

        public async Task<Rifero> GetRifero(Int32 oRiferoId)
        {
            return await Get(oRiferoId);
        }

        public async Task<Rifero> GetRiferoPorEmail(string oEmail)
        {
            return await _RiferoRepository.GetRiferoPorEmail(oEmail);
        }

        public async Task<RiferoFrontDTO> InsertRifero(RiferoDTO oRiferoDTO)
        {
            Rifero oRiferoActual = new();
            RiferoFrontDTO oRiferoFrontDTO = new();

            //Verificamos si el password cumple con las siguientes caraterísticas:
            //Tenga Mayúsculas, Minusculas, Digitos, Caracteres Especiales y Más de 8 caracteres.
            bool oCheckPassword = CheckPassword(oRiferoDTO.Password);

            if (!oCheckPassword)
            {
                oRiferoFrontDTO.Error = true;
                oRiferoFrontDTO.Mensaje = "El password ingresado no cumple con los criterios: [Al menos un mayúscula][Al menos una minúscula][Al menos un dígito][Al menos un caracter especial][Más de 8 caracteres de longitud]";
                log.Info(sServicio + oRiferoFrontDTO.Mensaje);
                return oRiferoFrontDTO;
            }

            oRiferoActual = await GetRiferoPorEmail(oRiferoDTO.Email);

            //Verificamos si la cuenta existe, en caso exista se da por terminado el proceso
            if (oRiferoActual != null){
                oRiferoFrontDTO.Error = true;
                oRiferoFrontDTO.Mensaje = "La cuenta " + oRiferoDTO.Email + " ya existe" ;
                log.Info(sServicio + oRiferoFrontDTO.Mensaje);
                return oRiferoFrontDTO;
            }

            //Encrypt the OpcionId con el ID devuelto
            //List<TokenDTO> oListToken = [];
            List<string> oListToken = [];
            oListToken = _cryptoService.IEncrypt(oRiferoDTO.Password);

            Rifero oRifero = new(){

                Nombre = oRiferoDTO.Nombres, 
                ApellidoPaterno = oRiferoDTO.ApellidoPaterno, 
                ApellidoMaterno = oRiferoDTO.ApellidoMaterno,
                Email = oRiferoDTO.Email,
                Password = oListToken[0],
                Key1 = oListToken[1],
                Key2 = oListToken[2],
                TipoDocumentoId = oRiferoDTO.TipoDocumentoID,
                NumeroDocumentoIdentidad = oRiferoDTO.NumeroDocumento,
                Telefono = oRiferoDTO.Telefono,
                AuditoriaFechaIngreso = DateTime.Now 
                
            };

            oRifero = await Insert(oRifero);

            oRiferoFrontDTO.Email = oRifero.Email;
            oRiferoFrontDTO.Nombres = oRifero.Nombre;
            oRiferoFrontDTO.ApellidoPaterno = oRifero.ApellidoPaterno; 
            oRiferoFrontDTO.ApellidoMaterno = oRifero.ApellidoMaterno;
            oRiferoFrontDTO.TipoDocumento = oRifero.TipoDocumentoId;
            oRiferoFrontDTO.NumeroDocumento = oRifero.NumeroDocumentoIdentidad;
            oRiferoFrontDTO.Telefono = oRifero.Telefono;

            return oRiferoFrontDTO;

        }

        public async Task<RiferoFrontDTO> UpdateRifero(RiferoDTO oRiferoDTO)
        {

            Rifero oRifero = await Get(oRiferoDTO.RiferoId);

            //El ID no se modifica
            oRifero.Nombre = oRiferoDTO.Nombres;
            oRifero.ApellidoPaterno = oRiferoDTO.ApellidoPaterno;
            oRifero.ApellidoMaterno = oRiferoDTO.ApellidoMaterno;
            //El correo no se cambia actualiza o modifica
            oRifero.Email = oRifero.Email;
            //El password no se cambia por este medio
            oRifero.Password = oRifero.Password;
            oRifero.TipoDocumentoId = oRiferoDTO.TipoDocumentoID;
            oRifero.NumeroDocumentoIdentidad = oRiferoDTO.NumeroDocumento;
            oRifero.Telefono = oRiferoDTO.Telefono;
            oRifero.AuditoriaUsuarioModificacion = oRiferoDTO.AuditoriaUsuario; 
            oRifero.AuditoriaFechaModificacion = DateTime.Now;

            oRifero = await Update(oRifero);

            RiferoFrontDTO oRiferoFrontDTO = new()
            {
                Email = oRifero.Email,
                Nombres = oRifero.Nombre,
                ApellidoPaterno = oRifero.ApellidoPaterno,
                ApellidoMaterno = oRifero.ApellidoMaterno,
                TipoDocumento = oRifero.TipoDocumentoId,
                NumeroDocumento = oRifero.NumeroDocumentoIdentidad,
                Telefono = oRifero.Telefono
            };

            return oRiferoFrontDTO;

        }

        public async Task<RiferoFrontDTO> DeleteRifero(Int32 oRiferoId)
        {

            Rifero oRifero = await Get(oRiferoId); 

            await Delete(oRiferoId);

            RiferoFrontDTO oRiferoFrontDTO = new()
            {
                Email = oRifero.Email,
                Nombres = oRifero.Nombre,
                ApellidoPaterno = oRifero.ApellidoPaterno,
                ApellidoMaterno = oRifero.ApellidoMaterno,
                TipoDocumento = oRifero.TipoDocumentoId,
                NumeroDocumento = oRifero.NumeroDocumentoIdentidad,
                Telefono = oRifero.Telefono
            };

            return oRiferoFrontDTO;

        }

        public async Task<RiferoFrontDTO> UpdatePasswordRifero(RiferoPasswordDTO RiferoPasswordDTO)
        {

            Rifero oRiferoActual = await GetRiferoPorEmail(RiferoPasswordDTO.Email);

            List<string> oListToken = [];
       
            oListToken.Add(oRiferoActual.Password);
            oListToken.Add(oRiferoActual.Key1);
            oListToken.Add(oRiferoActual.Key2);
            
            //Decrypt the password
            string sDecryptedPassword = _cryptoService.IDecrypt(oListToken);

            RiferoFrontDTO oRiferoFrontDTO = new(); 

            if (RiferoPasswordDTO.Password != sDecryptedPassword){
                oRiferoFrontDTO.Error = true;
                oRiferoFrontDTO.Mensaje = "La contraseña actual no coincide";
                log.Error(sServicio + oRiferoFrontDTO.Mensaje);
                return oRiferoFrontDTO;
            }

            if (RiferoPasswordDTO.PasswordNuevo != RiferoPasswordDTO.PasswordNuevoConfirmado){
                oRiferoFrontDTO.Error = true;
                oRiferoFrontDTO.Mensaje = "La nueva contraseña no coincide con la contraseña de confirmación.";
                log.Error(sServicio + oRiferoFrontDTO.Mensaje);
                return oRiferoFrontDTO;
            };

            //Encrypt the OpcionId con el ID devuelto
            oListToken = _cryptoService.IEncrypt(RiferoPasswordDTO.PasswordNuevo);

            oRiferoActual.Password = oListToken[0];
            oRiferoActual.Key1 = oListToken[1];
            oRiferoActual.Key2 = oListToken[2];
            oRiferoActual.AuditoriaUsuarioModificacion = RiferoPasswordDTO.AuditoriaUsuario; 
            oRiferoActual.AuditoriaFechaModificacion = DateTime.Now;

            oRiferoActual = await Update(oRiferoActual);

            oRiferoFrontDTO.Nombres = oRiferoActual.Nombre;
            oRiferoFrontDTO.ApellidoPaterno = oRiferoActual.ApellidoPaterno; 
            oRiferoFrontDTO.ApellidoMaterno = oRiferoActual.ApellidoMaterno;
            oRiferoFrontDTO.Email = oRiferoActual.Email;
            oRiferoFrontDTO.TipoDocumento = oRiferoActual.TipoDocumentoId;
            oRiferoFrontDTO.NumeroDocumento = oRiferoActual.NumeroDocumentoIdentidad;
            oRiferoFrontDTO.Telefono = oRiferoActual.Telefono;

            return oRiferoFrontDTO;

        }

        public async Task<RiferoFrontDTO> RecuperarPassword(string oEmail)
        {

            Rifero oRiferoActual = await GetRiferoPorEmail(oEmail);
            RiferoFrontDTO oRiferoFrontDTO = new();
            List<string> oListToken = [];
            string sPasswordNuevo = string.Empty; 

            if (oRiferoActual == null)
            {
                oRiferoFrontDTO.Error = true;
                oRiferoFrontDTO.Mensaje = "Verifique, la cuenta de correo no se encuentra registrada.";
                log.Error(sServicio + oRiferoFrontDTO.Mensaje);
                return oRiferoFrontDTO;
            }

            byte[] oPasswordNuevo = new byte[16];

            using(RandomNumberGenerator rng = RandomNumberGenerator.Create()) {
            rng.GetBytes(oPasswordNuevo);
            }

            sPasswordNuevo = Convert.ToBase64String(oPasswordNuevo);

            //Encrypt the OpcionId con el ID devuelto
            oListToken = _cryptoService.IEncrypt(sPasswordNuevo);

            oRiferoActual.Password = oListToken[0];
            oRiferoActual.Key1 = oListToken[1];
            oRiferoActual.Key2 = oListToken[2];
            oRiferoActual.AuditoriaUsuarioModificacion = oRiferoActual.Email; 
            oRiferoActual.AuditoriaFechaModificacion = DateTime.Now;

            oRiferoActual = await Update(oRiferoActual);

            //Obtenemos la plantilla de envío de email 
            string path = Directory.GetCurrentDirectory();
            StreamReader oEmailBody = new($"{path}\\template\\EmailRecuperarPassword.html");

            string oText = oEmailBody.ReadToEnd();
            oEmailBody.Close();

            //Reemplazamos los valores dinámicos
            oText = oText.Replace("!#Nombre#!", oRiferoActual.Nombre);
            oText = oText.Replace("!#Email#!", oRiferoActual.Email);
            oText = oText.Replace("!#Password#!", sPasswordNuevo);
        
            EmailDTO oEmailDTO = new()
            {
                EmailFrom = _configuration["Email:EmailFrom"],
                EmailTo = oRiferoActual.Email,
                EmailPassword = _configuration["Email:EmailPassword"],
                EmailSubject = "RifamosTodo.online | Recuperar Contraseña",
                EmailBody = oText,
                EmailAttachment = string.Empty,
                EmailContentId = string.Empty,
                EmailAttachmentContent = string.Empty
            };

            //Invocamos el método de envío de correo.
            bool oSendEmailGmail = _emailService.SendEmailGmail(oEmailDTO);

            oRiferoFrontDTO.Email = oRiferoActual.Email;
            oRiferoFrontDTO.Nombres = oRiferoActual.Nombre;
            oRiferoFrontDTO.ApellidoPaterno = oRiferoActual.ApellidoPaterno; 
            oRiferoFrontDTO.ApellidoMaterno = oRiferoActual.ApellidoMaterno;
            oRiferoFrontDTO.TipoDocumento = oRiferoActual.TipoDocumentoId;
            oRiferoFrontDTO.NumeroDocumento = oRiferoActual.NumeroDocumentoIdentidad;
            oRiferoFrontDTO.Telefono = oRiferoActual.Telefono;

            return oRiferoFrontDTO;

        }

    }
}