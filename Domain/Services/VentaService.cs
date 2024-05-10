using Api.Rifamos.BackEnd.Adapter;
using Api.Rifamos.BackEnd.Domain.Interfaces.Repositories;
using Api.Rifamos.BackEnd.Domain.Interfaces.Services;
using Api.Rifamos.BackEnd.Domain.Models;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Drawing.Imaging;
//using Newtonsoft.Json;

namespace Api.Rifamos.BackEnd.Domain.Services{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IRifaService _rifaService;
        private readonly IPremioService _premioService;        
        private readonly IOpcionService _opcionService;
        private readonly IPrecioService _precioService;
        private readonly IQRService _qrService;
        private readonly IUsuarioService _usuarioService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        // private IHostingEnvironment _environment;
        //private static readonly ILog log = LogManager.GetLogger(typeof(UltimusService));

        public VentaService(IVentaRepository ventaRepository,
                            IRifaService rifaService,
                            IPremioService premioService,
                            IOpcionRepository opcionRepository,
                            IOpcionService opcionService,
                            IPrecioService precioService,
                            IQRService qrService,
                            IUsuarioService usuarioService,
                            IEmailService emailService,
                            IConfiguration configuration/*,
                            IHostingEnvironment environment*/
                            )
        {
            _ventaRepository = ventaRepository;
            _rifaService = rifaService;
            _premioService = premioService;
            _opcionService = opcionService;
            _precioService = precioService;
            _qrService = qrService;
            _usuarioService = usuarioService;
            _emailService = emailService;
            _configuration = configuration;
            // _environment = environment;
        }

        public async Task<Ventum> Get(Int32 oVentumId) => await _ventaRepository.Get(oVentumId);

        public async Task<Ventum> Insert(Ventum oVentum)
        {
     
            await _ventaRepository.Post(oVentum);

            return await Get(oVentum.VentaId);

        }

        public async Task<Ventum> Update(Ventum oVentum)
        {

            await _ventaRepository.Put(oVentum);

            return await Get(oVentum.VentaId);

        }

        public async Task<Ventum> Delete(Int32 oVentumId) 
        {

            Ventum oVentum = await Get(oVentumId);

            await _ventaRepository.Delete(oVentum);

            return oVentum;

        }

        //Métodos Complementarios
        public async Task<Ventum> GetVenta(Int32 oVentaId) => await _ventaRepository.Get(oVentaId);

        private async Task<Ventum> InsertVenta(VentaDTO oVentaDTO)
        {
     
            Ventum oVenta = new()
            {
                VentaId = oVentaDTO.VentaId,
                OpcionId = oVentaDTO.OpcionId,
                AuditoriaUsuarioIngreso = oVentaDTO.AuditoriaUsuario,  
                AuditoriaFechaIngreso = DateTime.Now
            };

            return await Insert(oVenta);

        }

        private async Task<Ventum> UpdateVenta(VentaDTO oVentaDTO)
        {

            Ventum oVenta = new()
            {
                VentaId = oVentaDTO.VentaId,
                OpcionId = oVentaDTO.OpcionId,
                AuditoriaUsuarioModificacion = oVentaDTO.AuditoriaUsuario,  
                AuditoriaFechaModificacion = DateTime.Now
            };

            return await Update(oVenta);

        }

        private async Task<Ventum> DeleteVenta(Int32 oVentaId)
        {

            Ventum oVenta = await Get(oVentaId);

            await _ventaRepository.Delete(oVenta);

            return oVenta;

        }

        public async Task<VentaFrontDTO> GetVentaOpcion(Int32 oVentaId)
        {
            
            Ventum oVenta = await Get(oVentaId);

            Opcion oOpcion = await _opcionService.Get(oVenta.OpcionId);

            VentaFrontDTO oVentaFrontDTO = new()
            {
                OpcionId = oOpcion.OpcionId,
                RifaId = oOpcion.RifaId,
                CantidadOpciones = oOpcion.CantidadOpciones,
                TokenOpcion = oOpcion.TokenOpcion,
                EstadoOpcion = oOpcion.EstadoOpcion,
                VentaId = oVenta.VentaId,
                TipoComprobante = oVenta.TipoComprobante,
                SerieComprobante = oVenta.SerieComprobante,
                NumeroComprobante = oVenta.NumeroComprobante,
                Moneda = oVenta.Moneda,
                Monto = oVenta.Monto,
                EstadoVenta = oVenta.EstadoVenta
            };

            return oVentaFrontDTO;

        } 

