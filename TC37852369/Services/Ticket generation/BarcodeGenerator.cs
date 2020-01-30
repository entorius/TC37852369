using IronBarCode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;

using ZXing.Common;
using ZXing;
using ZXing.QrCode;
using ZXing.Rendering;

namespace TC37852369.Services.Ticket_generation
{
    public class BarcodeGenerator
    {
        LastEntityIdentificationNumberServices lastEntityIdentificationNumberServices =
            new LastEntityIdentificationNumberServices();
        public static string workingDirectory = Environment.CurrentDirectory;
        QrCodeEncodingOptions options = new QrCodeEncodingOptions();
        ZXing.BarcodeWriter writer = new ZXing.BarcodeWriter();

        public BarcodeGenerator()
        {
            options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = 250,
                Height = 250,
                
            };
            
            writer.Format = BarcodeFormat.QR_CODE;
            writer.Options = options;
        }
        public Image generateQRBarcode(string textToConvert, Color color)
        {
            var MyBarCode = IronBarCode.BarcodeWriter.CreateBarcode(textToConvert, BarcodeWriterEncoding.QRCode);
            //MyBarCode.AddAnnotationTextAboveBarcode("Product URL:");
            MyBarCode.AddBarcodeValueTextBelowBarcode();
            MyBarCode.SetMargins(100);
            //MyBarCode.ChangeBarCodeColor(Color.Green);
            // Save as HTML
            MyBarCode.SaveAsPng(Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\barcode.png");
            return MyBarCode.Image;
        }

        public Image generateQrBarcodeZXing(string textToConvert, Color color)
        {
            if (String.IsNullOrWhiteSpace(textToConvert) || String.IsNullOrEmpty(textToConvert))
            {
                Console.WriteLine("Text not found");
                return null;
            }
            else
            {
                var qr = new ZXing.BarcodeWriter
                {
                    Renderer = new BitmapRenderer
                    {
                        Foreground = color
                    }
                };
                qr.Options = options;
                qr.Format = ZXing.BarcodeFormat.QR_CODE;
                var result = new Bitmap(qr.Write(textToConvert));
                return result;
            }
            
        }
        public async Task<string> generateBarcodeNumber(string barcode)
        {
            LastIdentificationNumber id = await lastEntityIdentificationNumberServices.getBarcodeLastIdentificationNumber();
            int howMany0ToGenerate = 7 - barcode.Length;

            string generatedBarcode = "IL" + id.id.ToString();
            for(int i=0;i< howMany0ToGenerate; i++)
            {
                generatedBarcode = generatedBarcode + "0";
            }
            generatedBarcode = generatedBarcode + barcode;
            return generatedBarcode;
        }
    }
}
