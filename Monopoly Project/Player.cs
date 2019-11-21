using System;

namespace Monopoly_Project
{
    public class Player
    {
        private const double BASE_CASH = 1500;
        public static double SALARY = 200;

        public double Cash { get; set; }
        public Cell CurrentCell { get; set; }
        public string Name { get; set; }
        public bool IsInJail { get; set; }
        public int TurnsInJail { get; set; }
        public int ConsecutiveDoubles { get; set; }
        public bool Bankrupt { get; set; }

        public Player(string name)
        {
            Cash = BASE_CASH;
            CurrentCell = Gameboard.StartCell;
            Name = name;
            Bankrupt = false;
        }

        public Player(double cash, Cell actualCell, string name)
        {
            Cash = cash;
            CurrentCell = actualCell;
            Name = name;
        }
        
        //FACTORY
        public static Player GetNewPlayer(String name)
        {
            Player p = new Player(name);
            return p;
        }
    }
}