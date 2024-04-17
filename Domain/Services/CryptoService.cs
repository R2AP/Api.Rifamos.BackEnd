using System.Security.Cryptography;
using System.Text;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;

namespace Api.Rifamos.BackEnd.Domain.Services{
    public class CryptoService : ICryptoService
    {

        public CryptoService(IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            //_QRRepository = QRRepository;
            // _configuration = configuration;
            // _environment = environment;
        }

        // byte[] Encrypt(string QRId)
        // {
        //     byte[] oFile = new byte[1024];
        //     return oFile;

        // };  

        // byte[] Decrypt(string QRId){
        //     byte[] oFile = new byte[1024];
        //     return oFile;
        // };

    }
}
