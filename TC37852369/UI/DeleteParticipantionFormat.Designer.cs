namespace TC37852369.UI
{
    partial class DeleteParticipantionFormat
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
            this.ComboBox_ParticipationFormats = new MetroFramework.Controls.MetroComboBox();
            this.Button_Delete = new MetroFramework.Controls.MetroButton();
            this.Button_Cancel = new MetroFramework.Controls.MetroButton();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ComboBox_ParticipationFormats
            // 
            this.ComboBox_ParticipationFormats.FormattingEnabled = true;
            this.ComboBox_ParticipationFormats.ItemHeight = 23;
            this.ComboBox_ParticipationFormats.Location = new System.Drawing.Point(138, 103);
            this.ComboBox_ParticipationFormats.Name = "ComboBox_ParticipationFormats";
            this.ComboBox_ParticipationFormats.Size = new System.Drawing.Size(401, 29);
            this.ComboBox_ParticipationFormats.TabIndex = 0;
            this.ComboBox_ParticipationFormats.UseSelectable = true;
            // 
            // Button_Delete
            // 
            this.Button_Delete.BackColor = System.Drawing.Color.Maroon;
            this.Button_Delete.ForeColor = System.Drawing.Color.White;
            this.Button_Delete.Location = new System.Drawing.Point(468, 211);
            this.Button_Delete.Name = "Button_Delete";
            this.Button_Delete.Size = new System.Drawing.Size(150, 52);
            this.Button_Delete.TabIndex = 1;
            this.Button_Delete.Text = "Delete";
            this.Button_Delete.UseCustomBackColor = true;
            this.Button_Delete.UseCustomForeColor = true;
            this.Button_Delete.UseSelectable = true;
            this.Button_Delete.Click += new System.EventHandler(this.Button_Delete_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.BackColor = System.Drawing.Color.White;
            this.Button_Cancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Button_Cancel.Location = new System.Drawing.Point(62, 211);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(150, 52);
            this.Button_Cancel.TabIndex = 2;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseCustomBackColor = true;
            this.Button_Cancel.UseCustomForeColor = true;
            this.Button_Cancel.UseSelectable = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI Light", 13.91597F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label12.Location = new System.Drawing.Point(23, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(301, 32);
            this.label12.TabIndex = 140;
            this.label12.Text = "Delete Participantion Format";
            // 
            // DeleteParticipantionFormat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(119F, 119F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(699, 300);
            this.ClientSize = new System.Drawing.Size(699, 300);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_Delete);
            this.Controls.Add(this.ComboBox_ParticipationFormats);
            this.Name = "DeleteParticipantionFormat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroComboBox ComboBox_ParticipationFormats;
        private MetroFramework.Controls.MetroButton Button_Delete;
        private MetroFramework.Controls.MetroButton Button_Cancel;
        private System.Windows.Forms.Label label12;
    }
}