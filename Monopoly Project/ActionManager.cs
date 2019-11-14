using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_Project
{
    public static class ActionManager
    {
        public static Player CurrentPlayer { get; set; }
        public static Queue<MonopolyAction> Actions { get; set; }

        public static void Init()
        {
            Actions = new Queue<MonopolyAction>();
        }
    }
}
