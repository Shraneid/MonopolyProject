using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly_Project
{
    public class ActionManager
    {
        public static ActionManager Instance;
        public Player CurrentPlayer { get; set; }
        public List<MonopolyAction> Actions { get; set; }

        public static void Init()
        {
            Instance = new ActionManager();
            Instance.Actions = new List<MonopolyAction>();
        }

        internal static void PlayTurn()
        {
            AddAction(new RollDiceAction());

            Instance.ResolveActions();
        }

        internal void ResolveActions()
        {
            //RUN ALL ACTIONS UNTIL EMPTY
            while (Instance.Actions.Count != 0)
            {
                //EXECUTE THE ACTUAL ACTION
                Instance.Actions[0].Execute();

                Player p = Instance.CurrentPlayer;

                //MANAGER OF SUCCESSIVE DOUBLE
                if (Instance.Actions[0].GetType() == typeof(MoveAction))
                {
                    if (p.ConsecutiveDoubles != 0)
                    {
                        if (p.ConsecutiveDoubles == 3)
                        {
                            p.ConsecutiveDoubles = 0;
                            Instance.Actions.Clear();
                            AddAction(new DummyAction());
                            AddAction(new GoToJailAction());
                        }
                        else if (p.IsInJail)
                        {
                            AddImmediateAction(new GetOutOfJailAction());
                        }
                        else
                        {
                            AddAction(new RollDiceAction());
                        }
                    }
                }
                //If he is stuck in prison for 3 consecutive turns, we immediatly pop him from prison
                //but we do this after checking for doubles, because he shouldn't be able to benefit
                //from the double rule if he gets out of prison
                if (p.TurnsInJail >= 3)
                {
                    AddImmediateAction(new GetOutOfJailAction());
                }

                //REMOVING THE ACTION WHEN IT WAS MANAGED (IT IS NOT POSSIBLE TO USE A QUEUE AS WE NEED
                //TO KEEP A ROLLDICEACTION AT THE END OF THE LIST TO ENSURE THAT THE PLAYERS PLAYS TWICE ON DOUBLE,
                //CF THE ADD ACTION METHOD FURTHER DOWN)
                Instance.Actions.RemoveAt(0);
            }
        }

        public static void AddAction(MonopolyAction a)
        {
            //if the player got a double, we want the action of rerolling the dices to be kept at the end 
            //of the action list when we are resolving all of the actions.
            //as all actions are not defined at the beginning (we don't know if the player will buy a property upon visiting
            //one, we need to make sure it is taken into account here as we keep the rolldice as the last action that should
            //resolve). This is the reason we can't use a Queue instead of a List (or we could by overriding the Enqueue() 
            //method but it wouldn't make much sense and we'd loose the utility of a Queue)
            if (Instance.Actions.Count > 1 && Instance.Actions[Instance.Actions.Count - 1].GetType() == typeof(RollDiceAction))
            {
                Instance.Actions.Insert(Instance.Actions.Count - 1, a);
            }
            else
            {
                Instance.Actions.Add(a);
            }
        }
        public static void AddImmediateAction(MonopolyAction a)
        {
            Instance.Actions.Insert(0, a);
        }
    }
}
