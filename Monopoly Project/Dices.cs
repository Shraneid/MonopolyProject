using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_Project
{
    static class Dices
    {
        static bool is_double;
        static int Roll()
        {
            Random rand = new Random();
            int dice1 = rand.Next(1,7);
            int dice2 = rand.Next(1, 7);
            if (dice1.Equals(dice2))
            {
                is_double = true;
            }
            else
            {
                is_double = false;
            }
            return dice1 + dice2;
        }


    }
}
