using System.Security.Cryptography;
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

        public byte[] Encrypt(string simpletext, byte[] key, byte[] iv)
        {
            byte[] cipheredtext;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(simpletext);
                        }
    
                        cipheredtext = memoryStream.ToArray();
                    }
                }
            }
            return cipheredtext;
        }

        public string Decrypt(byte[] cipheredtext, byte[] key, byte[] iv)
        {
            string simpletext = String.Empty;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
                using (MemoryStream memoryStream = new MemoryStream(cipheredtext))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            simpletext = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            return simpletext;
        }

    }
}
