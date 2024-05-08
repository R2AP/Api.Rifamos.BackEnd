using QRCoder;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using System.Buffers.Text;

namespace Api.Rifamos.BackEnd.Domain.Services{
    public class QRService : IQRService
    {

        public QRService(IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            //_QRRepository = QRRepository;
            // _configuration = configuration;
            // _environment = environment;
        }

        public string GetQR(string QRId)
        //public byte[] GetQR(string QRId)
        {

            // Decodificamos la url para eliminar los valores ASCII Encoding Reference
            QRId = System.Web.HttpUtility.UrlDecode(QRId);
            
            Console.WriteLine("The URL dencoding the given URL is: " + System.Web.HttpUtility.UrlDecode(QRId));

            var oQRGenerator = new QRCodeGenerator();
            var oQRCodeData = oQRGenerator.CreateQrCode(QRId, QRCodeGenerator.ECCLevel.M);
            BitmapByteQRCode oBitmapByteQRCode = new(oQRCodeData);
            var oBitMap = oBitmapByteQRCode.GetGraphic(20);

            using var oMemoryStream = new MemoryStream();
            oMemoryStream.Write(oBitMap);
            byte[] aByteImage = oMemoryStream.ToArray(); 

            return Convert.ToBase64String(aByteImage);

        }
    }
}
