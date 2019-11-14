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

        public static void Init()
        {
            instance = new Gameboard{Cells = new Cell[40]};

            InstantiatePropertyCells(instance.Cells);

            StartCell = instance.Cells[0];
            JailCell = instance.Cells[10];
        }

        public static void InstantiatePropertyCells(Cell[] Cells)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\..\\CellsConfig.monopoly");
            string[] lines = File.ReadAllLines(path);
                                                    
            for (int i = 0; i < Cells.Length; i++)
            {
                CellType type = Cell.GetType(lines[i].Split(',')[0]);
                Cells[i] = Cell.GetCell(type);
                if (Cells[i].Type == CellType.PropertyCell)
                {
                    ((PropertyCell)Cells[i]).Value = Convert.ToDouble(lines[i].Split(',')[1]);
                    ((PropertyCell)Cells[i]).StreetName = lines[i].Split(',')[2];
                }
            }
        }
    }
}

/*
TurnManager.AddAction(new MoveAction(Dices.Roll()))
//
//  TURNMANAGER LOGIC
//

while(ActionManager.Actions.Length != 0){
    ActionManager.NextAction();
}

if (Dices.WasDouble){
    TurnManager.AddAction(new RollDicesAction())
}*/