using System;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.Excel;

namespace ExcelToDB
{
    internal class Program
    {
        private const string FileName = "gruppa";

        private static void Main(string[] args)
        {
            var data = ReadFromTable();

            using (var db = new GroupContext())
            {
                for (var i = 0; i < data.GetLength(1); i++)
                    db.Students.Add(new Student
                        {FirstName = data[0, i], SecondName = data[1, i], Birttday = DateTime.Parse(data[2, i])});

                db.SaveChanges();

                var query = from b in db.Students
                    orderby b.Id
                    select b;

                Console.WriteLine("All blogs in the database:");
                foreach (var student in query)
                    Console.WriteLine($"| {student.FirstName,15}|{student.SecondName,15}|{student.Birttday} |");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static string[,] ReadFromTable()
        {
            var ex = new Application();
            var curDirectory = Directory.GetCurrentDirectory();
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

            return data;
        }
    }
}