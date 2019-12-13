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
        public List<MonopolyAction> Actions { get; set; }

        public static void Init()
        {
            instance = new ActionManager { Actions = new List<MonopolyAction>() };
        }

        internal static void PlayTurn()
        {
            AddAction(new RollDiceAction());

            instance.ResolveActions();
        }

        internal void ResolveActions()
        {
            Player p = instance.CurrentPlayer;

            //If he is stuck in prison for 3 consecutive turns, we immediatly pop him from prison
            if (p.TurnsInJail >= 4 && p.IsInJail)
            {
                AddInstantAction(new GetOutOfJailAction());
                AddInstantAction(new DummyAction());
            }

            //RUN ALL ACTIONS UNTIL EMPTY
            while (instance.Actions.Count > 0)
            {
                /*if (instance.Actions[0].GetType() == typeof(RollDiceAction) && p.ConsecutiveDoubles != 0)
                {
                    
                }*/

                //EXECUTE THE ACTUAL ACTION
                if (instance.Actions[0].IsLegalMove())
                {
                    instance.Actions[0].Execute();
                }

                //MANAGER OF SUCCESSIVE DOUBLE
                if (instance.Actions[0].GetType() == typeof(MoveAction))
                {
                    if (p.ConsecutiveDoubles != 0)
                    {
                        if (p.ConsecutiveDoubles == 3)
                        {
                            p.ConsecutiveDoubles = 0;
                            instance.Actions.Clear();
                            AddAction(new DummyAction());
                            AddAction(new DoublesToJailAction());
                        }
                        else if (p.IsInJail)
                        {
                            AddInstantAction(new GetOutOfJailAction());
                            AddInstantAction(new DummyAction());
                        }
                        else
                        {
                            AddAction(new RollDiceAction());
                        }
                    }
                }

                //If you end up on the GoToJailCell, we need to remove all actions in case you arrived
                //on it by a double on a card
                if (p.ActualCell == Gameboard.GoToJailCell)
                {
                    instance.Actions.Clear();
                    //We need to add a dummy action here, so that it is not removed at the end of this 
                    //loop so the gotojail plays correctly
                    AddAction(new DummyAction());
                    AddAction(new GoToJailAction());
                }

                //REMOVING THE ACTION WHEN IT WAS MANAGED (IT IS NOT POSSIBLE TO USE A QUEUE AS WE NEED
                //TO KEEP A ROLLDICEACTION AT THE END OF THE LIST TO ENSURE THAT THE PLAYERS PLAYS TWICE,
                //CF THE ADD ACTION METHOD FURTHER DOWN)
                instance.Actions.RemoveAt(0);
            }

            //increments 1 when getting in prison directly so we need to check when the player spent 4 turns
            //in jail (3 + the turn in which he arrived)
            if (p.IsInJail)
            {
                p.TurnsInJail++;
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
            if (instance.Actions.Count > 1 && instance.Actions[instance.Actions.Count - 1].GetType() == typeof(RollDiceAction))
            {
                instance.Actions.Insert(instance.Actions.Count - 1, a);
            }
            else
            {
                instance.Actions.Add(a);
            }
        }
        public static void AddInstantAction(MonopolyAction a)
        {
            instance.Actions.Insert(0, a);
        }
    }
}
