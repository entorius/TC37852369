using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TC37852369.DomainEntities;
using TC37852369.Helpers;
using TC37852369.Services;
using TC37852369.Services.EmailSending;
using TC37852369.UI.helpers;

namespace TC37852369.UI
{
    public partial class AddChangeCompanyInformation : MetroForm
    {
        CompanyDataServices companyDataServices = new CompanyDataServices();
        MetroMessageBoxHelper messageBoxHelper = new MetroMessageBoxHelper();
        LastEntityIdentificationNumberServices lastEntityIdentificationNumberService =
            new LastEntityIdentificationNumberServices();
        ImageEntityServices imageEntityServices = new ImageEntityServices();
        ImageEntity companyImageInCloud = null;
        ImageEntity companyImageToDelete = null;
        Image companyImage = null;
        string companyImagePath = "";
        public static string workingDirectory = Environment.CurrentDirectory;
        string imageSavingPath = Directory.GetParent(workingDirectory).Parent.FullName + @"\UI\Images\";
        

        CompanyData companyData;
        MainWindow mainWindow;
        public AddChangeCompanyInformation(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            InitializeComponent();
            this.FormClosed += CloseHandler;
            InitializeWindowData();
            bool toMaximize = WindowHelper.checkIfMaximizeWindow(this.Width, this.Height);
            if (toMaximize)
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }
        private async void InitializeWindowData()
        {
            companyData = await companyDataServices.GetCompanyData();
            if(companyData != null)
            {
                TextBox_Address.Text = companyData.address;
                TextBox_Email.Text = companyData.email;
                TextBox_CompanyName.Text = companyData.companyName;
                TextBox_PhoneNumber.Text = companyData.phoneNumber;
                TextBox_WebPageAddress.Text = companyData.webPageAddress;
                TextBox_Username.Text = companyData.emailUsername;
                TextBox_Password.Text = companyData.emailPassword;

                List<ImageEntity> companyImages = await imageEntityServices.GetCompanyImageEntities();
                if (companyImages.Count > 0)
                {
                    companyImageInCloud = companyImages[0];
                    string companyImagePath = await imageEntityServices.downloadCompanyImage(companyImageInCloud, imageSavingPath);
                    Image image = Image.FromFile(companyImagePath);
                    addImageToPictureBox(image);
                }

            }
            
        }

        private async void Button_Save_Click(object sender, EventArgs e)
        {
            bool emailCorrect = true;
            if (TextBox_Username.Text.Length > 0 && TextBox_Password.Text.Length > 0)
            {
                emailCorrect = await SendEmail.TryToLogin(TextBox_Username.Text, TextBox_Password.Text);
            }
            if (!emailCorrect)
            {
                messageBoxHelper.showWarning(this, "1) incorrect email or password entered\n" +
                    "2) Bad internet connection", "Warning");
            }
            else
            {
                CompanyData dataSaved = null;
                bool isDataSaved = true;
                try
                {
                    dataSaved = await companyDataServices.EditCompanyData(
                        TextBox_Address.Text,
                        TextBox_CompanyName.Text,
                        TextBox_Email.Text,
                        TextBox_PhoneNumber.Text,
                        TextBox_WebPageAddress.Text,
                        TextBox_Username.Text,
                        TextBox_Password.Text,
                        PictureBox_CompanyLogo.Image
                        );
                }
                catch(Exception)
                {
                    isDataSaved = false;
                }
                if (dataSaved == null && isDataSaved)
                {
                    messageBoxHelper.showWarning(this, "Data save unsuccesfull:\n" +
                        "1) Bad internet connection\n" +
                        "2) Database problems (contact the programmer if problem continues)", "Warning");
                }
                else
                {
                    if (companyImage != null)
                    {
                        bool imageAdded = await AddCompanyImageToDatabase("0");
                        if (companyImageToDelete != null)
                        {
                            bool deleted = await imageEntityServices.DeleteCompanyImageEntity(companyImageToDelete);
                        }
                    }
                    mainWindow.companyData = dataSaved;
                    this.Dispose();
                    mainWindow.Enabled = true;
                }
            }
            
           
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            mainWindow.Enabled = true;
        }
        protected void CloseHandler(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
        }

        private void Button_ChangeCompanyLogo_Click(object sender, EventArgs e)
        {
            FileDialog_CompanyLogo.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (FileDialog_CompanyLogo.ShowDialog() == DialogResult.OK)
            {
                string fileName = FileDialog_CompanyLogo.FileName;
                Image image = Image.FromFile(fileName);
                companyImage = image;
                companyImagePath = fileName;
                if(companyImageInCloud != null)
                {
                    companyImageToDelete = companyImageInCloud;
                    companyImageInCloud = null;
                }
                addImageToPictureBox(image);
            }
        }
        private void addImageToPictureBox(Image image)
        {
            Image resizedImage;

            int highestParameter = Math.Max(image.Width, image.Height);
            double div = 1;
            bool resized = false;

            if (image.Width == highestParameter)
            {
                if (image.Width > PictureBox_CompanyLogo.Width)
                {
                    div = Convert.ToDouble(image.Width) / PictureBox_CompanyLogo.Width;
                    resized = true;
                }
            }
            if (image.Height == highestParameter)
            {
                if (image.Height > PictureBox_CompanyLogo.Height)
                {
                    div = Convert.ToDouble(image.Height) / PictureBox_CompanyLogo.Height;
                    resized = true;
                }
            }

            resizedImage = image;
            if (resized)
            {
                double resizedSizeWidth = (image.Width / div) * 0.95;
                double resizedSizeHeight = (image.Height / div) * 0.95;
                resizedImage = resizeImage(image,
                    new Size(Convert.ToInt32(resizedSizeWidth), Convert.ToInt32(resizedSizeHeight)));
            }


            PictureBox_CompanyLogo.BackgroundImage = resizedImage;
        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }
        private async Task<bool> AddCompanyImageToDatabase(string imageNumber)
        {
            string path = companyImagePath;
            int imageNumberToAdd;
            bool successfulCoversion = Int32.TryParse(imageNumber, out imageNumberToAdd);
            if (successfulCoversion && companyImage != null)
            {
                
                LastIdentificationNumber lastId = await lastEntityIdentificationNumberService.getImageEntityLastIdentificationNumber();
                string image1Name = imageEntityServices.addCompanyImage(path, lastId.id.ToString());
                await imageEntityServices.AddCompanyImageEntity(image1Name, imageNumber);
            }
            return successfulCoversion && companyImage != null;
        }
    }
}
