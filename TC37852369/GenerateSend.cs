using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace TC37852369
{
    public partial class GenerateSend : MetroForm
    {
        public GenerateSend()
        {
            InitializeComponent();
            List<string> values = new List<string>();
            values.Add("Event1");
            values.Add("Event2");
            values.Add("Event3");

            GenerateEventsCombobox(values);
        }
        void GenerateEventsCombobox(List<string> values)
        {
            foreach (string value in values)
            {
                ComboBox_Events.Items.Add(value);
            }
        }
    }
}
