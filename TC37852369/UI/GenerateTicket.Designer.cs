namespace TC37852369
{
    partial class GenerateTicket
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TextBox_UserId = new System.Windows.Forms.TextBox();
            this.Button_Generate = new MetroFramework.Controls.MetroButton();
            this.Button_Cancel = new MetroFramework.Controls.MetroButton();
            this.Button_GenerateBarcode = new MetroFramework.Controls.MetroButton();
            this.Button_GenerateBarcodeNumber = new MetroFramework.Controls.MetroButton();
            this.TextBox_Barcode = new System.Windows.Forms.TextBox();
            this.Button_GenerateUsers = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextBox_UserId
            // 
            this.TextBox_UserId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_UserId.Location = new System.Drawing.Point(95, 90);
            this.TextBox_UserId.Name = "TextBox_UserId";
            this.TextBox_UserId.Size = new System.Drawing.Size(408, 30);
            this.TextBox_UserId.TabIndex = 1;
            // 
            // Button_Generate
            // 
            this.Button_Generate.BackColor = System.Drawing.Color.Maroon;
            this.Button_Generate.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Button_Generate.Location = new System.Drawing.Point(367, 162);
            this.Button_Generate.Name = "Button_Generate";
            this.Button_Generate.Size = new System.Drawing.Size(136, 51);
            this.Button_Generate.Style = MetroFramework.MetroColorStyle.White;
            this.Button_Generate.TabIndex = 10;
            this.Button_Generate.Text = "Generate";
            this.Button_Generate.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Button_Generate.UseCustomBackColor = true;
            this.Button_Generate.UseSelectable = true;
            this.Button_Generate.UseStyleColors = true;
            this.Button_Generate.Click += new System.EventHandler(this.Button_Generate_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Button_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_Cancel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Button_Cancel.Location = new System.Drawing.Point(95, 162);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(136, 51);
            this.Button_Cancel.TabIndex = 11;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseSelectable = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Button_GenerateBarcode
            // 
            this.Button_GenerateBarcode.BackColor = System.Drawing.Color.Maroon;
            this.Button_GenerateBarcode.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Button_GenerateBarcode.Location = new System.Drawing.Point(563, 251);
            this.Button_GenerateBarcode.Name = "Button_GenerateBarcode";
            this.Button_GenerateBarcode.Size = new System.Drawing.Size(316, 51);
            this.Button_GenerateBarcode.Style = MetroFramework.MetroColorStyle.White;
            this.Button_GenerateBarcode.TabIndex = 12;
            this.Button_GenerateBarcode.Text = "Generate barcode";
            this.Button_GenerateBarcode.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Button_GenerateBarcode.UseCustomBackColor = true;
            this.Button_GenerateBarcode.UseSelectable = true;
            this.Button_GenerateBarcode.UseStyleColors = true;
            this.Button_GenerateBarcode.Click += new System.EventHandler(this.Button_GenerateBarcode_Click);
            // 
            // Button_GenerateBarcodeNumber
            // 
            this.Button_GenerateBarcodeNumber.BackColor = System.Drawing.Color.Maroon;
            this.Button_GenerateBarcodeNumber.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Button_GenerateBarcodeNumber.Location = new System.Drawing.Point(563, 348);
            this.Button_GenerateBarcodeNumber.Name = "Button_GenerateBarcodeNumber";
            this.Button_GenerateBarcodeNumber.Size = new System.Drawing.Size(316, 51);
            this.Button_GenerateBarcodeNumber.Style = MetroFramework.MetroColorStyle.White;
            this.Button_GenerateBarcodeNumber.TabIndex = 13;
            this.Button_GenerateBarcodeNumber.Text = "Generate barcode number";
            this.Button_GenerateBarcodeNumber.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Button_GenerateBarcodeNumber.UseCustomBackColor = true;
            this.Button_GenerateBarcodeNumber.UseSelectable = true;
            this.Button_GenerateBarcodeNumber.UseStyleColors = true;
            this.Button_GenerateBarcodeNumber.Click += new System.EventHandler(this.Button_GenerateBarcodeNumber_Click);
            // 
            // TextBox_Barcode
            // 
            this.TextBox_Barcode.Location = new System.Drawing.Point(422, 438);
            this.TextBox_Barcode.Name = "TextBox_Barcode";
            this.TextBox_Barcode.Size = new System.Drawing.Size(428, 22);
            this.TextBox_Barcode.TabIndex = 14;
            // 
            // Button_GenerateUsers
            // 
            this.Button_GenerateUsers.Location = new System.Drawing.Point(563, 173);
            this.Button_GenerateUsers.Name = "Button_GenerateUsers";
            this.Button_GenerateUsers.Size = new System.Drawing.Size(316, 51);
            this.Button_GenerateUsers.TabIndex = 15;
            this.Button_GenerateUsers.Text = "Generate 100 users";
            this.Button_GenerateUsers.UseVisualStyleBackColor = true;
            this.Button_GenerateUsers.Click += new System.EventHandler(this.Button_GenerateUsers_Click);
            // 
            // GenerateTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.Button_GenerateUsers);
            this.Controls.Add(this.TextBox_Barcode);
            this.Controls.Add(this.Button_GenerateBarcodeNumber);
            this.Controls.Add(this.Button_GenerateBarcode);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Generate);
            this.Controls.Add(this.TextBox_UserId);
            this.Name = "GenerateTicket";
            this.Padding = new System.Windows.Forms.Padding(0, 60, 0, 0);
            this.Text = "GenerateTicket";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TextBox_UserId;
        private MetroFramework.Controls.MetroButton Button_Generate;
        private MetroFramework.Controls.MetroButton Button_Cancel;
        private MetroFramework.Controls.MetroButton Button_GenerateBarcode;
        private MetroFramework.Controls.MetroButton Button_GenerateBarcodeNumber;
        private System.Windows.Forms.TextBox TextBox_Barcode;
        private System.Windows.Forms.Button Button_GenerateUsers;
    }
}