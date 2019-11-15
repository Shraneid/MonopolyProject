using System;

namespace Monopoly_Project
{
    public abstract class MonopolyAction
    {
        public abstract void Execute();
        public virtual bool IsLegalMove()
        {
            return true;
        }
    }

    public class MoveAction : MonopolyAction
    {
        public int NumberOfSteps { get; set; }
        public MoveAction(int numberOfSteps)
        {
            NumberOfSteps = numberOfSteps;
        }

        public override void Execute()
        {
            Console.WriteLine("You move forward " + NumberOfSteps + " cells");
            ActionManager.instance.CurrentPlayer.ActualCell = Gameboard.instance.Cells[(ActionManager.instance.CurrentPlayer.ActualCell.Index+NumberOfSteps)%40];
            if ((ActionManager.instance.CurrentPlayer.ActualCell.Index + NumberOfSteps) / 40 > 0)
            {
                ActionManager.AddAction(new GetSalaryAction());
            }
        }

        public override bool IsLegalMove()
        {
            if (ActionManager.instance.CurrentPlayer.IsInJail)
            {
                return false;
            }
            return true;
        }
    }

    public class GoToJailAction : MonopolyAction
    {
        public int NumberOfSteps { get; set; }
        public GoToJailAction(){}

        public override void Execute()
        {
            Console.WriteLine("You have gotten 3 doubles in a row, therefore you are caught by the police and " +
                "are sent to jail");
            Player p = ActionManager.instance.CurrentPlayer;
            p.ActualCell = Gameboard.JailCell;
            p.IsInJail = true;
        }
    }

    public class GetSalaryAction : MonopolyAction
    {
        public int NumberOfSteps { get; set; }
        public GetSalaryAction() { }

        public override void Execute()
        {
            ActionManager.instance.CurrentPlayer.Cash += Player.SALARY;
            Console.WriteLine("Player " + ActionManager.instance.CurrentPlayer.Name + " has now " + ActionManager.instance.CurrentPlayer.Cash + "$");
        }
    }

    public class RollDiceAction : MonopolyAction
    {
        public RollDiceAction() { }

        public override void Execute()
        {
            Console.WriteLine(ActionManager.instance.CurrentPlayer.Name + "'s turn to roll the dices, press enter to proceed");
            Console.ReadKey();
            int moveBy = Dices.Roll();
            if (ActionManager.instance.CurrentPlayer.ConsecutiveDoubles != 0)
            {
                Console.WriteLine("Double " + moveBy / 2 + " !");
            }
            ActionManager.AddAction(new MoveAction(moveBy));
        }
    }

    public class GetOutOfJailAction : MonopolyAction
    {
        public GetOutOfJailAction() { }

        public override void Execute()
        {
            ActionManager.instance.CurrentPlayer.IsInJail = false;
        }
    }

    public class DummyAction : MonopolyAction
    {
        public DummyAction() { }
        public override void Execute()
        {
            return;
        }
    }
}