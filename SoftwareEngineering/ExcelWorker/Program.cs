using System;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.Excel;

namespace ExcelWorker
{
    internal class Program
    {
        private const string FileName = "gruppa";

        private static void Main()
        {
            var ex = new Application {DisplayAlerts = false, Visible = false};
            var curDirectory = Directory.GetCurrentDirectory();
            try
            {
                WriteToTable(ex, curDirectory);
                ReadFromTable(ex, curDirectory);
            }
            finally
            {
                ex.Quit();
            }

            Console.ReadKey();
        }

        private static void ReadFromTable(Application ex, string curDirectory)
        {
            var book = ex.Workbooks.Open(curDirectory + $"\\{FileName}.xlsx");
            var sheet = (Worksheet) book.Sheets[1];
            var lastCell = sheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell);
            var data = new string[lastCell.Column, lastCell.Row];
            for (var i = 0; i < lastCell.Column; i++)
            for (var j = 0; j < lastCell.Row; j++)
                data[i, j] = sheet.Cells[j + 1, i + 1].Text.ToString();
            book.Close(false, Type.Missing, Type.Missing);
            for (var i = 0; i < data.GetLength(1); i++)
            {
                for (var j = 0; j < data.GetLength(0); j++)
                    Console.Write($"| {data[j, i],15} |");
                Console.WriteLine();
            }
        }

        private static void WriteToTable(Application ex, string curDirectory)
        {
            var book = ex.Workbooks.Add(1);
            var sheet = (Worksheet) book.Sheets[1];
            sheet.Name = "Группа ЭМА-18-2";
            var data = File.ReadAllLines($"{FileName}.txt").Select(x => x.Split()).ToArray();
            for (var i = 0; i < data.Length; i++)
            for (var j = 0; j < data[i].Length; j++)
                sheet.Cells[i + 1, j + 1] = data[i][j];

            book.SaveAs(curDirectory + $"\\{FileName}.xlsx");
            book.Close();
            Console.WriteLine($"Файл сохранен в {curDirectory}");
        }
    }
}