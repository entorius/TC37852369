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
        public PDFConverter pdfConverter;
        private StringEncoder stringEncoder = new StringEncoder();
        private BarcodeGenerator barcodeGenerator = new BarcodeGenerator();
        private ImageEntityServices imageEntityServices = new ImageEntityServices();
        public Microsoft.Office.Interop.Word.Application MSdoc;
        public static string workingDirectory = Environment.CurrentDirectory;
        Dictionary<string, Dictionary<int,string>> eventsImagesPaths = new Dictionary<string, Dictionary<int,string>>();
        string imagesSavingPath = Directory.GetParent(workingDirectory).Parent.FullName + @"\bin\Debug\docs";
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
        public string createTicket(string firstName, string lastName, string company, string format,
            string eventName,string date, int monthNumber,string location1Row, string location2Row, string barcode,
           System.Drawing.Color barcodeColor, string savingName,string firmImagePath,
           string eventImagePath,string sponsorsImagePath,string savingPath)
        {
            string encodedBarcode = StringEncoder.ReturnEncryptedString(barcode);
            System.Drawing.Image barcodeImage = barcodeGenerator.generateQrBarcodeZXing(encodedBarcode, barcodeColor);

            string localSavingPath = @"docs\";

            if (savingPath.Replace(" ","").Length > 0)
            {
                localSavingPath = savingPath + @"\";
            }

            using (DocX document = DocX.Create(localSavingPath + savingName + ".docx"))
            {
                document.InsertParagraph();
                document.InsertParagraph();
                System.Drawing.Color borderColor = System.Drawing.ColorTranslator.FromHtml("#00A86B");
                // Set document margins
                document.MarginBottom = 0.7F;
                document.MarginTop = 0.7F;
                document.MarginRight = 35F;
                document.MarginLeft = 35F;

                document.InsertParagraph();
                //add logo image
                Picture eventpic = null;
                Picture sponsorspic = null;
                if (eventImagePath.Length > 0) 
                {
                    Image image = document.AddImage(eventImagePath);
                    eventpic = image.CreatePicture();
                }
                if (sponsorsImagePath.Length > 0)
                {
                    Image image = document.AddImage(sponsorsImagePath);
                    sponsorspic = image.CreatePicture();
                }
                if (eventImagePath.Length > 0 && sponsorsImagePath.Length > 0)
                {
                    Table table = document.AddTable(1, 2);
                    float[] widthsPercentage = { 40F, 60F };
                    table.SetWidthsPercentage(widthsPercentage, document.PageWidth - 20F);
                    table.Design = TableDesign.TableGrid;
                    Border emptyBorder = new Border(BorderStyle.Tcbs_none, BorderSize.one, 0, System.Drawing.Color.Transparent);
                    table.SetBorder(TableBorderType.Top, emptyBorder);
                    table.SetBorder(TableBorderType.Right, emptyBorder);
                    table.SetBorder(TableBorderType.Left, emptyBorder);
                    table.SetBorder(TableBorderType.Bottom, emptyBorder);
                    Border notEmptyBorder = new Border(BorderStyle.Tcbs_single, BorderSize.one, 1, borderColor);
                    
                    table.SetBorder(TableBorderType.InsideV, notEmptyBorder);
                    if (eventpic != null)
                    {
                        table.Rows[0].Cells[0].VerticalAlignment = VerticalAlignment.Center;

                        table.Rows[0].Cells[0].Paragraphs[0].AppendPicture(resizePicture(eventpic,300,200));
                        
                    }
                    if (sponsorspic != null)
                    {
                        table.Rows[0].Cells[1].VerticalAlignment = VerticalAlignment.Center;
                        table.Rows[0].Cells[1].MarginLeft = 20F;
                        table.Rows[0].Cells[1].Paragraphs[0].AppendPicture(resizePicture(sponsorspic,600,200));
                        
                    }
                    table.SetBorder(TableBorderType.Bottom, emptyBorder);
                    //table.AutoFit = AutoFit.Contents;
                    document.InsertTable(table);
                    table.SetBorder(TableBorderType.Bottom, emptyBorder);
                }
                else if (eventImagePath.Length > 0)
                {
                    addImageToDocument(document, 150, 500, eventImagePath, Alignment.center);
                }
                else if (sponsorsImagePath.Length > 0)
                {
                    addImageToDocument(document, 150, 500, sponsorsImagePath, Alignment.center);
                }
                //addImageToDocument(document, 145, 473, LogoFinalImage, Alignment.center);

                float pageWidth = document.PageWidth * 18;
                //add line through all page

                document.InsertParagraph();
                addLineToDocument(document, Xceed.Document.NET.BorderStyle.Tcbs_dashed,
                    pageWidth, borderColor);

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
                string tabs = "\t\t\t\t\t";
                if (monthNumber >= 11 || monthNumber == 9)
                {
                    tabs = "\t\t\t\t";
                }
                string datePlace = date + tabs + location1Row;
                string TimeLocation = "\t\t\t\t\t\t\t\t\t" + location2Row;



                //string calendarMap = "Add to calendar\t\t\t\t\t\t\t\t\t\t\tView map";





                addTextToDocument(document, dateLocation, Alignment.left, "Calibri", 14, headerColor);
                addTextToDocument(document, datePlace, Alignment.left, "Calibri", 16, paragraphColor);
                addTextToDocument(document, TimeLocation, Alignment.left, "Calibri", 16, paragraphColor);
                document.InsertParagraph();



                addImageToDocument(document, 250, 250, barcodeImage, Alignment.center);

                //addTextToDocument(document, calendarMap, Alignment.left, "Calibri", 11, linkColor);
                document.InsertParagraph();
                addLineToDocument(document, Xceed.Document.NET.BorderStyle.Tcbs_dashed,
                    pageWidth, borderColor);

                document.InsertParagraph();
                addTextToDocument(document, "ORGANIZER", Alignment.left, "Calibri", 14, headerColor);

                string companyImagePath = Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\" + "mergedImage" + ".png";
                if (firmImagePath.Replace(" ", "").Length < 1)
                {
                    addTwoImagesToText("\t\t\t\t\t\t", companyImagePath, iliniumImage, document, Alignment.right, "Calibri",
                        14, System.Drawing.Color.Black);
                }
                else
                {
                    addTwoImagesToText("\t\t\t\t\t\t", companyImagePath, firmImagePath, document, Alignment.right, "Calibri",
                       14, System.Drawing.Color.Black);
                }
                // Save this document.
                document.Save();
            }


            string targetPath = Directory.GetParent(workingDirectory).Parent.FullName + @"\bin\Debug\docs\";
            if (savingPath.Replace(" ", "").Length > 0)
            {
                targetPath = savingPath + @"\";
            }
            string targetFile = targetPath + savingName + ".pdf";
            convertWordToPdf(targetPath + savingName + ".docx", targetFile);
            
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
        public async Task<List<string>> generateTicketsAndSave(List<Participant> participants,List<Event> events, CompanyData companyData)
        {
            if (MSdoc == null) { MSdoc = new Microsoft.Office.Interop.Word.Application(); }
            pdfConverter = new PDFConverter(MSdoc);
            List<string> ticketsPaths = new List<string>(); 
            foreach(Event ev in events)
            {
                Dictionary<int,string> eventImagesPaths = new Dictionary<int,string>();
                List<ImageEntity> imageEntities = await imageEntityServices.GetEventImageEntities(ev);
                foreach(ImageEntity ie in imageEntities)
                {
                    string imagePath = await imageEntityServices.downloadEventImage(ie, imagesSavingPath);
                    eventImagesPaths.Add(Int32.Parse(ie.imageNumber.ToString()),imagePath);
                }
                eventsImagesPaths.Add(ev.eventName, eventImagesPaths);
            }
            List<ImageEntity> companyImageEntity = await imageEntityServices.GetCompanyImageEntities();

            string companyImagePath = "";
            if (companyImageEntity.Count > 0)
            {
                companyImagePath = await imageEntityServices.downloadCompanyImage(companyImageEntity[0], imagesSavingPath);
            }
            foreach(Participant participant in participants)
            {
                Event participantEvent = events.FindLast(e => e.id.ToString().Equals(participant.eventId));
                string eventDate = formatEventDate(participantEvent.date_From);
                string eventImagePath = "";
                bool gotEventImagePath = eventsImagesPaths[participantEvent.eventName].TryGetValue(1,out eventImagePath);
                string sponsorsImagePath = "";
                bool gotSponsorsImagePath = eventsImagesPaths[participantEvent.eventName].TryGetValue(2, out eventImagePath);
                if (!gotEventImagePath)
                {
                    eventImagePath = "";
                }
                if (!gotSponsorsImagePath)
                {
                    sponsorsImagePath = "";
                }

                System.Drawing.Image image = GenerateCompanyCredentialsImage(companyData.companyName,
            companyData.address, companyData.email, companyData.phoneNumber, participantEvent.webPage);
                string ticketPath = createTicket(
                    participant.firstName,
                    participant.lastName,
                    participant.companyName,
                    participant.participationFormat,
                    participantEvent.eventName,
                    eventDate,
                    participantEvent.date_From.Month,
                    participantEvent.venueName,
                    participantEvent.venueAdress,
                    participant.ticketBarcode,
                    System.Drawing.Color.Black,
                    participantEvent.eventName + participant.participantId,
                    companyImagePath,
                    eventImagePath,
                    sponsorsImagePath,
                    ""
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
        public Picture resizePicture(Picture picture, int maxWidth, int maxHeight)
        {
            bool widthBigger = picture.Width > picture.Height ? true : false;

            double resizeValue;
            double maxDouble = 1;
            double picSize = 1;
            if (widthBigger)
            {
                if (picture.Width > maxWidth) {
                    maxDouble = (double)maxWidth;
                    picSize = (double)picture.Width;
                    resizeValue = picSize / maxDouble;
                    picture.Width = (int)(picSize / resizeValue);
                    picture.Height = (int)((double)picture.Height / resizeValue);
                }
                
            }
            
               
            if (picture.Height > maxHeight)
            {
                maxDouble = (double)maxHeight;
                picSize = (double)picture.Height;
                resizeValue = picSize / maxDouble;
                picture.Height = (int)(picSize / resizeValue);
                picture.Width = (int)((double)picture.Width / resizeValue);
            }
            return picture;
            

            
        }
       
    }
}
