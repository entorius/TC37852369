namespace TC37852369.UI
{
    partial class SendTemplate
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
            this.components = new System.ComponentModel.Container();
            this.ComboBox_ChooseTemplate = new MetroFramework.Controls.MetroComboBox();
            this.TextBox_Subject = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.TextBox_Body = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.Button_Send = new MetroFramework.Controls.MetroButton();
            this.Button_Cancel = new MetroFramework.Controls.MetroButton();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.Timer_StringChanged = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ComboBox_ChooseTemplate
            // 
            this.ComboBox_ChooseTemplate.FormattingEnabled = true;
            this.ComboBox_ChooseTemplate.ItemHeight = 23;
            this.ComboBox_ChooseTemplate.Location = new System.Drawing.Point(197, 85);
            this.ComboBox_ChooseTemplate.Name = "ComboBox_ChooseTemplate";
            this.ComboBox_ChooseTemplate.Size = new System.Drawing.Size(367, 29);
            this.ComboBox_ChooseTemplate.TabIndex = 0;
            this.ComboBox_ChooseTemplate.UseSelectable = true;
            this.ComboBox_ChooseTemplate.SelectedIndexChanged += new System.EventHandler(this.ComboBox_ChooseTemplate_SelectedIndexChanged);
            // 
            // TextBox_Subject
            // 
            // 
            // 
            // 
            this.TextBox_Subject.CustomButton.Image = null;
            this.TextBox_Subject.CustomButton.Location = new System.Drawing.Point(436, 1);
            this.TextBox_Subject.CustomButton.Name = "";
            this.TextBox_Subject.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.TextBox_Subject.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.TextBox_Subject.CustomButton.TabIndex = 1;
            this.TextBox_Subject.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.TextBox_Subject.CustomButton.UseSelectable = true;
            this.TextBox_Subject.CustomButton.Visible = false;
            this.TextBox_Subject.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.TextBox_Subject.Lines = new string[0];
            this.TextBox_Subject.Location = new System.Drawing.Point(720, 85);
            this.TextBox_Subject.MaxLength = 32767;
            this.TextBox_Subject.Name = "TextBox_Subject";
            this.TextBox_Subject.PasswordChar = '\0';
            this.TextBox_Subject.ReadOnly = true;
            this.TextBox_Subject.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TextBox_Subject.SelectedText = "";
            this.TextBox_Subject.SelectionLength = 0;
            this.TextBox_Subject.SelectionStart = 0;
            this.TextBox_Subject.ShortcutsEnabled = true;
            this.TextBox_Subject.Size = new System.Drawing.Size(458, 23);
            this.TextBox_Subject.TabIndex = 1;
            this.TextBox_Subject.UseSelectable = true;
            this.TextBox_Subject.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.TextBox_Subject.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(69, 89);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(109, 19);
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "Choose template";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(650, 85);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(51, 19);
            this.metroLabel2.TabIndex = 3;
            this.metroLabel2.Text = "Subject";
            // 
            // TextBox_Body
            // 
            // 
            // 
            // 
            this.TextBox_Body.CustomButton.Image = null;
            this.TextBox_Body.CustomButton.Location = new System.Drawing.Point(182, 1);
            this.TextBox_Body.CustomButton.Name = "";
            this.TextBox_Body.CustomButton.Size = new System.Drawing.Size(275, 275);
            this.TextBox_Body.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.TextBox_Body.CustomButton.TabIndex = 1;
            this.TextBox_Body.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.TextBox_Body.CustomButton.UseSelectable = true;
            this.TextBox_Body.CustomButton.Visible = false;
            this.TextBox_Body.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.TextBox_Body.Lines = new string[0];
            this.TextBox_Body.Location = new System.Drawing.Point(720, 134);
            this.TextBox_Body.MaxLength = 32767;
            this.TextBox_Body.Multiline = true;
            this.TextBox_Body.Name = "TextBox_Body";
            this.TextBox_Body.PasswordChar = '\0';
            this.TextBox_Body.ReadOnly = true;
            this.TextBox_Body.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBox_Body.SelectedText = "";
            this.TextBox_Body.SelectionLength = 0;
            this.TextBox_Body.SelectionStart = 0;
            this.TextBox_Body.ShortcutsEnabled = true;
            this.TextBox_Body.Size = new System.Drawing.Size(458, 277);
            this.TextBox_Body.TabIndex = 4;
            this.TextBox_Body.UseSelectable = true;
            this.TextBox_Body.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.TextBox_Body.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(650, 134);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(39, 19);
            this.metroLabel3.TabIndex = 5;
            this.metroLabel3.Text = "Body";
            // 
            // Button_Send
            // 
            this.Button_Send.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Button_Send.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Button_Send.ForeColor = System.Drawing.Color.White;
            this.Button_Send.Location = new System.Drawing.Point(464, 339);
            this.Button_Send.Name = "Button_Send";
            this.Button_Send.Size = new System.Drawing.Size(147, 72);
            this.Button_Send.TabIndex = 6;
            this.Button_Send.Text = "Send";
            this.Button_Send.UseCustomBackColor = true;
            this.Button_Send.UseCustomForeColor = true;
            this.Button_Send.UseSelectable = true;
            this.Button_Send.Click += new System.EventHandler(this.Button_Send_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Button_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Button_Cancel.Location = new System.Drawing.Point(69, 339);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(147, 72);
            this.Button_Cancel.TabIndex = 7;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseCustomBackColor = true;
            this.Button_Cancel.UseCustomForeColor = true;
            this.Button_Cancel.UseSelectable = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel4.Location = new System.Drawing.Point(23, 25);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(130, 25);
            this.metroLabel4.TabIndex = 8;
            this.metroLabel4.Text = "Send Template";
            // 
            // Timer_StringChanged
            // 
            this.Timer_StringChanged.Tick += new System.EventHandler(this.Timer_StringChanged_Tick);
            // 
            // SendTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(119F, 119F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1245, 484);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Send);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.TextBox_Body);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.TextBox_Subject);
            this.Controls.Add(this.ComboBox_ChooseTemplate);
            this.Name = "SendTemplate";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroComboBox ComboBox_ChooseTemplate;
        private MetroFramework.Controls.MetroTextBox TextBox_Subject;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox TextBox_Body;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        public MetroFramework.Controls.MetroButton Button_Send;
        private MetroFramework.Controls.MetroButton Button_Cancel;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private System.Windows.Forms.Timer Timer_StringChanged;
    }
}