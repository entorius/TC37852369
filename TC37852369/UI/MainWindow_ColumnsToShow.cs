using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TC37852369.UI
{
    public partial class MainWindow_ColumnsToShow : MetroForm
    {
        MainWindow mainWindow;
        List<MetroCheckBox> checkBoxList = new List<MetroCheckBox>();
        public MainWindow_ColumnsToShow(MainWindow mainWindow)
        {
           
            this.mainWindow = mainWindow;
            
            InitializeComponent();

            bool toMaximize = WindowHelper.checkIfMaximizeWindow(this.Width, this.Height);
            if (toMaximize)
            {
                this.WindowState = FormWindowState.Maximized;
            }

            checkBoxList.Add(CheckBox_FirstName);
            checkBoxList.Add(CheckBox_LastName);
            checkBoxList.Add(CheckBox_CompanyType);
            checkBoxList.Add(CheckBox_JobTitle);
            checkBoxList.Add(CheckBox_CompanyName);
            checkBoxList.Add(CheckBox_ParticipationFormat);
            checkBoxList.Add(CheckBox_PaymentStatus);
            checkBoxList.Add(CheckBox_PaymentAmount);
            checkBoxList.Add(CheckBox_TicketSent);
            checkBoxList.Add(CheckBox_Edit);
            checkBoxList.Add(CheckBox_RegisteredInDay);
            checkBoxList.Add(CheckBox_CheckedInDay);
            checkBoxList.Add(CheckBox_CheckIn);
            for (int i = 0; i <= 12; i++)
            {
                checkBoxList[i].Checked = mainWindow.participantTableColumnShow[i];
            }
            this.FormClosed += ClosedHandler;
        }

        private void CheckBox_CheckAll_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_CheckAll.Checked)
            {
                foreach (MetroCheckBox c in checkBoxList)
                {
                    c.Checked = true;
                }
            }
            else
            {
                foreach (MetroCheckBox c in checkBoxList)
                {
                    c.Checked = false;
                }
            }
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
            this.Dispose();
        }
        protected void ClosedHandler(object sender, EventArgs e)
        {
            mainWindow.Enabled = true;
        }

        private void Button_Confirm_Click(object sender, EventArgs e)
        {
            showHideColumn(CheckBox_FirstName, 0, 150);
            showHideColumn(CheckBox_LastName, 1, 150);
            showHideColumn(CheckBox_CompanyType, 2, 150);
            showHideColumn(CheckBox_JobTitle, 3, 150);
            showHideColumn(CheckBox_CompanyName, 4, 150);
            showHideColumn(CheckBox_ParticipationFormat, 5, 160);
            showHideColumn(CheckBox_PaymentStatus, 6, 100);
            showHideColumn(CheckBox_PaymentAmount, 7, 100);
            showHideColumn(CheckBox_TicketSent, 8, 150);
            showHideColumn(CheckBox_Edit, 9, 150);
            showHideColumn(CheckBox_RegisteredInDay, 10, 100);
            showHideColumn(CheckBox_CheckedInDay, 11, 100);
            showHideColumn(CheckBox_CheckIn, 12, 100);
            for (int i = 0; i<=12; i++)
            {
                mainWindow.participantTableColumnShow[i] = checkBoxList[i].Checked;
            }
            mainWindow.Enabled = true;
            this.Dispose();
        }
        private void showHideColumn(MetroCheckBox checkBox,int columnNumber, int columnWidth)
        {
            if (!checkBox.Checked)
            {
                mainWindow.Table_ParticipantsData1.ColumnStyles[columnNumber].Width = 0;
                mainWindow.table_ParticipantsHeader.ColumnStyles[columnNumber].Width = 0;
            }
            else
            {
                mainWindow.Table_ParticipantsData1.ColumnStyles[columnNumber].Width = columnWidth;
                mainWindow.table_ParticipantsHeader.ColumnStyles[columnNumber].Width = columnWidth;
            }
        }
    }
}
