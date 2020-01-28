using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TC37852369.UI.helpers
{
    public class MetroMessageBoxHelper
    {
        public DialogResult showWarning(MetroForm form,string text, string type)
        {
            return MetroFramework.MetroMessageBox.Show(form, text, type, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public DialogResult showYesNo(MetroForm form, string text, string type)
        {
            return MetroFramework.MetroMessageBox.Show(form, text, type, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
    }
}
