using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_Project
{
    public static class Dices
    {
        static int c = 0;
        public static int Roll()
        {
            Random rand = new Random();
            int dice1 = rand.Next(1, 7);
            int dice2 = rand.Next(1, 7);
            //to test for doubles
            //dice2 = dice1;
            /*if (c < 7)
            {
                dice2 = dice1;
                c++;
            }*/

            if (dice1 == dice2)
            {
                ActionManager.instance.CurrentPlayer.ConsecutiveDoubles++;
                if (ActionManager.instance.CurrentPlayer.IsInJail)
                {
                    ActionManager.instance.Actions.Clear();
                    ActionManager.AddInstantAction(new GetOutOfJailAction());
                    ActionManager.AddInstantAction(new DummyAction());
                }
            }
            else
            {
                ActionManager.instance.CurrentPlayer.ConsecutiveDoubles = 0;
            }
            return dice1 + dice2;
        }
        public static int RollDouble()
        {
            Random rand = new Random();
            int dice1 = rand.Next(1, 7);
            int dice2 = dice1;

            if (dice1 == dice2)
            {
                ActionManager.instance.CurrentPlayer.ConsecutiveDoubles++;
            }
            else
            {
                ActionManager.instance.CurrentPlayer.ConsecutiveDoubles = 0;
            }
            return dice1 + dice2;
        }
    }
}
