using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace TC37852369.Services.Ticket_generation
{
    public class ImagesConverter
    {
        public Image convertTextToImage(string text, float textSizeMine)
        {

            // first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            FontFamily fontFamily = new FontFamily("Calibri");

            Font font = new Font(fontFamily, textSizeMine * 3F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Document);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            Color backColor = Color.Transparent;
            //paint the background
            drawing.Clear(backColor);

            Color textColor = System.Drawing.ColorTranslator.FromHtml("#EAEAEA"); ;

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, Brushes.Gray, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();


            return img;
        }
        private void SaveBmpAsPNG(string imagePath, string savingPath, string savingName)
        {
            Bitmap bmp1 = new Bitmap(imagePath);
            bmp1.Save(savingPath + @"\" + savingName + ".png", ImageFormat.Png);
        }
        public Image MergeTwoImagesHorizontaly(Image image1, Image image2, string savingName)
        {
            Image newImage1 = resizeImage(image1, new Size(14, 14));
            Bitmap bitmap = new Bitmap(image1.Width + image2.Width, Math.Max(newImage1.Height, image2.Height));
            using (Graphics g = Graphics.FromImage(bitmap))
            {

                g.DrawImage(newImage1, 0, 4);
                g.DrawImage(image2, image1.Width, 0);
            }

            Image img = bitmap;

            return img;
        }

        public Image MergeTwoImagesVerticaly(Image image1, Image image2, string savingName)
        {
            Bitmap bitmap = new Bitmap(Math.Max(image1.Width, image2.Width), image1.Height + image2.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {

                g.DrawImage(image1, 0, 0);
                g.DrawImage(image2, 0, image1.Height);
            }

            Image img = bitmap;

            return img;
        }
        public Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
    }
}
