namespace TC37852369
{
    partial class RegisterParticipationString
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
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.TextBox_ParticipationFormatName = new System.Windows.Forms.TextBox();
            this.Button_Cancel = new MetroFramework.Controls.MetroButton();
            this.Button_Add = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(36, 119);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(167, 20);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Participation format name";
            // 
            // TextBox_ParticipationFormatName
            // 
            this.TextBox_ParticipationFormatName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.TextBox_ParticipationFormatName.Location = new System.Drawing.Point(317, 119);
            this.TextBox_ParticipationFormatName.Name = "TextBox_ParticipationFormatName";
            this.TextBox_ParticipationFormatName.Size = new System.Drawing.Size(315, 30);
            this.TextBox_ParticipationFormatName.TabIndex = 1;
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.BackColor = System.Drawing.Color.Silver;
            this.Button_Cancel.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Button_Cancel.Location = new System.Drawing.Point(19, 209);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(200, 75);
            this.Button_Cancel.Style = MetroFramework.MetroColorStyle.Black;
            this.Button_Cancel.TabIndex = 15;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Button_Cancel.UseCustomBackColor = true;
            this.Button_Cancel.UseSelectable = true;
            this.Button_Cancel.UseStyleColors = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Button_Add
            // 
            this.Button_Add.BackColor = System.Drawing.Color.Maroon;
            this.Button_Add.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.Button_Add.Location = new System.Drawing.Point(475, 209);
            this.Button_Add.Name = "Button_Add";
            this.Button_Add.Size = new System.Drawing.Size(200, 75);
            this.Button_Add.Style = MetroFramework.MetroColorStyle.White;
            this.Button_Add.TabIndex = 14;
            this.Button_Add.Text = "Add";
            this.Button_Add.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Button_Add.UseCustomBackColor = true;
            this.Button_Add.UseSelectable = true;
            this.Button_Add.UseStyleColors = true;
            this.Button_Add.Click += new System.EventHandler(this.Button_Add_Click);
            // 
            // RegisterParticipationString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 364);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Add);
            this.Controls.Add(this.TextBox_ParticipationFormatName);
            this.Controls.Add(this.metroLabel1);
            this.Name = "RegisterParticipationString";
            this.Text = "Add new participation format";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.TextBox TextBox_ParticipationFormatName;
        private MetroFramework.Controls.MetroButton Button_Cancel;
        private MetroFramework.Controls.MetroButton Button_Add;
    }
}