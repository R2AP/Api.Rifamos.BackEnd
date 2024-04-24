using System.Security.Cryptography;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Adapter;

namespace Api.Rifamos.BackEnd.Domain.Services{
    public class CryptoService : ICryptoService
    {
        public CryptoService(IConfiguration configuration){}

        public List<string> IEncrypt (string sValorEncriptar){

            List<string> oListToken = [];

            byte[] oKey = new byte[16];
            byte[] oIV = new byte[16];

            using(RandomNumberGenerator rng = RandomNumberGenerator.Create()) {
            rng.GetBytes(oKey);
            rng.GetBytes(oIV);
            }

            byte[] oEncryptedOpcionId = Encrypt(sValorEncriptar, oKey, oIV);

            string sKey; 

            sKey = Convert.ToBase64String(oEncryptedOpcionId);
            oListToken.Add(sKey);

            sKey = Convert.ToBase64String(oKey);
            oListToken.Add(sKey);

            sKey = Convert.ToBase64String(oIV);
            oListToken.Add(sKey);

            return oListToken;

        }

        public string IDecrypt (List<string> oListToken){

            // Validamos que el token sea el correcto 
            byte[] oKey = Convert.FromBase64String(oListToken[1]);
            byte[] oIV = Convert.FromBase64String(oListToken[2]);
            byte[] oEncryptedPassword = Convert.FromBase64String(oListToken[0]);

            string sDecryptedPassword = Decrypt(oEncryptedPassword, oKey, oIV);

            return sDecryptedPassword;

        }

        private static byte[] Encrypt(string simpletext, byte[] key, byte[] iv)
        {
            byte[] cipheredtext;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);
                using MemoryStream memoryStream = new();
                using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new(cryptoStream))
                {
                    streamWriter.Write(simpletext);
                }

                cipheredtext = memoryStream.ToArray();
            }
            return cipheredtext;
        }

        private static string Decrypt(byte[] cipheredtext, byte[] key, byte[] iv)
        {
            string simpletext = String.Empty;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
                using MemoryStream memoryStream = new(cipheredtext);
                using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
                using StreamReader streamReader = new(cryptoStream);
                simpletext = streamReader.ReadToEnd();
            }
            return simpletext;
        }

    }
}
