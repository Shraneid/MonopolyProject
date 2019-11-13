using System;
using System.IO;
using System.Reflection;
using static Monopoly_Project.Cell;

namespace Monopoly_Project
{
    public class Gameboard
    {
        public static Gameboard instance;

        public static Cell[] Cells { get; internal set; }
        public static Cell StartCell;
        public static Cell JailCell;

        public static void Init()
        {
            instance = new Gameboard();
            Cells = new Cell[40];
            for (int i = 0; i < 40; i++)
            {
                Cells[i] = Cell.GetCell(CellType.PropertyCell);
            }
            Cells[0] = Cell.GetCell(CellType.StartCell);
            Cells[10] = Cell.GetCell(CellType.JailCell);
            Cells[20] = Cell.GetCell(CellType.FreeParking);
            Cells[30] = Cell.GetCell(CellType.GoToJailCell);

            InstantiatePropertyCells(Cells);

            StartCell = Cells[0];
            JailCell = Cells[10];
        }

        public static void InstantiatePropertyCells(Cell[] Cells)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\Cells.monopoly");
            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < Cells.Length; i++)
            {
                if (Cells[i].Type == CellType.PropertyCell)
                {
                    ((PropertyCell)Cells[i]).Value = Convert.ToDouble(lines[i].Split(',')[0]);
                    ((PropertyCell)Cells[i]).StreetName = lines[i].Split(',')[1];
                }
            }
        }
    }
}