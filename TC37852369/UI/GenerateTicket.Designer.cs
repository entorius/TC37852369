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
            this.Button_Generate = new MetroFramework.Controls.MetroButton();
            this.Button_Cancel = new MetroFramework.Controls.MetroButton();
            this.ComboBox_Events = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.FolderBrowserDialog_Generation = new System.Windows.Forms.FolderBrowserDialog();
            this.Label_Generating = new MetroFramework.Controls.MetroLabel();
            this.SuspendLayout();
            // 
            // Button_Generate
            // 
            this.Button_Generate.BackColor = System.Drawing.Color.Maroon;
            this.Button_Generate.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Button_Generate.Location = new System.Drawing.Point(481, 363);
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
            this.Button_Cancel.Location = new System.Drawing.Point(95, 363);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(136, 51);
            this.Button_Cancel.TabIndex = 11;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseSelectable = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // ComboBox_Events
            // 
            this.ComboBox_Events.FormattingEnabled = true;
            this.ComboBox_Events.ItemHeight = 23;
            this.ComboBox_Events.Location = new System.Drawing.Point(209, 103);
            this.ComboBox_Events.Name = "ComboBox_Events";
            this.ComboBox_Events.Size = new System.Drawing.Size(408, 29);
            this.ComboBox_Events.TabIndex = 12;
            this.ComboBox_Events.UseSelectable = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(95, 103);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(88, 19);
            this.metroLabel1.TabIndex = 13;
            this.metroLabel1.Text = "Choose Event";
            // 
            // Label_Generating
            // 
            this.Label_Generating.AutoSize = true;
            this.Label_Generating.Location = new System.Drawing.Point(481, 421);
            this.Label_Generating.Name = "Label_Generating";
            this.Label_Generating.Size = new System.Drawing.Size(0, 0);
            this.Label_Generating.TabIndex = 14;
            // 
            // GenerateTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(800, 600);
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.Label_Generating);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.ComboBox_Events);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Generate);
            this.Name = "GenerateTicket";
            this.Padding = new System.Windows.Forms.Padding(0, 60, 0, 0);
            this.Text = "GenerateTicket";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroButton Button_Generate;
        private MetroFramework.Controls.MetroButton Button_Cancel;
        private MetroFramework.Controls.MetroComboBox ComboBox_Events;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowserDialog_Generation;
        private MetroFramework.Controls.MetroLabel Label_Generating;
    }
}