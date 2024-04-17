using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using System.Security.Cryptography;
using System.Text;

namespace Api.Rifamos.BackEnd.Domain.Services{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICryptoService _cryptoService;
        
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

        public async Task<Usuario>GetUsuario(Int32 UsuarioId)
        {
            return await _usuarioRepository.Get(UsuarioId);
        }

        public async Task<Usuario>InsertUsuario(UsuarioDTO UsuarioDTO, string Password)
        {

            byte[] key = new byte[16];
            byte[] iv = new byte[16];

            using(RandomNumberGenerator rng = RandomNumberGenerator.Create()) {
            rng.GetBytes(key);
            rng.GetBytes(iv);
            }            

            //Encrypt the password
            byte[] encryptedPassword = _cryptoService.Encrypt(Password, key, iv);
            string encryptedPasswordString = Convert.ToBase64String(encryptedPassword);
            
            Usuario oUsuario = new Usuario(){

                UsuarioId = UsuarioDTO.UsuarioId,
                Nombres = UsuarioDTO.Nombres, 
                ApellidoPaterno = UsuarioDTO.ApellidoPaterno, 
                ApellidoMaterno = UsuarioDTO.ApellidoMaterno,
                Email = UsuarioDTO.Email,
                Password = encryptedPassword,
                Key1 = key,
                Key2 = iv,
                TipoDocumento = UsuarioDTO.TipoDocumento,
                NumeroDocumento = UsuarioDTO.NumeroDocumento,
                Telefono = UsuarioDTO.Telefono,
                AuditoriaUsuarioIngreso = UsuarioDTO.AuditoriaUsuarioIngreso, 
                AuditoriaFechaIngreso = DateTime.Now 
                
            };

            await _usuarioRepository.Post(oUsuario);

            return oUsuario;

        }

        public async Task<Usuario>UpdateUsuario(UsuarioDTO UsuarioDTO)
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
            oUsuario.AuditoriaUsuarioIngreso = UsuarioDTO.AuditoriaUsuarioIngreso;
            oUsuario.AuditoriaUsuarioModificacion = UsuarioDTO.AuditoriaUsuarioModificacion; 
            oUsuario.AuditoriaFechaModificacion = DateTime.Now;

            await _usuarioRepository.Put(oUsuario);

            return oUsuario;

        }

        public async Task<Usuario>DeleteUsuario(Int32 UsuarioId)
        {

            Usuario oUsuario = await _usuarioRepository.Get(UsuarioId);

            await _usuarioRepository.Delete(oUsuario);

            return oUsuario;

        }
        
        public async Task<Usuario>LoginUsuario(string Usuario, string Password)
        {

            Usuario oUsuario = await _usuarioRepository.LoginUsuario(Usuario, Password);

            byte[] key = new byte[16];
            byte[] iv = new byte[16];
            byte[] encryptedPassword = oUsuario.Password;
            key = oUsuario.Key1;
            iv = oUsuario.Key2;

            //Decrypt the password
            string decryptedPassword = _cryptoService.Decrypt(encryptedPassword, key, iv);
            
            if (Password == decryptedPassword)
            {
                Console.WriteLine("Ok");
            }
            else
            {
                Console.WriteLine("No Ok");
            }

            await _usuarioRepository.LoginUsuario(Usuario, Password);

            return oUsuario;

        }
          
    }

}
