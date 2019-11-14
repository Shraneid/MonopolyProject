using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_Project
{
    public class ActionManager
    {
        public static ActionManager instance;
        public Player CurrentPlayer { get; set; }
        public Queue<MonopolyAction> Actions { get; set; }

        public static void Init()
        {
            instance = new ActionManager();
            instance.Actions = new Queue<MonopolyAction>();
        }

        internal static void ResolveActions()
        {
            instance.Actions.Enqueue(new MoveAction(3));

            //RUN ALL ACTIONS UNTIL EMPTY
            while (instance.Actions.Count != 0)
            {
                instance.Actions.Dequeue().Execute();
            }
        }
    }
}
