namespace TC37852369
{
    partial class EditEmailTemplate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditEmailTemplate));
            this.TextBox_Subject = new System.Windows.Forms.TextBox();
            this.TextBox_Body = new System.Windows.Forms.TextBox();
            this.Button_Save = new MetroFramework.Controls.MetroButton();
            this.Button_Cancel = new MetroFramework.Controls.MetroButton();
            this.TextBox_TemplateName = new System.Windows.Forms.TextBox();
            this.ListBox_EmailTemplates = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Button_CreateNew = new System.Windows.Forms.Button();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.Button_Close = new MetroFramework.Controls.MetroButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ComboBox_TemplateStrings = new MetroFramework.Controls.MetroComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TextBox_Subject
            // 
            this.TextBox_Subject.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.91597F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_Subject.Location = new System.Drawing.Point(764, 300);
            this.TextBox_Subject.Name = "TextBox_Subject";
            this.TextBox_Subject.Size = new System.Drawing.Size(564, 34);
            this.TextBox_Subject.TabIndex = 1;
            // 
            // TextBox_Body
            // 
            this.TextBox_Body.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.91597F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_Body.Location = new System.Drawing.Point(764, 357);
            this.TextBox_Body.Multiline = true;
            this.TextBox_Body.Name = "TextBox_Body";
            this.TextBox_Body.Size = new System.Drawing.Size(564, 365);
            this.TextBox_Body.TabIndex = 2;
            // 
            // Button_Save
            // 
            this.Button_Save.BackColor = System.Drawing.Color.Maroon;
            this.Button_Save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_Save.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Button_Save.Location = new System.Drawing.Point(1188, 851);
            this.Button_Save.Name = "Button_Save";
            this.Button_Save.Size = new System.Drawing.Size(205, 88);
            this.Button_Save.Style = MetroFramework.MetroColorStyle.White;
            this.Button_Save.TabIndex = 11;
            this.Button_Save.Text = "Save";
            this.Button_Save.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Button_Save.UseCustomBackColor = true;
            this.Button_Save.UseSelectable = true;
            this.Button_Save.UseStyleColors = true;
            this.Button_Save.Click += new System.EventHandler(this.Button_Save_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.BackColor = System.Drawing.Color.Silver;
            this.Button_Cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_Cancel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Button_Cancel.Location = new System.Drawing.Point(673, 851);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(205, 88);
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
            this.TextBox_TemplateName.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.91597F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_TemplateName.Location = new System.Drawing.Point(764, 240);
            this.TextBox_TemplateName.Name = "TextBox_TemplateName";
            this.TextBox_TemplateName.Size = new System.Drawing.Size(564, 34);
            this.TextBox_TemplateName.TabIndex = 15;
            // 
            // ListBox_EmailTemplates
            // 
            this.ListBox_EmailTemplates.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.89076F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.ListBox_EmailTemplates.FormattingEnabled = true;
            this.ListBox_EmailTemplates.ItemHeight = 22;
            this.ListBox_EmailTemplates.Location = new System.Drawing.Point(31, 173);
            this.ListBox_EmailTemplates.Name = "ListBox_EmailTemplates";
            this.ListBox_EmailTemplates.Size = new System.Drawing.Size(454, 642);
            this.ListBox_EmailTemplates.TabIndex = 16;
            this.ListBox_EmailTemplates.SelectedIndexChanged += new System.EventHandler(this.ListBox_EmailTemplates_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 19.96639F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 46);
            this.label1.TabIndex = 17;
            this.label1.Text = "Email Templates";
            // 
            // Button_CreateNew
            // 
            this.Button_CreateNew.BackColor = System.Drawing.Color.ForestGreen;
            this.Button_CreateNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_CreateNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_CreateNew.Font = new System.Drawing.Font("Segoe UI Semibold", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.Button_CreateNew.ForeColor = System.Drawing.Color.White;
            this.Button_CreateNew.Image = ((System.Drawing.Image)(resources.GetObject("Button_CreateNew.Image")));
            this.Button_CreateNew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Button_CreateNew.Location = new System.Drawing.Point(50, 852);
            this.Button_CreateNew.Margin = new System.Windows.Forms.Padding(0);
            this.Button_CreateNew.Name = "Button_CreateNew";
            this.Button_CreateNew.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.Button_CreateNew.Size = new System.Drawing.Size(205, 88);
            this.Button_CreateNew.TabIndex = 19;
            this.Button_CreateNew.Text = "Create New";
            this.Button_CreateNew.UseVisualStyleBackColor = false;
            this.Button_CreateNew.Click += new System.EventHandler(this.Button_CreateNew_Click);
            // 
            // metroLabel9
            // 
            this.metroLabel9.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.metroLabel9.Location = new System.Drawing.Point(598, 91);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(10, 981);
            this.metroLabel9.TabIndex = 26;
            // 
            // Button_Close
            // 
            this.Button_Close.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Button_Close.Location = new System.Drawing.Point(374, 852);
            this.Button_Close.Name = "Button_Close";
            this.Button_Close.Size = new System.Drawing.Size(205, 88);
            this.Button_Close.Style = MetroFramework.MetroColorStyle.Black;
            this.Button_Close.TabIndex = 27;
            this.Button_Close.Text = "Close";
            this.Button_Close.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Button_Close.UseSelectable = true;
            this.Button_Close.Click += new System.EventHandler(this.Button_Close_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 19.96639F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(629, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(432, 46);
            this.label2.TabIndex = 28;
            this.label2.Text = "Edit/Create email template";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(638, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 17);
            this.label3.TabIndex = 29;
            this.label3.Text = "Template name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(638, 300);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 17);
            this.label4.TabIndex = 30;
            this.label4.Text = "Subject";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(638, 357);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 17);
            this.label5.TabIndex = 31;
            this.label5.Text = "Body";
            // 
            // ComboBox_TemplateStrings
            // 
            this.ComboBox_TemplateStrings.FormattingEnabled = true;
            this.ComboBox_TemplateStrings.ItemHeight = 23;
            this.ComboBox_TemplateStrings.Location = new System.Drawing.Point(764, 173);
            this.ComboBox_TemplateStrings.Name = "ComboBox_TemplateStrings";
            this.ComboBox_TemplateStrings.Size = new System.Drawing.Size(564, 29);
            this.ComboBox_TemplateStrings.TabIndex = 32;
            this.ComboBox_TemplateStrings.UseSelectable = true;
            this.ComboBox_TemplateStrings.SelectedIndexChanged += new System.EventHandler(this.ComboBox_TemplateStrings_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(638, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 17);
            this.label6.TabIndex = 33;
            this.label6.Text = "Template strings";
            // 
            // EditEmailTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1500, 1020);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ComboBox_TemplateStrings);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Button_Close);
            this.Controls.Add(this.metroLabel9);
            this.Controls.Add(this.Button_CreateNew);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ListBox_EmailTemplates);
            this.Controls.Add(this.TextBox_TemplateName);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Save);
            this.Controls.Add(this.TextBox_Body);
            this.Controls.Add(this.TextBox_Subject);
            this.Name = "EditEmailTemplate";
            this.Text = "Email Template";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TextBox_Subject;
        private System.Windows.Forms.TextBox TextBox_Body;
        private MetroFramework.Controls.MetroButton Button_Save;
        private MetroFramework.Controls.MetroButton Button_Cancel;
        private System.Windows.Forms.TextBox TextBox_TemplateName;
        private System.Windows.Forms.ListBox ListBox_EmailTemplates;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Button_CreateNew;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        private MetroFramework.Controls.MetroButton Button_Close;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private MetroFramework.Controls.MetroComboBox ComboBox_TemplateStrings;
        private System.Windows.Forms.Label label6;
    }
}