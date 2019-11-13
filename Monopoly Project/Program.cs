using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            int NbPlayers = 2;
            List<Player> players = new List<Player>();
            Gameboard.Init();
            Gameboard g = Gameboard.instance;

            for (int i = 0; i < NbPlayers; i++)
            {
                Console.WriteLine("Player " + (i+1) + "'s name : ");
                players.Add(Player.GetNewPlayer(Console.ReadLine()));
            }

            Console.ReadKey();
        }
    }
}
