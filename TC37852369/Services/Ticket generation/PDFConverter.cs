using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;

namespace TC37852369.Services.Ticket_generation
{
    public class PDFConverter
    {
        private Application MSdoc;
        public PDFConverter(Application app)
        {
            MSdoc = app;
        }
        public void convertWordToPdf(object filePath, object targetFile)
        {
            object Unknown = Type.Missing;
            // Creating the instance of Word Application
            if (MSdoc == null) MSdoc = new Application();

            try
            {
                MSdoc.Visible = false;
                MSdoc.Documents.Open(ref filePath, ref Unknown,
                     ref Unknown, ref Unknown, ref Unknown,
                     ref Unknown, ref Unknown, ref Unknown,
                     ref Unknown, ref Unknown, ref Unknown,
                     ref Unknown, ref Unknown, ref Unknown, ref Unknown, ref Unknown);
                MSdoc.Application.Visible = false;
                MSdoc.WindowState = Microsoft.Office.Interop.Word.WdWindowState.wdWindowStateMinimize;

                object format = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;

                MSdoc.ActiveDocument.SaveAs(ref targetFile, ref format,
                        ref Unknown, ref Unknown, ref Unknown,
                        ref Unknown, ref Unknown, ref Unknown,
                        ref Unknown, ref Unknown, ref Unknown,
                        ref Unknown, ref Unknown, ref Unknown,
                       ref Unknown, ref Unknown);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
