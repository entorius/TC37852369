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
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
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
            // 
            // metroButton1
            // 
            this.metroButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.metroButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.metroButton1.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButton1.Location = new System.Drawing.Point(95, 162);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(136, 51);
            this.metroButton1.TabIndex = 11;
            this.metroButton1.Text = "Cancel";
            this.metroButton1.UseSelectable = true;
            // 
            // GenerateTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 252);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.Button_Generate);
            this.Controls.Add(this.TextBox_UserId);
            this.Name = "GenerateTicket";
            this.Text = "GenerateTicket";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TextBox_UserId;
        private MetroFramework.Controls.MetroButton Button_Generate;
        private MetroFramework.Controls.MetroButton metroButton1;
    }
}