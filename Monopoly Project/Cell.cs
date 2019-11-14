using System;

namespace Monopoly_Project
{
    public abstract class Cell
    {
        public static class CellType
        {
            public static int StartCell = 0;
            public static int PropertyCell = 1;
            public static int JailCell = 2;
            public static int GoToJailCell = 3;
            public static int FreeParking = 4;

            internal static int GetType(string v)
            {
                if (v == "StartCell")
                {
                    return StartCell;
                }
                if (v == "PropertyCell")
                {
                    return PropertyCell;
                }
                if (v == "JailCell")
                {
                    return JailCell;
                }
                if (v == "GoToJailCell")
                {
                    return GoToJailCell;
                }
                if (v == "FreeParking")
                {
                    return FreeParking;
                }
                return -1;
            }
        }

        public int Type { get; set; }
        public Action Action { get; set; }

        internal static Cell GetCell(int cellType)
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