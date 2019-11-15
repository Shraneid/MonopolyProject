using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Monopoly_Project.Cell;

namespace Monopoly_Project
{
    public class TurnManager
    {
        public static TurnManager Instance { get; set; }
        public int Turn { get; set; }
        public int PlayerIndex { get; set; }
        public Player[] Players { get; set; }

        public static void Init()
        {
            Instance = new TurnManager();
            Instance.Turn = 0;
            Instance.PlayerIndex = -1;
        }

        public void NextTurn()
        {
            if (Turn == 0)
            {
                InitPlayers();
                EndTurn();
            }
            else
            {
                PlayerIndex = ++PlayerIndex % Players.Length;
                while (Players[PlayerIndex].Bankrupt)
                {
                    PlayerIndex = PlayerIndex++ % Players.Length;
                }
                ActionManager.instance.CurrentPlayer = Players[PlayerIndex];
                ActionManager.PlayTurn();

                EndTurn();
            }
        }

        private static void EndTurn()
        {
            Instance.Turn++;
            ActionManager.instance.Actions.Clear();
            Console.ReadKey();
        }

        private static void InitPlayers()
        {
            Console.WriteLine("How many players are there");
            int NbPlayers = Convert.ToInt32(Console.ReadLine());
            Instance.Players = new Player[NbPlayers];

            for (int i = 0; i < NbPlayers; i++)
            {
                Console.WriteLine("Player " + (i + 1) + "'s name : ");
                Instance.Players[i] = (Player.GetNewPlayer(Console.ReadLine()));
            }
        }

        public static bool GameEnded()
        {
            int count = 0;
            foreach (Player p in Instance.Players)
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
