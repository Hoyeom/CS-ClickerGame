using System;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelToCsv
{
    internal class Program
    {
        private static string _tablePath = null;
        private static string _csvPath = null;
        private const int CsvUtf8Format = 62;
        
        public static void Main(string[] args)
        {
            InitPath();

            if(!Directory.Exists(_tablePath))
                Console.WriteLine($"Missing Directory: {_tablePath}");

            CreatePath(_csvPath);

            OpenExcel(out Excel.Workbook xlWorkBook);
            
            foreach (Excel.Worksheet sheet in xlWorkBook.Worksheets)
                SaveSheet(sheet, _csvPath);
            
            xlWorkBook.Close(false);
        }
        private static void InitPath()
        {
            Directory.SetCurrentDirectory($"{Directory.GetCurrentDirectory()}/Assets");
            
            _tablePath = $"{Directory.GetCurrentDirectory()}/Editor/Data/Excel/"; 
            _csvPath = $"{Directory.GetCurrentDirectory()}/Editor/Data/Csv/";
        }
        private static void OpenExcel(out Excel.Workbook xlWorkBook)
        {
            Excel.Application xlApp = new Excel.ApplicationClass();
            
            xlWorkBook = xlApp.Workbooks.Open($"{_tablePath}ExcelData.xlsx");

            xlApp.Visible = true;
            xlApp.DisplayAlerts = false;
        }
        private static void SaveSheet(Excel.Worksheet sheet,string savePath)
        {
            if(sheet.Name.Contains("_")) return;
            
            string sheetPath = $"{savePath}{sheet.Name}.csv";

            sheet.Select();

            sheet.SaveAs(sheetPath, CsvUtf8Format); // 62 = CSV UTF-8
        }
        private static void CreatePath(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}