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
            ActionManager.CurrentPlayer.ActualCell = Gameboard.instance.Cells[(ActionManager.CurrentPlayer.ActualCell.Index+NumberOfSteps)%40];
            if ((ActionManager.CurrentPlayer.ActualCell.Index + NumberOfSteps) / 40 > 0)
            {
                ActionManager.Actions.Enqueue(new GetSalaryAction());
            }
        }

        public bool IsLegalMove()
        {
            if (ActionManager.CurrentPlayer.IsInPrison)
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
            ActionManager.CurrentPlayer.Cash += Player.SALARY;
        }

        public bool IsLegalMove()
        {
            return true;
        }
    }
}