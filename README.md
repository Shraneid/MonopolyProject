# Monopoly Project 
![](http://c.shld.net/rpx/i/s/i/spin/10000351/prod_12303494912??hei=64&wid=64&qlt=50)
The objective of the project isto simulatea simplified version of theMonopoly™ game

A set of players is given, each with name and initial position. Dices are rolled and players’positions on the game board will change.The game is played on a circular game board, composed of 40 positions on the board, indexed from 0 to 39. If a player reaches position 39 and still needs to move forward, he'll continue from position 0. In other words, positions 38, 39, 0, 1, 2 are contiguous

Each player rolls two dice and moves forward by a number of positions equal to
the sum of the numbers told by the two dice.
A player turn ends after moving.
The same position can be occupied by more than one player.
If a player gets both dice with the same value, then he rolls the dice and moves
again. If this happens three times in a row, the player goes to jail and ends his
turn.
Jail can be only visited or be a situation the player is in. The board has a Visit
Only / In Jail at position 10 and a Go To Jail at position 30.
If at the end of a basic move, the player lands on Go To Jail, then he immediately
moves to the position Visit Only / In Jail and is in jail. His turn ends
If after moving, the player lands on Visit Only / In Jail, he is visiting only and is
not in jail.
While the player is in jail, he still rolls the dice on his turn as usual, but does not
move until either:
(a) he gets a both dice with the same value, or
(b) he fails to roll both dice with the same value for three times in a row (i.e. his
previous two turns after moving to jail and his current turn).
If either (a) or (b) happens in the player's turn, then he moves forward by the
sum of the dice rolled positions and his turn ends. He does not roll the dice again
even if he has rolled a both dice with the same value.
# Our implementation 
We implemented several design pattern : 
FACTORY PATTERN  :
* Players
```cs
        public static Player GetNewPlayer(String name)
        {
            Player p = new Player(name);
            return p;
        }
```
* Cells for Gameboard where we have to distinguish every cell types (Jail, Start, Free parking, and other cells that contains buildings, cost to buy etc...)
* We actually created a text file containing every of the 40 Cells with every street name and cost of it.
```cs
        internal static Cell GetCell(int index, CellType cellType)
        {
            Cell cell = null;
            if (cellType == CellType.StartCell)
            {
                cell = new StartCell();
            }
            if (cellType == CellType.PropertyCell)
            {
                cell = new PropertyCell();
            }
            if (cellType == CellType.JailCell)
            {
                cell = new JailCell();
            }
            if (cellType == CellType.GoToJailCell)
            {
                cell = new GoToJailCell();
            }
            if (cellType == CellType.FreeParking)
            {
                cell = new FreeParking();
            }
            cell.Index = index;
            return cell;
        }
```
SINGLETON PATTERN:
* For the gameboard 
```cs
        public static void Init()
        {
            instance = new Gameboard{Cells = new Cell[40]};

            InstantiatePropertyCells(instance.Cells);

            StartCell = instance.Cells[0];
            JailCell = instance.Cells[10];
        }
```
MVC PATTERN : 
* Model for our players/cells/gameboard/dice
* View with the command console
* Controller with our Turn Manager, Action Manager, Monopoly Action classes

* Dice class :
```cs
    public static class Dices
    {
        public static int Roll()
        {
            Random rand = new Random();
            int dice1 = rand.Next(1, 7);
            int dice2 = rand.Next(1, 7);
            //for testing doubles
            //dice2 = dice1;

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
```
To play this game, we had to make a loop that would stop whenever a player wins, which means that there's only one player left. 
We also had to take into account the player's turns and actions.
That's why we needed a strategy pattern that would include two new classes : TurnManager and ActionManager.
```cs
    public class TurnManager
    {
        public static TurnManager Instance { get; set; }
        public int Turn { get; set; }
        public int PlayerIndex { get; set; }
        public Player[] Players { get; set; }

        public static void Init()
        {
            Instance = new TurnManager();
            Instance.Turn = 0;
            Instance.PlayerIndex = -1;
        }
```
```cs
    public class ActionManager
{
        public static ActionManager instance;
        public Player CurrentPlayer { get; set; }
        public List<MonopolyAction> Actions { get; set; }

        public static void Init()
        {
            instance = new ActionManager();
            instance.Actions = new List<MonopolyAction>();
        }
}
```
In addition, A monopoly Action Class will analyze every of the action assigned to a player's turn and check if it's a legal move or not.
```cs
    public abstract class MonopolyAction
    {
        public abstract void Execute();
        public virtual bool IsLegalMove()
        {
            return true;
        }
```
And for every instanciated actions, we create a inherited class from monopoly action that would have two methods : Islegalmove() and Execute(). For example : 
```cs
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
```

### Requirements 

* Microsoft Visual Studio 2019
* Markdown
* Git

### Installation
Repository : https://github.com/Shraneid/MonopolyProject.git


