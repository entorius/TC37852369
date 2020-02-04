using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.Services.Images
{
    public class GifImage
    {
        private Image gifImage;
        private FrameDimension dimension;
        private int frameCount;
        private int currentFrame = -1;
        private bool reverse;
        private int step = 1;
        private int width;
        private int height;

        public GifImage(string path, int width, int height)
        {
            gifImage = Image.FromFile(path);
            this.width = width;
            this.height = height;
            //initialize
            dimension = new FrameDimension(gifImage.FrameDimensionsList[0]);
            //gets the GUID
            //total frames in the animation
            frameCount = gifImage.GetFrameCount(dimension);
        }

        public bool ReverseAtEnd
        {
            //whether the gif should play backwards when it reaches the end
            get { return reverse; }
            set { reverse = value; }
        }

        public Image GetNextFrame()
        {

            currentFrame += step;

            //if the animation reaches a boundary...
            if (currentFrame >= frameCount || currentFrame < 0)
            {
                if (reverse)
                {
                    step *= -1;
                    //...reverse the count
                    //apply it
                    currentFrame += step;
                }
                else
                {
                    currentFrame = 0;
                    //...or start over
                }
            }
            return GetFrame(currentFrame);
        }

        public Image GetFrame(int index)
        {
            gifImage.SelectActiveFrame(dimension, index);
            //find the frame
            Image activeFrame = (Image)gifImage.Clone();
            return resizeImage(activeFrame, new Size(width,height));
            //return a copy of it
        }
        private Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
    }
}
