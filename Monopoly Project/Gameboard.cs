using System;
using System.IO;
using System.Reflection;
using static Monopoly_Project.Cell;

namespace Monopoly_Project
{
    public class Gameboard
    {
        public static Gameboard instance;

        public Cell[] Cells { get; internal set; }
        public static Cell StartCell;
        public static Cell JailCell;
        public static Cell GoToJailCell;

        public static void Init()
        {
            instance = new Gameboard { };

            //instance.Cells = Program.LoadGame();
            /*try { }
            catch
            {*/
            if (instance.Cells == null)
            {
                instance.Cells = new Cell[40];

                InstantiateCells(instance.Cells);
            }

            StartCell = instance.Cells[0];
            JailCell = instance.Cells[10];
            GoToJailCell = instance.Cells[30];
            //}
        }

        public static void InstantiateCells(Cell[] Cells)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\CellsConfig.monopoly");
            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < Cells.Length; i++)
            {
                CellType type = Cell.GetType(lines[i].Split(',')[0]);
                Cells[i] = Cell.GetCell(i, type);
                if (Cells[i].Type == CellType.PropertyCell)
                {
                    ((PropertyCell)Cells[i]).Value = Convert.ToDouble(lines[i].Split(',')[1]);
                    ((PropertyCell)Cells[i]).StreetName = lines[i].Split(',')[2];
                }
            }
        }
    }
}