namespace Monopoly_Project
{
    public interface MonopolyAction
    {
        bool IsLegalMove();
        void Execute();
    }

    public class MoveAction : MonopolyAction
    {
        public int NumberOfSteps { get; set; }
        public MoveAction(int numberOfSteps)
        {
            NumberOfSteps = numberOfSteps;
        }

        public void Execute()
        {
            ActionManager.instance.CurrentPlayer.ActualCell = Gameboard.instance.Cells[(ActionManager.instance.CurrentPlayer.ActualCell.Index+NumberOfSteps)%40];
            if ((ActionManager.instance.CurrentPlayer.ActualCell.Index + NumberOfSteps) / 40 > 0)
            {
                ActionManager.instance.Actions.Enqueue(new GetSalaryAction());
            }
        }

        public bool IsLegalMove()
        {
            if (ActionManager.instance.CurrentPlayer.IsInPrison)
            {
                return false;
            }
            return true;
        }
    }

    public class GetSalaryAction : MonopolyAction
    {
        public int NumberOfSteps { get; set; }
        public GetSalaryAction(){}

        public void Execute()
        {
            ActionManager.instance.CurrentPlayer.Cash += Player.SALARY;
        }

        public bool IsLegalMove()
        {
            return true;
        }
    }
}