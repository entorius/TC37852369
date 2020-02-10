using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;
using TC37852369.Services.Encoder;
using Xceed.Document.NET;
using Xceed.Words.NET;


namespace TC37852369.Services.Ticket_generation
{
    public class TicketCreation
    {
        private PDFConverter pdfConverter;
        private StringEncoder stringEncoder = new StringEncoder();
        private BarcodeGenerator barcodeGenerator = new BarcodeGenerator(); 
        private Microsoft.Office.Interop.Word.Application MSdoc;
        public static string workingDirectory = Environment.CurrentDirectory;

        string myImageFullPath = Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\websiteQRCode_noFrame.png";
        string LogoFinalImage = Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\logo_final.png";
        string phoneImagePath = Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\phone.png";
        string emailImagePath = Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\mail.png";
        string worldImagePath = Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\internet.png";
        string placeImagePath = Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\enviroment.png";
        string iliniumImage = Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\inlinum-logo.png";
        public TicketCreation()
        {
            
        }
        //creates ticket and returns its path
        private string createTicket(string firstName, string lastName, string company, string format,
            string eventName,string date,string location1Row, string location2Row, string barcode,
           System.Drawing.Color barcodeColor, string savingName)
        {
            string encodedBarcode = StringEncoder.ReturnEncryptedString(barcode);
            System.Drawing.Image barcodeImage = barcodeGenerator.generateQrBarcodeZXing(encodedBarcode, barcodeColor);

            using (DocX document = DocX.Create(@"docs\" + savingName + ".docx"))
            {
                // Set document margins
                document.MarginBottom = 0.7F;
                document.MarginTop = 0.7F;
                document.MarginRight = 35F;
                document.MarginLeft = 35F;

                document.InsertParagraph();
                //add logo image
                addImageToDocument(document, 145, 473, LogoFinalImage, Alignment.center);


                float pageWidth = document.PageWidth * 18;
                //add line through all page
                System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml("#00A86B");
                addLineToDocument(document, Xceed.Document.NET.BorderStyle.Tcbs_dashed,
                    pageWidth, color);

                document.InsertParagraph();

                System.Drawing.Color headerColor = System.Drawing.ColorTranslator.FromHtml("#9E9E9E");
                System.Drawing.Color paragraphColor = System.Drawing.ColorTranslator.FromHtml("#404040");
                System.Drawing.Color linkColor = System.Drawing.ColorTranslator.FromHtml("#E36C0A");
                //Add text to document
                addTextToDocument(document, "Name", Alignment.left, "Calibri", 14, headerColor);
                addTextToDocument(document, firstName + " " + lastName, Alignment.left, "Calibri", 28, paragraphColor);
                document.InsertParagraph();
                addTextToDocument(document, "Company", Alignment.left, "Calibri", 14, headerColor);
                addTextToDocument(document, company, Alignment.left, "Calibri", 16, paragraphColor);
                document.InsertParagraph();
                addTextToDocument(document, "Format", Alignment.left, "Calibri", 14, headerColor);
                addTextToDocument(document, format, Alignment.left, "Calibri", 16, paragraphColor);
                document.InsertParagraph();
                addTextToDocument(document, "Event", Alignment.left, "Calibri", 14, headerColor);
                addTextToDocument(document, eventName, Alignment.left, "Calibri", 16, paragraphColor);
                document.InsertParagraph();
                string dateLocation = "Date\t\t\t\t\t\t\t\t\tLocation";
                string datePlace = date + "\t\t\t\t\t" + location1Row;
                string TimeLocation = "\t\t\t\t\t\t\t\t\t" + location2Row;



                string calendarMap = "Add to calendar\t\t\t\t\t\t\t\t\t\t\tView map";





                addTextToDocument(document, dateLocation, Alignment.left, "Calibri", 14, headerColor);
                addTextToDocument(document, datePlace, Alignment.left, "Calibri", 16, paragraphColor);
                addTextToDocument(document, TimeLocation, Alignment.left, "Calibri", 16, paragraphColor);
                document.InsertParagraph();



                addImageToDocument(document, 250, 250, barcodeImage, Alignment.center);

                addTextToDocument(document, calendarMap, Alignment.left, "Calibri", 11, linkColor);
                document.InsertParagraph();
                addLineToDocument(document, Xceed.Document.NET.BorderStyle.Tcbs_dashed,
                    pageWidth, color);

                document.InsertParagraph();
                addTextToDocument(document, "ORGANIZER", Alignment.left, "Calibri", 14, headerColor);

                string companyImagePath = Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\" + "mergedImage" + ".png";

                addTwoImagesToText("\t\t\t\t\t\t", companyImagePath, iliniumImage, document, Alignment.right, "Calibri",
                    14, System.Drawing.Color.Black);
                // Save this document.
                document.Save();
            }

            string targetFile = Directory.GetParent(workingDirectory).Parent.FullName + @"\bin\Debug\docs\" + savingName + ".pdf";
            convertWordToPdf(Directory.GetParent(workingDirectory).Parent.FullName + @"\bin\Debug\docs\" + savingName + ".docx", targetFile);
            
            return targetFile;

        }
        private void addLineToDocument(Document document, Xceed.Document.NET.BorderStyle borderStyle, float width,
            System.Drawing.Color color)
        {
            Table t = document.AddTable(1, 1);

            Xceed.Document.NET.Border border = new Xceed.Document.NET.Border();
            border.Tcbs = borderStyle;
            border.Color = color;
            Xceed.Document.NET.Border notVisible = new Xceed.Document.NET.Border();
            notVisible.Color = System.Drawing.Color.Transparent;
            t.SetBorder(TableBorderType.Bottom, border);
            t.SetBorder(TableBorderType.Top, notVisible);
            t.SetBorder(TableBorderType.Right, notVisible);
            t.SetBorder(TableBorderType.Left, notVisible);

            t.SetColumnWidth(0, width);
            document.InsertTable(t);
        }
        public void addImageToDocument(Document document, int height, int width,
            string imagePath, Alignment alignment)
        {
            // Add an image into the document.    
            Image image = document.AddImage(imagePath);

            // Create a picture (A custom view of an Image).
            Picture picture = image.CreatePicture();
            if (height > 0 || width > 0)
            {
                picture.Height = height;
                picture.Width = width;
            }

            // Insert a new Paragraph into the document.
            Paragraph p1 = document.InsertParagraph();

            // Append content to the Paragraph
            p1.AppendPicture(picture);
            p1.Alignment = alignment;
        }
        public void addImageToDocument(Document document, int height, int width,
            System.Drawing.Image imageS, Alignment alignment)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                
                imageS.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                ms.Seek(0, SeekOrigin.Begin);
                // Add an image into the document.    
                Image image = document.AddImage(ms);

            // Create a picture (A custom view of an Image).
                Picture picture = image.CreatePicture();
                if (height > 0 || width > 0)
                {
                    picture.Height = height;
                    picture.Width = width;
                }

                // Insert a new Paragraph into the document.
                Paragraph p1 = document.InsertParagraph();

                // Append content to the Paragraph
                p1.AppendPicture(picture);
                p1.Alignment = alignment;
            }
        }

        public System.Drawing.Image GenerateCompanyCredentialsImage(string companyName, string address, 
            string contactEmail, string contactNumber, string webPage)
        {
            System.Drawing.Image placeImage = System.Drawing.Image.FromFile(placeImagePath);
            System.Drawing.Image phoneImage = System.Drawing.Image.FromFile(phoneImagePath);
            System.Drawing.Image emailImage = System.Drawing.Image.FromFile(emailImagePath);
            System.Drawing.Image worldImage = System.Drawing.Image.FromFile(worldImagePath);

            ImagesConverter toImg = new ImagesConverter();
            System.Drawing.Image image0 = toImg.convertTextToImage("InLinum", 14F);
            System.Drawing.Image image1 = toImg.convertTextToImage("Verkiai 30B, Vilnius, Lithuania", 14F);
            System.Drawing.Image image2 = toImg.convertTextToImage("info@ ilinum.com", 14F);
            System.Drawing.Image image3 = toImg.convertTextToImage("+37069508296", 14F);
            System.Drawing.Image image4 = toImg.convertTextToImage("www.greenautosummit.com", 14F);



            System.Drawing.Image convInLinum = toImg.MergeTwoImagesHorizontaly(phoneImage, image0, "InLinum");
            System.Drawing.Image convStreet = toImg.MergeTwoImagesHorizontaly(placeImage, image1, "street");
            System.Drawing.Image convAddress = toImg.MergeTwoImagesHorizontaly(emailImage, image2, "address");
            System.Drawing.Image convPhoneNumber = toImg.MergeTwoImagesHorizontaly(phoneImage, image3, "phoneNumber");
            System.Drawing.Image convPage = toImg.MergeTwoImagesHorizontaly(worldImage, image4, "page");
            System.Drawing.Image convWholeImage = toImg.MergeTwoImagesVerticaly(image0, convStreet, "mergedImage");
            convWholeImage = toImg.MergeTwoImagesVerticaly(convWholeImage, convAddress, "mergedImage");
            convWholeImage = toImg.MergeTwoImagesVerticaly(convWholeImage, convPhoneNumber, "mergedImage");
            convWholeImage = toImg.MergeTwoImagesVerticaly(convWholeImage, convPage, "mergedImage");

            convWholeImage.Save(Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\" + "mergedImage" + ".png", System.Drawing.Imaging.ImageFormat.Png);


            return image1;
        }
        public void addTextToDocument(Document document, string text, Alignment alignment,
            string fontName, double fontSize, System.Drawing.Color color)
        {
            // Insert a new Paragraph into the document.
            Paragraph p1 = document.InsertParagraph().Append(text).Font(fontName).FontSize(fontSize)
                .Color(color);
            p1.Alignment = alignment;
        }
        private string addSpaces(string text, int spaces)
        {
            string newText = text;
            for (int i = 0; i < spaces; i++)
            {
                newText = newText + ' ';
            }
            return newText;
        }


        public void addTextWithImageInFront(string text, string imagePath, int width, int height, Document document, Alignment alignment,
            string fontName, double fontSize, System.Drawing.Color color)
        {
            // Add an image into the document.    
            Image image = document.AddImage(imagePath);

            // Create a picture (A custom view of an Image).
            Picture picture = image.CreatePicture();
            if (height > 0 || width > 0)
            {
                picture.Height = 300;
                picture.Width = 300;
            }

            // Insert a new Paragraph into the document.
            Paragraph p1 = document.InsertParagraph();

            // Append content to the Paragraph
            p1.AppendPicture(picture).Append(text).Font(fontName).FontSize(fontSize)
                .Color(color);
        }
        public void addTwoImagesToText(string textBetween, string image1Path, string image2Path, Document document, Alignment alignment,
            string fontName, double fontSize, System.Drawing.Color color)
        {
            Image image1 = document.AddImage(image1Path);

            // Create a picture (A custom view of an Image).
            Picture picture1 = image1.CreatePicture();

            Image image2 = document.AddImage(image2Path);

            // Create a picture (A custom view of an Image).
            Picture picture2 = image2.CreatePicture();

            picture2.Height = 114;
            picture2.Width = 251;

            // Insert a new Paragraph into the document.
            Paragraph p1 = document.InsertParagraph();

            // Append content to the Paragraph
            p1.AppendPicture(picture1).Append(textBetween).Font(fontName).AppendPicture(picture2).
                FontSize(fontSize).Color(color);


        }


        private void convertWordToPdf(object filePath, object targetFile)
        {
            pdfConverter.convertWordToPdf(filePath, targetFile);


        }
        //returns paths
        public List<string> generateTicketsAndSave(List<Participant> participants,List<Event> events, CompanyData companyData)
        {
            if (MSdoc == null) { MSdoc = new Microsoft.Office.Interop.Word.Application(); }
            pdfConverter = new PDFConverter(MSdoc);
            List<string> ticketsPaths = new List<string>(); 
            foreach(Participant participant in participants)
            {
                Event participantEvent = events.FindLast(e => e.id.ToString().Equals(participant.eventId));
                string eventDate = formatEventDate(participantEvent.date_From);
                System.Drawing.Image image = GenerateCompanyCredentialsImage(companyData.companyName,
            companyData.address, companyData.email, companyData.phoneNumber, participantEvent.webPage);
                string ticketPath = createTicket(
                    participant.firstName,
                    participant.lastName,
                    participant.companyName,
                    participant.participationFormat,
                    participantEvent.eventName,
                    eventDate,
                    participantEvent.venueName,
                    participantEvent.venueAdress,
                    participant.ticketBarcode,
                    System.Drawing.Color.Black,
                    participantEvent.eventName + participant.participantId
                    );
                ticketsPaths.Add(ticketPath);
            }
            if (MSdoc != null)
            {
                object Unknown = Type.Missing;
                MSdoc.Documents.Close(ref Unknown, ref Unknown, ref Unknown);
                MSdoc.Quit(ref Unknown, ref Unknown, ref Unknown);
            }
            return ticketsPaths;
        }
        public string formatEventDate(DateTime date)
        {

            CultureInfo usEnglish = new CultureInfo("en-US");
            string month = usEnglish.DateTimeFormat.GetMonthName(date.Month);
            string dayOfWeek = date.DayOfWeek.ToString();
            string day = date.Day.ToString();
            string year = date.Year.ToString();

            return dayOfWeek + ", " + day + " " + month + " " + year;


        }
    }
}
