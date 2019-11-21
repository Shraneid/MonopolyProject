using System;
using static Monopoly_Project.Cell;

namespace Monopoly_Project
{
    public abstract class MonopolyAction
    {
        public abstract void Execute();
        public virtual bool IsLegalMove()
        {
            return true;
        }

        public static void PrintCell(Cell cell)
        {
            if (cell.Type == CellType.PropertyCell)
            {
                PropertyCell Cell = (PropertyCell)cell;
                Console.WriteLine(ActionManager.Instance.CurrentPlayer.Name + "'s cell : " + Cell.StreetName +
                    ", " + Cell.Value + "$");
            }
            else
            {
                Console.WriteLine(ActionManager.Instance.CurrentPlayer.Name + " is on the " + Cell.ToString(cell.Type) + "\n");
            }
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
            Player p = ActionManager.Instance.CurrentPlayer;
            p.CurrentCell = Gameboard.Instance.Cells[(p.CurrentCell.Index + NumberOfSteps) % 40];
            if ((p.CurrentCell.Index + NumberOfSteps) / 40 > 0)
            {
                ActionManager.AddAction(new GetSalaryAction());
            }

            PrintCell(p.CurrentCell);
            ActionManager.AddAction(p.CurrentCell.CellAction);
        }

        public override bool IsLegalMove()
        {
            if (ActionManager.Instance.CurrentPlayer.IsInJail)
            {
                ActionManager.Instance.CurrentPlayer.TurnsInJail++;
                return false;
            }
            return true;
        }
    }

    public class GoToJailAction : MonopolyAction
    {
        public int NumberOfSteps { get; set; }
        public GoToJailAction() { }

        public override void Execute()
        {
            Console.WriteLine("You have gotten 3 doubles in a row, therefore you are caught by the police and " +
                "are sent to jail");
            Player p = ActionManager.Instance.CurrentPlayer;
            p.CurrentCell = Gameboard.JailCell;
            p.IsInJail = true;
        }
    }

    public class GetSalaryAction : MonopolyAction
    {
        public int NumberOfSteps { get; set; }
        public GetSalaryAction() { }

        public override void Execute()
        {
            ActionManager.Instance.CurrentPlayer.Cash += Player.SALARY;
            Console.WriteLine("Player " + ActionManager.Instance.CurrentPlayer.Name + " has now " + ActionManager.Instance.CurrentPlayer.Cash + "$");
        }
    }
    public class AttemptToBuyAction : MonopolyAction
    {
        public AttemptToBuyAction() { }

        public override void Execute()
        {
            Player p = ActionManager.Instance.CurrentPlayer;
            PropertyCell propertyCell = (PropertyCell)p.CurrentCell;
            Console.WriteLine("Player " + ActionManager.Instance.CurrentPlayer.Name + " has now " + ActionManager.Instance.CurrentPlayer.Cash + "$\n");
            if (p.Cash >= propertyCell.Value)
            {
                ActionManager.AddAction(new BuyPropertyAction());
            }
            else
            {
                Console.WriteLine("You don't have enough cash to attempt any transaction in this street.");
            }
        }

        public override bool IsLegalMove()
        {
            return ((ActionManager.Instance.CurrentPlayer.CurrentCell).Type == CellType.PropertyCell);
        }
    }

    public class BuyPropertyAction : MonopolyAction
    {
        public BuyPropertyAction() { }

        public override void Execute()
        {
            Player p = ActionManager.Instance.CurrentPlayer;
            PropertyCell propertyCell = (PropertyCell)p.CurrentCell;
            if (propertyCell.Landlord == null)
            {
                Console.WriteLine("\nProperty of : No one\nDo you want to buy this street?\nTap Enter to buy, anything else to proceed.");
                var input = Console.ReadKey();
                switch (input.Key)
                {
                    default:
                        break;
                    case ConsoleKey.Enter:
                        propertyCell.Landlord = ActionManager.Instance.CurrentPlayer;
                        propertyCell.Landlord.Cash -= propertyCell.Value;
                        Console.WriteLine("Done !\nYou now have : " + ActionManager.Instance.CurrentPlayer.Cash + "$\n");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Property of : " + propertyCell.Landlord.Name + "\n");
                if (p.Cash >= 2 * propertyCell.Value)
                {
                    Console.WriteLine("Do you want to buy this street for the price of " + propertyCell.Value * 2 + "?\nTap Enter to buy, anything else to cancel.");
                    ConsoleKey input = Console.ReadKey().Key;
                    switch (input)
                    {
                        default:
                            break;
                        case ConsoleKey.Enter:
                            propertyCell.Landlord = ActionManager.Instance.CurrentPlayer;
                            propertyCell.Landlord.Cash -= 2 * propertyCell.Value;
                            Console.WriteLine("Done !\nYou have now : " + ActionManager.Instance.CurrentPlayer.Cash + "\n");
                            break;
                    }
                }
            }
        }

        public override bool IsLegalMove()
        {
            return ((ActionManager.Instance.CurrentPlayer.CurrentCell).Type == CellType.PropertyCell);
        }

    }
    public class RollDiceAction : MonopolyAction
    {
        public RollDiceAction() { }

        public override void Execute()
        {
            Console.WriteLine(ActionManager.Instance.CurrentPlayer.Name + "'s turn to roll the dices, press any key to proceed");
            Console.ReadKey();
            int moveBy = Dices.Roll();
            if (ActionManager.Instance.CurrentPlayer.ConsecutiveDoubles != 0)
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
            ActionManager.Instance.CurrentPlayer.IsInJail = false;
        }
    }

    public class PayAction : MonopolyAction
    {
        public Player ToPay { get; set; }
        public Player ToDebit { get; set; }
        public double Value { get; set; }
        public PayAction(Player toPay, Player toDebit, double value)
        {
            ToPay = toPay;
            ToDebit = toDebit;
            Value = value;
        }

        public override void Execute()
        {
            if (ToDebit.Cash > Value)
            {
                ToPay.Cash += Value;
                ToDebit.Cash -= Value;
            } else
            {
                ToPay.Cash += ToDebit.Cash;
                ToDebit.Cash = 0;
                ToDebit.Bankrupt = true;
            }
            Console.WriteLine("New balances :");
            Console.WriteLine(ToPay.Name + " now has " + ToPay.Cash + "$");
            Console.WriteLine(ToDebit.Name + " now has " + ToDebit.Cash + "$");
        }
    }

    public class ResolvePropertyAction : MonopolyAction
    {
        public ResolvePropertyAction() { }
        public override void Execute()
        {
            Player p = ActionManager.Instance.CurrentPlayer;
            PropertyCell c = p.CurrentCell as PropertyCell;

            if (c.Landlord != null)
            {
                ActionManager.AddImmediateAction(new PayAction(c.Landlord, p, c.GetValue()));
            }
            ActionManager.AddAction(new AttemptToBuyAction());
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