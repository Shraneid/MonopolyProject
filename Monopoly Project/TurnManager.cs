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
        public static TurnManager instance { get; set; }
        public int Turn { get; set; }
        public int PlayerIndex { get; set; }
        public Player[] Players { get; set; }

        public static void Init()
        {
            instance = new TurnManager();
            instance.Turn = 0;
            instance.PlayerIndex = -1;
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
                ActionManager.ResolveActions();

                EndTurn();
            }
        }

        private static void EndTurn()
        {
            if (instance.Turn > 0)
            {
                if ((ActionManager.instance.CurrentPlayer.ActualCell).Type == CellType.PropertyCell)
                {
                    PropertyCell cell = ((PropertyCell)ActionManager.instance.CurrentPlayer.ActualCell);
                    Console.WriteLine(ActionManager.instance.CurrentPlayer.Name + "'s cell : " + cell.StreetName + ", " + cell.Value + "$");
                }
            }
            instance.Turn++;
            ActionManager.instance.Actions.Clear();
            Console.ReadKey();
        }

        private static void InitPlayers()
        {
            Console.WriteLine("How many players are there");
            int NbPlayers = Convert.ToInt32(Console.ReadLine());
            instance.Players = new Player[NbPlayers];

            for (int i = 0; i < NbPlayers; i++)
            {
                Console.WriteLine("Player " + (i + 1) + "'s name : ");
                instance.Players[i] = (Player.GetNewPlayer(Console.ReadLine()));
            }
        }

        public static bool GameEnded()
        {
            int count = 0;
            foreach (Player p in instance.Players)
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
