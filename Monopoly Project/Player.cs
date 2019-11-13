using System;

namespace Monopoly_Project
{
    public class Player
    {
        private const double BASE_CASH = 200;
        public double Cash { get; set; }
        public Cell ActualCell { get; set; }
        public string Name { get; set; }

        public Player(string name)
        {
            Cash = BASE_CASH;
            ActualCell = Gameboard.StartCell;
            Name = name;
        }

        public Player(double cash, Cell actualCell, string name)
        {
            Cash = cash;
            ActualCell = actualCell;
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