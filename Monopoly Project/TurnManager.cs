using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_Project
{
    public static class TurnManager
    {
        public static int Turn { get; set; }
        public static int PlayerIndex { get; set; }
        public static Player[] Players { get; set; }

        public static void Init()
        {
            Turn = 0;
        }

        public static void NextTurn()
        {
            if (Turn == 0)
            {
                InitPlayers();
                EndTurn();
            }
            else
            {
                PlayerIndex = PlayerIndex++ % Players.Length;
                while (Players[PlayerIndex].Bankrupt)
                {
                    PlayerIndex = PlayerIndex++ % Players.Length;
                }
                ActionManager.CurrentPlayer = Players[PlayerIndex];

                //RUN ALL ACTIONS UNTIL EMPTY
                while (ActionManager.Actions.Count != 0)
                {
                    ActionManager.Actions.Dequeue().Execute();
                }

                EndTurn();
            }
        }

        private static void EndTurn()
        {
            Turn++;
            ActionManager.Actions.Clear();
            Console.ReadKey();
        }

        private static void InitPlayers()
        {
            Console.WriteLine("How many players are there");
            int NbPlayers = Convert.ToInt32(Console.ReadLine());
            Players = new Player[NbPlayers];

            for (int i = 0; i < NbPlayers; i++)
            {
                Console.WriteLine("Player " + (i + 1) + "'s name : ");
                Players[i] = (Player.GetNewPlayer(Console.ReadLine()));
            }
        }

        public static bool GameEnded()
        {
            int count = 0;
            foreach (Player p in Players)
            {
                if (!p.Bankrupt)
                {
                    count++;
                }
            }
            if (count > 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