        public async Task<VentaFrontDTO> InsertVentaOpcion(VentaDTO oVentaDTO)
        {

            //Obtenemos la ruta principal para poder leer las plantillas y archivos
            string path = Directory.GetCurrentDirectory();
            string oNombreArchivo = string.Empty;
            string oExtensionArchivoBPM = ".bpm";
            string oExtensionArchivoPNG = ".png";

            //Registramos las Opciones y la Venta
            OpcionDTO oOpcionDTO = new(){
                OpcionId = oVentaDTO.OpcionId, 
                RifaId = oVentaDTO.RifaId,
                UsuarioId = oVentaDTO.UsuarioId,
                CantidadOpciones = oVentaDTO.CantidadOpciones,
                TokenOpcion = "0",
                TokenKey1 = "0",
                TokenKey2 = "0",
                EstadoOpcion = Int32.Parse(_configuration["EstadoOpcion:Registrada"]),
                AuditoriaUsuarioIngreso = oVentaDTO.AuditoriaUsuario,
                AuditoriaFechaIngreso = DateTime.Now
            };

            //Insertamos la opción que devuelve el registro recién insertado
            Opcion oOpcion = await _opcionService.InsertOpcion(oOpcionDTO);

            //Obtenemos el precio de la Rifa para registrarlo en la venta
            Precio oPrecio = await _precioService.GetPrecioUnitario(oVentaDTO.RifaId);

            Ventum oVenta = new()
            {
                VentaId =  oVentaDTO.VentaId,
                OpcionId = oOpcion.OpcionId, // Relación entre Opción y Venta
                TipoComprobante = Int32.Parse(_configuration["TipoComprobante:Boleta"]),
                SerieComprobante = _configuration["SeriComprobante:SeriComprobanteBoleta"],
                NumeroComprobante = "0", //VentaDTO.NumeroComprobante,
                Moneda = Int32.Parse(_configuration["Moneda:Soles"]),
                Monto = oPrecio.PrecioUnitario *  oVentaDTO.CantidadOpciones, // VentaDTO.Monto,
                EstadoVenta = Int32.Parse(_configuration["EstadoVenta:Registrada"]),
                AuditoriaUsuarioIngreso = oVentaDTO.AuditoriaUsuario,
                AuditoriaFechaIngreso = DateTime.Now
            };

            //Insertamos la venta, el métoo devuelve el registro recién insertado
            oVenta = await Insert(oVenta);

            //Envío de email con la confirmación de la compra
            //Seleccionamos las entidades que permiten generar el email de comprobación de compra de opciones
            Rifa oRifa = await _rifaService.Get(oOpcion.RifaId);
            List<Premio> oListPremio = await _premioService.GetListPremio(oOpcion.RifaId);
            Usuario oUsuario = await _usuarioService.GetUsuario(oOpcion.UsuarioId);

            //Obtenemos la plantilla de envío de email 
            StreamReader oEmailBody = new($"{path}\\template\\EmailConfirmacionCompra.html");
            string oText = oEmailBody.ReadToEnd();
            oEmailBody.Close();

            //Reemplazamos los valores dinámicos
            oText = oText.Replace("!#Nombre#!", oUsuario.Nombres);
            oText = oText.Replace("!#CantidadOpciones#!", oVentaDTO.CantidadOpciones.ToString());
            oText = oText.Replace("!#NombreRifa#!", oRifa.RifaDescripcion);            
            oText = oText.Replace("!#Premio1#!", oListPremio[0].PremioDescripcion);
            oText = oText.Replace("!#Premio2#!", oListPremio[1].PremioDescripcion);
            oText = oText.Replace("!#Premio3#!", oListPremio[2].PremioDescripcion);
            oText = oText.Replace("!#Fecha#!", oRifa.FechaSorteo.ToString());
            oText = oText.Replace("!#Hora#!", oRifa.HoraSorteo.ToString());

            //Creamos el QR para ser enviado como atachment
            byte[] oQR = _qrService.GetQR("http://localhost:5175/api/QR/obtener-QR/" + oOpcion.TokenOpcion);

            //save to file
            oNombreArchivo = "Ticket-" + oOpcion.OpcionId.ToString();
            File.WriteAllBytes($"{path}\\qr\\" + oNombreArchivo + oExtensionArchivoPNG, oQR);

            //save like png
            // Bitmap oBitMap = new($"{path}\\qr\\" + oNombreArchivo + oExtensionArchivoBPM);
            // MemoryStream oStream = new MemoryStream();
            // oBitMap.Save(oStream, ImageFormat.Png);

            //Reemplazamos en la plantilla el valor del nombre del archivo
            oText = oText.Replace("!#FileName#!", oNombreArchivo);

            //Creamos el objeto de la clase Email
            EmailDTO oEmailDTO = new()
            {
                EmailFrom = _configuration["Email:EmailFrom"],
                EmailTo = oUsuario.Email,
                EmailPassword = _configuration["Email:EmailPassword"],
                EmailSubject = "RifamosTodo.online | Compra de Opciones",
                EmailBody = oText,
                EmailAttachment = string.Empty,
                EmailContentId = oNombreArchivo,
                EmailAttachmentContent = $"{path}\\qr\\" + oNombreArchivo + ".png",
            };

            //Invocamos el método de envío de correo.
            bool oSendEmailGmail = _emailService.SendEmailGmail(oEmailDTO);

            //delete to file
            File.Delete($"{path}\\qr\\" + oNombreArchivo + oExtensionArchivoBPM);

            //Devolvemos la entidad Opción + Venta
            VentaFrontDTO oVentaFrontDTO = new()
            {
                OpcionId = oOpcion.OpcionId,
                RifaId = oOpcion.RifaId,
                CantidadOpciones = oOpcion.CantidadOpciones,
                TokenOpcion = oOpcion.TokenOpcion,
                EstadoOpcion = oOpcion.EstadoOpcion,
                VentaId = oVenta.VentaId,
                TipoComprobante = oVenta.TipoComprobante,
                SerieComprobante = oVenta.SerieComprobante,
                NumeroComprobante = oVenta.NumeroComprobante,
                Moneda = oVenta.Moneda,
                Monto = oVenta.Monto,
                EstadoVenta = oVenta.EstadoVenta
            };

            return oVentaFrontDTO;

        }

    }

}
