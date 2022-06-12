using System;
using System.Diagnostics;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelToCsv
{
    internal class Program
    {
        private static string tablePath = null;
        private static string csvPath = null;
        
        public static void Main(string[] args)
        {
            InitPath();

            if(!Directory.Exists(tablePath))
                Console.WriteLine($"Missing Directory: {tablePath}");

            CreatePath(csvPath);

            OpenExcel(out Excel.Workbook xlWorkBook);
            
            foreach (Excel.Worksheet sheet in xlWorkBook.Worksheets)
                SaveSheet(sheet, csvPath);
            
            xlWorkBook.Close(false);
        }

        
        private static void InitPath()
        {
            Directory.SetCurrentDirectory($"{Directory.GetCurrentDirectory()}/Assets");
            
            tablePath = $"{Directory.GetCurrentDirectory()}/Editor/Data/Excel/";
            csvPath = $"{Directory.GetCurrentDirectory()}/Editor/Data/Csv/";
        }

        private static void OpenExcel(out Excel.Workbook xlWorkBook)
        {
            Excel.Application xlApp = new Excel.ApplicationClass();
            
            xlWorkBook = xlApp.Workbooks.Open($"{tablePath}ExcelData.xlsx");

            xlApp.Visible = true;
            xlApp.DisplayAlerts = false;
        }

        private static void SaveSheet(Excel.Worksheet sheet,string savePath)
        {
            if(sheet.Name.Contains("_")) return;
            
            string sheetPath = $"{savePath}{sheet.Name}.csv";

            sheet.Select();

            sheet.SaveAs(sheetPath, 62);
        }

        private static void CreatePath(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}