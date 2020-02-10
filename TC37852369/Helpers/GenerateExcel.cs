using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TC37852369.DomainEntities;
using TC37852369.Services;

namespace TC37852369.Helpers
{
    public static class GenerateExcel
    {
        private static ParticipantServices participantServices = new ParticipantServices();
        public static  bool ExportEventInfo(Event eventEntity,List<Participant> participants,string savingFolderName,string fileName)
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
                    for (int i = 2; i <= 11; i++)
                    {
                        ws.Column(i).Width = 15;
                    }
                    excel.SaveAs(excelFile);
                    ws.View.ShowGridLines = false;
                    excel.SaveAs(excelFile);
                    return true;
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
            }
        }
        public static bool ExportAllEventInfo(Event eventEntity, List<Participant> participants, string savingFolderName, string fileName)
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                try
                {
                    List<string> content = new List<string>();
                    string excelFilePath = savingFolderName + @"\" + fileName + ".xlsx";

                    FileInfo excelFile = new FileInfo(excelFilePath);
                    ExcelPackage pck = excel;
                    var ws = excel.Workbook.Worksheets.Add("El. paštų sąrašas");

                    ws.Cells["A1"].Value = "Event name";
                    ws.Cells["A2"].Value = eventEntity.eventName;
                    ws.Cells["A2"].Style.Font.Size = 18;
                    ws.Cells["A2"].Style.Font.Color.SetColor(Color.DarkSeaGreen);

                    ws.Row(2).Height = 20;
                    ws.Column(1).Width = 30;

                    ws.Cells["A4"].Value = "Total Registered Attendees";
                    ws.Cells["B4"].Value = participants.Count;
                    ws.Cells["A5"].Value = "First Day Check in total";
                    ws.Cells["B5"].Value = participantServices.countCheckedInDay(1,participants);
                    if (eventEntity.eventLengthDays >= 2)
                    {
                        ws.Cells["A6"].Value = "Second Day Check in total";
                        ws.Cells["B6"].Value = participantServices.countCheckedInDay(2, participants); ;
                    }
                    if (eventEntity.eventLengthDays >= 3)
                    {
                        ws.Cells["A7"].Value = "Third Day Check in total";
                        ws.Cells["B7"].Value = participantServices.countCheckedInDay(3, participants); ;
                    }
                    if (eventEntity.eventLengthDays == 4)
                    {
                        ws.Cells["A8"].Value = "Fourth Day Check in total";
                        ws.Cells["B8"].Value = participantServices.countCheckedInDay(4, participants); ;
                    }

                    ws.Cells["B10"].Value = "First Name";
                    ws.Cells["C10"].Value = "Last Name";
                    ws.Cells["D10"].Value = "Job title";
                    ws.Cells["E10"].Value = "Company Name";
                    ws.Cells["F10"].Value = "Company Type";
                    ws.Cells["G10"].Value = "Email";
                    ws.Cells["H10"].Value = "Phone Number";
                    ws.Cells["I10"].Value = "Country";
                    ws.Cells["J10"].Value = "Participantion Format";
                    ws.Cells["K10"].Value = "Payment Status";
                    ws.Cells["L10"].Value = "Ticket Barcode";
                    ws.Cells["M10"].Value = "Ticket Sent";
                    ws.Cells["N10"].Value = "Materials";
                    ws.Cells["O10"].Value = "Participating Evening Event";
                    if(eventEntity.eventLengthDays == 1)
                    {
                        ws.Cells["P10"].Value = "Registered In Day 1";
                        ws.Cells["Q10"].Value = "Checked In Day 1";
                    }
                    else if (eventEntity.eventLengthDays == 2)
                    {
                        ws.Cells["P10"].Value = "Registered In Day 1";
                        ws.Cells["Q10"].Value = "Registered In Day 2";
                        ws.Cells["R10"].Value = "Checked In Day 1";
                        ws.Cells["S10"].Value = "Checked In Day 2";
                    }
                    else if (eventEntity.eventLengthDays == 3)
                    {
                        ws.Cells["P10"].Value = "Registered In Day 1";
                        ws.Cells["Q10"].Value = "Registered In Day 2";
                        ws.Cells["R10"].Value = "Registered In Day 3";
                        ws.Cells["S10"].Value = "Checked In Day 1";
                        ws.Cells["T10"].Value = "Checked In Day 2";
                        ws.Cells["U10"].Value = "Checked In Day 3";
                    }
                    else if (eventEntity.eventLengthDays == 4)
                    {
                        ws.Cells["P10"].Value = "Registered In Day 1";
                        ws.Cells["Q10"].Value = "Registered In Day 2";
                        ws.Cells["R10"].Value = "Registered In Day 3";
                        ws.Cells["S10"].Value = "Registered In Day 4";
                        ws.Cells["T10"].Value = "Checked In Day 1";
                        ws.Cells["U10"].Value = "Checked In Day 2";
                        ws.Cells["V10"].Value = "Checked In Day 3";
                        ws.Cells["W10"].Value = "Checked In Day 4";
                    }
                    int len = 11;

                    for (int i=0;i< participants.Count;i++ )
                    {
                        ws.Cells["B" + (len + i).ToString()].Value = participants[i].firstName;
                        ws.Cells["C" + (len + i).ToString()].Value = participants[i].lastName;
                        ws.Cells["D" + (len + i).ToString()].Value = participants[i].jobTitle;
                        ws.Cells["E" + (len + i).ToString()].Value = participants[i].companyName;
                        ws.Cells["F" + (len + i).ToString()].Value = participants[i].companyType;
                        ws.Cells["G" + (len + i).ToString()].Value = participants[i].email;
                        ws.Cells["H" + (len + i).ToString()].Value = participants[i].phoneNumber;
                        ws.Cells["I" + (len + i).ToString()].Value = participants[i].country;
                        ws.Cells["J" + (len + i).ToString()].Value = participants[i].participationFormat;
                        ws.Cells["K" + (len + i).ToString()].Value = participants[i].paymentStatus;
                        ws.Cells["L" + (len + i).ToString()].Value = participants[i].ticketBarcode;
                        ws.Cells["M" + (len + i).ToString()].Value = participants[i].ticketSent ? "Yes" : "No";
                        ws.Cells["N" + (len + i).ToString()].Value = participants[i].materials ? "Yes" : "No";
                        ws.Cells["O" + (len + i).ToString()].Value = participants[i].participateEveningEvent ? "Yes" : "No";

                        if (eventEntity.eventLengthDays == 1)
                        {
                            ws.Cells["P" + (len + i).ToString()].Value = participants[i].participateInDay1 ? "Yes" : "No";
                            ws.Cells["Q" + (len + i).ToString()].Value = participants[i].checkedInDay1 ? "Yes" : "No";
                        }
                        else if (eventEntity.eventLengthDays == 2)
                        {
                            ws.Cells["P" + (len + i).ToString()].Value = participants[i].participateInDay1 ? "Yes" : "No";
                            ws.Cells["Q" + (len + i).ToString()].Value = participants[i].participateInDay2 ? "Yes" : "No";
                            ws.Cells["R" + (len + i).ToString()].Value = participants[i].checkedInDay1 ? "Yes" : "No";
                            ws.Cells["S" + (len + i).ToString()].Value = participants[i].checkedInDay2 ? "Yes" : "No";
                        }
                        else if (eventEntity.eventLengthDays == 3)
                        {
                            ws.Cells["P" + (len + i).ToString()].Value = participants[i].participateInDay1 ? "Yes" : "No";
                            ws.Cells["Q" + (len + i).ToString()].Value = participants[i].participateInDay2 ? "Yes" : "No";
                            ws.Cells["R" + (len + i).ToString()].Value = participants[i].participateInDay3 ? "Yes" : "No";
                            ws.Cells["S" + (len + i).ToString()].Value = participants[i].checkedInDay1 ? "Yes" : "No";
                            ws.Cells["T" + (len + i).ToString()].Value = participants[i].checkedInDay2 ? "Yes" : "No";
                            ws.Cells["U" + (len + i).ToString()].Value = participants[i].checkedInDay3 ? "Yes" : "No";
                        }
                        else if (eventEntity.eventLengthDays == 4)
                        {
                            ws.Cells["P" + (len + i).ToString()].Value = participants[i].participateInDay1 ? "Yes" : "No";
                            ws.Cells["Q" + (len + i).ToString()].Value = participants[i].participateInDay2 ? "Yes" : "No";
                            ws.Cells["R" + (len + i).ToString()].Value = participants[i].participateInDay3 ? "Yes" : "No";
                            ws.Cells["S" + (len + i).ToString()].Value = participants[i].participateInDay4 ? "Yes" : "No";
                            ws.Cells["T" + (len + i).ToString()].Value = participants[i].checkedInDay1 ? "Yes" : "No";
                            ws.Cells["U" + (len + i).ToString()].Value = participants[i].checkedInDay2 ? "Yes" : "No";
                            ws.Cells["V" + (len + i).ToString()].Value = participants[i].checkedInDay3 ? "Yes" : "No";
                            ws.Cells["W" + (len + i).ToString()].Value = participants[i].checkedInDay4 ? "Yes" : "No";
                        }
                    }

                    
                    ws.Column(1).Width = 23;
                    for (int i = 2; i <= 24; i++)
                    {
                        ws.Column(i).Width = 23;
                    }
                    excel.SaveAs(excelFile);
                    ws.View.ShowGridLines = false;
                    excel.SaveAs(excelFile);
                    return true;
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
            }
        }
        public static bool DelegateAllEventInfo(Event eventEntity, List<Participant> participants, string savingFolderName, string fileName)
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                try
                {
                    List<string> content = new List<string>();
                    string excelFilePath = savingFolderName + @"\" + fileName + ".xlsx";

                    FileInfo excelFile = new FileInfo(excelFilePath);
                    ExcelPackage pck = excel;
                    var ws = excel.Workbook.Worksheets.Add("El. paštų sąrašas");

                    ws.Cells["A1"].Value = "Event name";
                    ws.Cells["A2"].Value = eventEntity.eventName;
                    ws.Cells["A2"].Style.Font.Size = 18;
                    ws.Cells["A2"].Style.Font.Color.SetColor(Color.DarkSeaGreen);

                    ws.Row(2).Height = 20;
                    ws.Column(1).Width = 30;

                    ws.Cells["A4"].Value = "Total Registered Attendees";
                    ws.Cells["B4"].Value = participants.Count;
                    

                    ws.Cells["B9"].Value = "First Name";
                    ws.Cells["C9"].Value = "Last Name";
                    ws.Cells["D9"].Value = "Job title";
                    ws.Cells["E9"].Value = "Company Name";
                    ws.Cells["F9"].Value = "Company Type";
                    
                    
                    int len = 10;

                    for (int i = 0; i < participants.Count; i++)
                    {
                        ws.Cells["B" + (len + i).ToString()].Value = participants[i].firstName;
                        ws.Cells["C" + (len + i).ToString()].Value = participants[i].lastName;
                        ws.Cells["D" + (len + i).ToString()].Value = participants[i].jobTitle;
                        ws.Cells["E" + (len + i).ToString()].Value = participants[i].companyName;
                        ws.Cells["F" + (len + i).ToString()].Value = participants[i].companyType;
                    }


                    ws.Column(1).Width = 23;
                    for (int i = 2; i <= 7; i++)
                    {

                        ws.Column(i).Width = 23;
                    }
                    excel.SaveAs(excelFile);
                    ws.View.ShowGridLines = false;
                    excel.SaveAs(excelFile);
                    return true;
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
            }
        }
        public static bool ReportEventInfo(Event eventEntity, List<Participant> participants, string savingFolderName, string fileName)
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                try
                {
                    List<string> content = new List<string>();
                    string excelFilePath = savingFolderName + @"\" + fileName + ".xlsx";

                    FileInfo excelFile = new FileInfo(excelFilePath);
                    ExcelPackage pck = excel;
                    var ws = excel.Workbook.Worksheets.Add("El. paštų sąrašas");


                    ws.Cells["A1"].Value = "Event name";
                    ws.Cells["A2"].Value = eventEntity.eventName;
                    ws.Cells["A2"].Style.Font.Size = 18;
                    ws.Cells["A2"].Style.Font.Color.SetColor(Color.DarkSeaGreen);

                    ws.Row(2).Height = 20;
                    ws.Column(1).Width = 30;

                    ws.Cells["A5"].Value = "Total Registered Attendees";
                    ws.Cells["B5"].Value = participants.Count;
                    ws.Cells["A6"].Value = "First Day Check in total";
                    ws.Cells["B6"].Value = participantServices.countCheckedInDay(1, participants);
                    if (eventEntity.eventLengthDays >= 2)
                    {
                        ws.Cells["A7"].Value = "Second Day Check in total";
                        ws.Cells["B7"].Value = participantServices.countCheckedInDay(2, participants); ;
                    }
                    if (eventEntity.eventLengthDays >= 3)
                    {
                        ws.Cells["A8"].Value = "Third Day Check in total";
                        ws.Cells["B8"].Value = participantServices.countCheckedInDay(3, participants); ;
                    }
                    if (eventEntity.eventLengthDays == 4)
                    {
                        ws.Cells["A9"].Value = "Fourth Day Check in total";
                        ws.Cells["B9"].Value = participantServices.countCheckedInDay(4, participants); ;
                    }

                    ws.Cells["B11"].Value = "First Name"; 
                    ws.Cells["C11"].Value = "Last Name";
                    ws.Cells["D11"].Value = "Job title";
                    ws.Cells["E11"].Value = "Company Name";
                    ws.Cells["F11"].Value = "Company Type";
                    ws.Cells["G11"].Value = "Email";
                    ws.Cells["H11"].Value = "Payment Status";
                    if (eventEntity.eventLengthDays == 1)
                    {
                        ws.Cells["I11"].Value = "Checked In Day 1";
                    }
                    else if (eventEntity.eventLengthDays == 2)
                    {
                        ws.Cells["I11"].Value = "Checked In Day 1";
                        ws.Cells["J11"].Value = "Checked In Day 2";
                    }
                    else if (eventEntity.eventLengthDays == 3)
                    {
                        ws.Cells["I11"].Value = "Checked In Day 1";
                        ws.Cells["J11"].Value = "Checked In Day 2";
                        ws.Cells["K11"].Value = "Checked In Day 3";
                    }
                    else if (eventEntity.eventLengthDays == 4)
                    {
                        ws.Cells["I11"].Value = "Checked In Day 1";
                        ws.Cells["J11"].Value = "Checked In Day 2";
                        ws.Cells["K11"].Value = "Checked In Day 3";
                        ws.Cells["L11"].Value = "Checked In Day 4";
                    }
                    int len = 12;

                    for (int i = 0; i < participants.Count; i++)
                    {
                        ws.Cells["B" + (len + i).ToString()].Value = participants[i].firstName;
                        ws.Cells["C" + (len + i).ToString()].Value = participants[i].lastName;
                        ws.Cells["D" + (len + i).ToString()].Value = participants[i].jobTitle;
                        ws.Cells["E" + (len + i).ToString()].Value = participants[i].companyName;
                        ws.Cells["F" + (len + i).ToString()].Value = participants[i].companyType;
                        ws.Cells["G" + (len + i).ToString()].Value = participants[i].email;
                        ws.Cells["H" + (len + i).ToString()].Value = participants[i].paymentStatus;

                        if (eventEntity.eventLengthDays == 1)
                        {
                            ws.Cells["I" + (len + i).ToString()].Value = participants[i].checkedInDay1 ? "Yes" : "No";
                        }
                        else if (eventEntity.eventLengthDays == 2)
                        {
                            ws.Cells["I" + (len + i).ToString()].Value = participants[i].checkedInDay1 ? "Yes" : "No";
                            ws.Cells["J" + (len + i).ToString()].Value = participants[i].checkedInDay2 ? "Yes" : "No";
                        }
                        else if (eventEntity.eventLengthDays == 3)
                        {
                            ws.Cells["I" + (len + i).ToString()].Value = participants[i].checkedInDay1 ? "Yes" : "No";
                            ws.Cells["J" + (len + i).ToString()].Value = participants[i].checkedInDay2 ? "Yes" : "No";
                            ws.Cells["K" + (len + i).ToString()].Value = participants[i].checkedInDay3 ? "Yes" : "No";
                        }
                        else if (eventEntity.eventLengthDays == 4)
                        {
                            ws.Cells["I" + (len + i).ToString()].Value = participants[i].checkedInDay1 ? "Yes" : "No";
                            ws.Cells["J" + (len + i).ToString()].Value = participants[i].checkedInDay2 ? "Yes" : "No";
                            ws.Cells["K" + (len + i).ToString()].Value = participants[i].checkedInDay3 ? "Yes" : "No";
                            ws.Cells["L" + (len + i).ToString()].Value = participants[i].checkedInDay4 ? "Yes" : "No";
                        }
                    }


                    ws.Column(1).Width = 23;
                    for (int i = 2; i <= 13; i++)
                    {
                        ws.Column(i).Width = 23;
                    }
                    excel.SaveAs(excelFile);
                    ws.View.ShowGridLines = false;
                    excel.SaveAs(excelFile);
                    return true;
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
            }
        }
    }
}
