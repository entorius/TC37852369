namespace TC37852369
{
    partial class EmailTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmailTemplate));
            this.TextBox_Subject = new System.Windows.Forms.TextBox();
            this.TextBox_Body = new System.Windows.Forms.TextBox();
            this.Button_Confirm = new MetroFramework.Controls.MetroButton();
            this.Button_Cancel = new MetroFramework.Controls.MetroButton();
            this.TextBox_TemplateName = new System.Windows.Forms.TextBox();
            this.ListBox_EmailTemplates = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TextBox_Subject
            // 
            this.TextBox_Subject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_Subject.Location = new System.Drawing.Point(345, 117);
            this.TextBox_Subject.Name = "TextBox_Subject";
            this.TextBox_Subject.Size = new System.Drawing.Size(463, 30);
            this.TextBox_Subject.TabIndex = 1;
            // 
            // TextBox_Body
            // 
            this.TextBox_Body.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_Body.Location = new System.Drawing.Point(345, 153);
            this.TextBox_Body.Multiline = true;
            this.TextBox_Body.Name = "TextBox_Body";
            this.TextBox_Body.Size = new System.Drawing.Size(463, 210);
            this.TextBox_Body.TabIndex = 2;
            // 
            // Button_Confirm
            // 
            this.Button_Confirm.BackColor = System.Drawing.Color.Maroon;
            this.Button_Confirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_Confirm.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Button_Confirm.Location = new System.Drawing.Point(672, 394);
            this.Button_Confirm.Name = "Button_Confirm";
            this.Button_Confirm.Size = new System.Drawing.Size(136, 51);
            this.Button_Confirm.Style = MetroFramework.MetroColorStyle.White;
            this.Button_Confirm.TabIndex = 11;
            this.Button_Confirm.Text = "Confirm";
            this.Button_Confirm.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Button_Confirm.UseCustomBackColor = true;
            this.Button_Confirm.UseSelectable = true;
            this.Button_Confirm.UseStyleColors = true;
            this.Button_Confirm.Click += new System.EventHandler(this.Button_Confirm_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.BackColor = System.Drawing.Color.Silver;
            this.Button_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_Cancel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Button_Cancel.Location = new System.Drawing.Point(23, 394);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(136, 51);
            this.Button_Cancel.Style = MetroFramework.MetroColorStyle.Black;
            this.Button_Cancel.TabIndex = 14;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Button_Cancel.UseCustomBackColor = true;
            this.Button_Cancel.UseSelectable = true;
            this.Button_Cancel.UseStyleColors = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // TextBox_TemplateName
            // 
            this.TextBox_TemplateName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_TemplateName.Location = new System.Drawing.Point(345, 81);
            this.TextBox_TemplateName.Name = "TextBox_TemplateName";
            this.TextBox_TemplateName.Size = new System.Drawing.Size(463, 30);
            this.TextBox_TemplateName.TabIndex = 15;
            // 
            // ListBox_EmailTemplates
            // 
            this.ListBox_EmailTemplates.FormattingEnabled = true;
            this.ListBox_EmailTemplates.ItemHeight = 16;
            this.ListBox_EmailTemplates.Location = new System.Drawing.Point(23, 117);
            this.ListBox_EmailTemplates.Name = "ListBox_EmailTemplates";
            this.ListBox_EmailTemplates.Size = new System.Drawing.Size(283, 164);
            this.ListBox_EmailTemplates.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label1.Location = new System.Drawing.Point(23, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 28);
            this.label1.TabIndex = 17;
            this.label1.Text = "Email Templates";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.ForestGreen;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(23, 296);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(283, 45);
            this.button1.TabIndex = 19;
            this.button1.Text = "Create New";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // EmailTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 471);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListBox_EmailTemplates);
            this.Controls.Add(this.TextBox_TemplateName);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Confirm);
            this.Controls.Add(this.TextBox_Body);
            this.Controls.Add(this.TextBox_Subject);
            this.Name = "EmailTemplate";
            this.Text = "Email Template";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TextBox_Subject;
        private System.Windows.Forms.TextBox TextBox_Body;
        private MetroFramework.Controls.MetroButton Button_Confirm;
        private MetroFramework.Controls.MetroButton Button_Cancel;
        private System.Windows.Forms.TextBox TextBox_TemplateName;
        private System.Windows.Forms.ListBox ListBox_EmailTemplates;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}