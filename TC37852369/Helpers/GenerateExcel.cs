using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TC37852369.Helpers
{
    public class GenerateExcel
    {
        public bool ExportEventInfo(String eventName,String savingFolderName,String fileName)
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                try
                {
                    List<string> content = new List<string>();
                    content.Add("Yeet");
                    content.Add("Yeet");
                    string excelFilePath = savingFolderName + @"\" + fileName + ".xlsx";
                    
                    FileInfo excelFile = new FileInfo(excelFilePath);
                    ExcelPackage pck = excel;
                    var ws = excel.Workbook.Worksheets.Add("El. paštų sąrašas");
                    ws.Cells["A2"].Value = "Total Registered Attendees";
                    ws.Cells["B2"].Value = "number";
                    ws.Cells["A3"].Value = "First Day Check in total";
                    ws.Cells["B3"].Value = "number";
                    ws.Cells["A4"].Value = "Second Day Check in total";
                    ws.Cells["B4"].Value = "number";

                    ws.Cells["B6"].Value = "First Name";
                    ws.Cells["C6"].Value = "Last Name";
                    ws.Cells["D6"].Value = "Company Name";
                    ws.Cells["E6"].Value = "Job title";
                    ws.Cells["F6"].Value = "Company Type";
                    ws.Cells["G6"].Value = "Payment Status";
                    ws.Cells["H6"].Value = "Email";
                    ws.Cells["I6"].Value = "Check In Day 1";
                    ws.Cells["J6"].Value = "Check In Day 2";

                    var firstNameCol =      ws.Cells["B7:B" + /*len*/8];
                    var lastNameCol =       ws.Cells["C7:C" + /*len*/8];
                    var companyNameCol =    ws.Cells["D7:D" + /*len*/8];
                    var jobTitleCol =       ws.Cells["E7:E" + /*len*/8];
                    var companyTypeCol =    ws.Cells["F7:F" + /*len*/8];
                    var paymentStatusCol =  ws.Cells["G7:G" + /*len*/8];
                    var emailCol =          ws.Cells["H7:H" + /*len*/8];
                    var checkInOneCol =     ws.Cells["I7:I" + /*len*/8];
                    var checkInSecondCol =  ws.Cells["J7:J" + /*currCell+len*/8];
                    
                    firstNameCol.LoadFromCollection(content);
                    lastNameCol.LoadFromCollection(content);
                    companyNameCol.LoadFromCollection(content);
                    jobTitleCol.LoadFromCollection(content);
                    companyTypeCol.LoadFromCollection(content);
                    paymentStatusCol.LoadFromCollection(content);
                    emailCol.LoadFromCollection(content);
                    checkInOneCol.LoadFromCollection(content);
                    checkInSecondCol.LoadFromCollection(content);
                    ws.Column(1).Width = 23;
                    for (int i = 2; i <= 10; i++)
                    {
                        ws.Column(i).Width = 15;
                    }
                    excel.SaveAs(excelFile);
                    ws.View.ShowGridLines = false;
                    excel.SaveAs(excelFile);
                    return true;
                }
                catch (InvalidOperationException e)
                {
                    return false;
                }
            }
        }
    }
}
