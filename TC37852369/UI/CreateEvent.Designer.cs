namespace TC37852369
{
    partial class CreateEvent
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
            this.TextBox_EventName = new System.Windows.Forms.TextBox();
            this.TextBox_VenueName = new System.Windows.Forms.TextBox();
            this.TextBox_VenueAdress = new System.Windows.Forms.TextBox();
            this.TextBox_Body = new System.Windows.Forms.TextBox();
            this.CheckBox_UseDefaultEmail = new System.Windows.Forms.CheckBox();
            this.Button_Create = new MetroFramework.Controls.MetroButton();
            this.Button_Cancel = new MetroFramework.Controls.MetroButton();
            this.TextBox_Subject = new System.Windows.Forms.TextBox();
            this.ComboBox_EmailTemplate = new MetroFramework.Controls.MetroComboBox();
            this.DateTime_EventDate = new MetroFramework.Controls.MetroDateTime();
            this.ComboBox_EventDuration = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel7 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.Label_Subject = new MetroFramework.Controls.MetroLabel();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel11 = new MetroFramework.Controls.MetroLabel();
            this.TextBox_Comments = new System.Windows.Forms.TextBox();
            this.ComboBox_Status = new MetroFramework.Controls.MetroComboBox();
            this.SuspendLayout();
            // 
            // TextBox_EventName
            // 
            this.TextBox_EventName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TextBox_EventName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_EventName.Location = new System.Drawing.Point(180, 80);
            this.TextBox_EventName.Name = "TextBox_EventName";
            this.TextBox_EventName.Size = new System.Drawing.Size(434, 30);
            this.TextBox_EventName.TabIndex = 1;
            // 
            // TextBox_VenueName
            // 
            this.TextBox_VenueName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TextBox_VenueName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_VenueName.Location = new System.Drawing.Point(180, 260);
            this.TextBox_VenueName.Name = "TextBox_VenueName";
            this.TextBox_VenueName.Size = new System.Drawing.Size(434, 30);
            this.TextBox_VenueName.TabIndex = 5;
            // 
            // TextBox_VenueAdress
            // 
            this.TextBox_VenueAdress.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TextBox_VenueAdress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_VenueAdress.Location = new System.Drawing.Point(180, 320);
            this.TextBox_VenueAdress.Name = "TextBox_VenueAdress";
            this.TextBox_VenueAdress.Size = new System.Drawing.Size(434, 30);
            this.TextBox_VenueAdress.TabIndex = 6;
            // 
            // TextBox_Body
            // 
            this.TextBox_Body.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TextBox_Body.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_Body.Location = new System.Drawing.Point(850, 170);
            this.TextBox_Body.Multiline = true;
            this.TextBox_Body.Name = "TextBox_Body";
            this.TextBox_Body.Size = new System.Drawing.Size(415, 290);
            this.TextBox_Body.TabIndex = 9;
            // 
            // CheckBox_UseDefaultEmail
            // 
            this.CheckBox_UseDefaultEmail.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CheckBox_UseDefaultEmail.AutoSize = true;
            this.CheckBox_UseDefaultEmail.Location = new System.Drawing.Point(850, 493);
            this.CheckBox_UseDefaultEmail.Name = "CheckBox_UseDefaultEmail";
            this.CheckBox_UseDefaultEmail.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CheckBox_UseDefaultEmail.Size = new System.Drawing.Size(189, 21);
            this.CheckBox_UseDefaultEmail.TabIndex = 10;
            this.CheckBox_UseDefaultEmail.Text = "Use default mail template";
            this.CheckBox_UseDefaultEmail.UseVisualStyleBackColor = true;
            this.CheckBox_UseDefaultEmail.CheckedChanged += new System.EventHandler(this.CheckBox_UseDefaultEmail_CheckedChanged);
            // 
            // Button_Create
            // 
            this.Button_Create.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Button_Create.BackColor = System.Drawing.Color.Maroon;
            this.Button_Create.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Button_Create.Location = new System.Drawing.Point(1040, 614);
            this.Button_Create.Name = "Button_Create";
            this.Button_Create.Size = new System.Drawing.Size(197, 68);
            this.Button_Create.Style = MetroFramework.MetroColorStyle.White;
            this.Button_Create.TabIndex = 11;
            this.Button_Create.Text = "Create";
            this.Button_Create.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Button_Create.UseCustomBackColor = true;
            this.Button_Create.UseSelectable = true;
            this.Button_Create.UseStyleColors = true;
            this.Button_Create.Click += new System.EventHandler(this.Button_Create_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Button_Cancel.BackColor = System.Drawing.Color.Silver;
            this.Button_Cancel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Button_Cancel.Location = new System.Drawing.Point(62, 614);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(197, 68);
            this.Button_Cancel.Style = MetroFramework.MetroColorStyle.Black;
            this.Button_Cancel.TabIndex = 12;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Button_Cancel.UseCustomBackColor = true;
            this.Button_Cancel.UseSelectable = true;
            this.Button_Cancel.UseStyleColors = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // TextBox_Subject
            // 
            this.TextBox_Subject.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TextBox_Subject.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_Subject.Location = new System.Drawing.Point(850, 84);
            this.TextBox_Subject.Name = "TextBox_Subject";
            this.TextBox_Subject.Size = new System.Drawing.Size(415, 30);
            this.TextBox_Subject.TabIndex = 13;
            // 
            // ComboBox_EmailTemplate
            // 
            this.ComboBox_EmailTemplate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ComboBox_EmailTemplate.FormattingEnabled = true;
            this.ComboBox_EmailTemplate.ItemHeight = 24;
            this.ComboBox_EmailTemplate.Location = new System.Drawing.Point(850, 547);
            this.ComboBox_EmailTemplate.Name = "ComboBox_EmailTemplate";
            this.ComboBox_EmailTemplate.Size = new System.Drawing.Size(415, 30);
            this.ComboBox_EmailTemplate.Style = MetroFramework.MetroColorStyle.Orange;
            this.ComboBox_EmailTemplate.TabIndex = 14;
            this.ComboBox_EmailTemplate.UseSelectable = true;
            // 
            // DateTime_EventDate
            // 
            this.DateTime_EventDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DateTime_EventDate.Location = new System.Drawing.Point(180, 140);
            this.DateTime_EventDate.MinimumSize = new System.Drawing.Size(0, 30);
            this.DateTime_EventDate.Name = "DateTime_EventDate";
            this.DateTime_EventDate.Size = new System.Drawing.Size(434, 30);
            this.DateTime_EventDate.TabIndex = 15;
            // 
            // ComboBox_EventDuration
            // 
            this.ComboBox_EventDuration.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ComboBox_EventDuration.FormattingEnabled = true;
            this.ComboBox_EventDuration.ItemHeight = 24;
            this.ComboBox_EventDuration.Location = new System.Drawing.Point(180, 200);
            this.ComboBox_EventDuration.Name = "ComboBox_EventDuration";
            this.ComboBox_EventDuration.Size = new System.Drawing.Size(434, 30);
            this.ComboBox_EventDuration.TabIndex = 16;
            this.ComboBox_EventDuration.UseSelectable = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(23, 140);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(75, 20);
            this.metroLabel1.TabIndex = 17;
            this.metroLabel1.Text = "Event date";
            // 
            // metroLabel2
            // 
            this.metroLabel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(23, 200);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(99, 20);
            this.metroLabel2.TabIndex = 18;
            this.metroLabel2.Text = "Event duration";
            // 
            // metroLabel3
            // 
            this.metroLabel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroLabel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.metroLabel3.Location = new System.Drawing.Point(666, 28);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(2, 624);
            this.metroLabel3.TabIndex = 19;
            // 
            // metroLabel4
            // 
            this.metroLabel4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(23, 80);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(83, 20);
            this.metroLabel4.TabIndex = 20;
            this.metroLabel4.Text = "Event name";
            // 
            // metroLabel5
            // 
            this.metroLabel5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(23, 260);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(88, 20);
            this.metroLabel5.TabIndex = 21;
            this.metroLabel5.Text = "Venue name";
            // 
            // metroLabel6
            // 
            this.metroLabel6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.Location = new System.Drawing.Point(23, 320);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(93, 20);
            this.metroLabel6.TabIndex = 22;
            this.metroLabel6.Text = "Venue adress";
            // 
            // metroLabel7
            // 
            this.metroLabel7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroLabel7.AutoSize = true;
            this.metroLabel7.Location = new System.Drawing.Point(23, 380);
            this.metroLabel7.Name = "metroLabel7";
            this.metroLabel7.Size = new System.Drawing.Size(45, 20);
            this.metroLabel7.TabIndex = 24;
            this.metroLabel7.Text = "Status";
            // 
            // metroLabel8
            // 
            this.metroLabel8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.Location = new System.Drawing.Point(23, 440);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(76, 20);
            this.metroLabel8.TabIndex = 26;
            this.metroLabel8.Text = "Comments";
            // 
            // Label_Subject
            // 
            this.Label_Subject.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Label_Subject.AutoSize = true;
            this.Label_Subject.Location = new System.Drawing.Point(704, 84);
            this.Label_Subject.Name = "Label_Subject";
            this.Label_Subject.Size = new System.Drawing.Size(90, 20);
            this.Label_Subject.TabIndex = 27;
            this.Label_Subject.Text = "Email subject";
            // 
            // metroLabel10
            // 
            this.metroLabel10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.Location = new System.Drawing.Point(704, 165);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(77, 20);
            this.metroLabel10.TabIndex = 28;
            this.metroLabel10.Text = "Email body";
            // 
            // metroLabel11
            // 
            this.metroLabel11.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.metroLabel11.AutoSize = true;
            this.metroLabel11.Location = new System.Drawing.Point(696, 547);
            this.metroLabel11.Name = "metroLabel11";
            this.metroLabel11.Size = new System.Drawing.Size(100, 20);
            this.metroLabel11.TabIndex = 29;
            this.metroLabel11.Text = "Email template";
            // 
            // TextBox_Comments
            // 
            this.TextBox_Comments.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TextBox_Comments.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_Comments.Location = new System.Drawing.Point(180, 440);
            this.TextBox_Comments.Multiline = true;
            this.TextBox_Comments.Name = "TextBox_Comments";
            this.TextBox_Comments.Size = new System.Drawing.Size(434, 137);
            this.TextBox_Comments.TabIndex = 30;
            // 
            // ComboBox_Status
            // 
            this.ComboBox_Status.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ComboBox_Status.FormattingEnabled = true;
            this.ComboBox_Status.ItemHeight = 24;
            this.ComboBox_Status.Location = new System.Drawing.Point(180, 380);
            this.ComboBox_Status.Name = "ComboBox_Status";
            this.ComboBox_Status.Size = new System.Drawing.Size(434, 30);
            this.ComboBox_Status.TabIndex = 31;
            this.ComboBox_Status.UseSelectable = true;
            // 
            // CreateEvent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1295, 705);
            this.Controls.Add(this.ComboBox_Status);
            this.Controls.Add(this.TextBox_Comments);
            this.Controls.Add(this.metroLabel11);
            this.Controls.Add(this.metroLabel10);
            this.Controls.Add(this.Label_Subject);
            this.Controls.Add(this.metroLabel8);
            this.Controls.Add(this.metroLabel7);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.ComboBox_EventDuration);
            this.Controls.Add(this.DateTime_EventDate);
            this.Controls.Add(this.ComboBox_EmailTemplate);
            this.Controls.Add(this.TextBox_Subject);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Create);
            this.Controls.Add(this.CheckBox_UseDefaultEmail);
            this.Controls.Add(this.TextBox_Body);
            this.Controls.Add(this.TextBox_VenueAdress);
            this.Controls.Add(this.TextBox_VenueName);
            this.Controls.Add(this.TextBox_EventName);
            this.Name = "CreateEvent";
            this.Text = "CreateEvent";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TextBox_EventName;
        private System.Windows.Forms.TextBox TextBox_VenueName;
        private System.Windows.Forms.TextBox TextBox_VenueAdress;
        private System.Windows.Forms.TextBox TextBox_Body;
        private System.Windows.Forms.CheckBox CheckBox_UseDefaultEmail;
        private MetroFramework.Controls.MetroButton Button_Create;
        private MetroFramework.Controls.MetroButton Button_Cancel;
        private System.Windows.Forms.TextBox TextBox_Subject;
        private MetroFramework.Controls.MetroComboBox ComboBox_EmailTemplate;
        private MetroFramework.Controls.MetroDateTime DateTime_EventDate;
        private MetroFramework.Controls.MetroComboBox ComboBox_EventDuration;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroLabel metroLabel7;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private MetroFramework.Controls.MetroLabel Label_Subject;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        private MetroFramework.Controls.MetroLabel metroLabel11;
        private System.Windows.Forms.TextBox TextBox_Comments;
        private MetroFramework.Controls.MetroComboBox ComboBox_Status;
    }
}