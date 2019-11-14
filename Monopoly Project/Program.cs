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
            Gameboard.Init();
            ActionManager.Init();
            TurnManager.Init();

            //FOR DEBUGGING

            Gameboard g = Gameboard.instance;
            ActionManager a = ActionManager.instance;
            TurnManager t = TurnManager.instance;

            do
            {
                TurnManager.instance.NextTurn();
            } while (!TurnManager.GameEnded());

            Console.ReadKey();
        }
    }
}
