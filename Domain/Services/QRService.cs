using QRCoder;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;

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

        //public string GetQR(string QRId)
        public byte[] GetQR(string QRId)
        {
            
            var oQRGenerator = new QRCodeGenerator();
            var oQRCodeData = oQRGenerator.CreateQrCode(QRId, QRCodeGenerator.ECCLevel.H);
            BitmapByteQRCode oBitmapByteQRCode = new BitmapByteQRCode(oQRCodeData);
            var oBitMap = oBitmapByteQRCode.GetGraphic(20);

            using var oMemoryStream = new MemoryStream();
            oMemoryStream.Write(oBitMap);
            byte[] aByteImage = oMemoryStream.ToArray(); 

            //return  Convert.ToBase64String(aByteImage);
            return aByteImage;

        }
    }
}
