using System;
using Monopoly_Project;

namespace MonopolyUnitTest
{
    public class PlayerFactory
    {
        public static double BASE_CASH = 200;

        public static Player getNewPlayer()
        {
            Player p = new Player(BASE_CASH);
            return p;
        }
    }
}