using System;

namespace Monopoly_Project
{
    [Serializable]
    public abstract class Cell
    {
        public enum CellType
        {
            StartCell,
            PropertyCell,
            JailCell,
            GoToJailCell,
            FreeParking,
            Bug
        }

        public Cell(){}

        public static String ToString(CellType cellType)
        {
            String txt = "";

            if (cellType == CellType.StartCell)
            {
                txt = "Start Cell";
            }
            if (cellType == CellType.PropertyCell)
            {
                txt = "Property Cell";
            }
            if (cellType == CellType.JailCell)
            {
                txt = "Jail Cell";
            }
            if (cellType == CellType.GoToJailCell)
            {
                txt = "go to Jail Cell";
            }
            if (cellType == CellType.FreeParking)
            {
                txt = "Free Parking";
            }
            if (cellType == CellType.Bug)
            {
                txt = "Bug";
            }

            return txt;
        }

        public static CellType GetType(string v)
        {
            CellType type = CellType.Bug;

            if (v == "StartCell")
            {
                type = CellType.StartCell;
            }
            if (v == "PropertyCell")
            {
                type = CellType.PropertyCell;
            }
            if (v == "JailCell")
            {
                type = CellType.JailCell;
            }
            if (v == "GoToJailCell")
            {
                type = CellType.GoToJailCell;
            }
            if (v == "FreeParking")
            {
                type = CellType.FreeParking;
            }
            return type;
        }

        public CellType Type { get; set; }
        public MonopolyAction Action { get; set; }
        public int Index { get; set; }

        internal static Cell GetCell(int index, CellType cellType)
        {
            Cell cell = null;
            if (cellType == CellType.StartCell)
            {
                cell = new StartCell();
            }
            if (cellType == CellType.PropertyCell)
            {
                cell = new PropertyCell();
            }
            if (cellType == CellType.JailCell)
            {
                cell = new JailCell();
            }
            if (cellType == CellType.GoToJailCell)
            {
                cell = new GoToJailCell();
            }
            if (cellType == CellType.FreeParking)
            {
                cell = new FreeParking();
            }
            cell.Index = index;
            return cell;
        }

        public class PropertyCell : Cell
        {
            public double Value { get; set; }
            public String StreetName { get; set; }

            public PropertyCell()
            {
                Type = CellType.PropertyCell;
            }
        }

        public class StartCell : Cell
        {
            public const double Salary = 20;

            public StartCell()
            {
                Type = CellType.StartCell;
            }
        }
        public class JailCell : Cell
        {
            public JailCell()
            {
                Type = CellType.JailCell;
            }
        }
        public class GoToJailCell : Cell
        {
            public GoToJailCell()
            {
                Type = CellType.GoToJailCell;
            }
        }
        public class FreeParking : Cell
        {
            public FreeParking()
            {
                Type = CellType.FreeParking;
            }
        }
    }
}