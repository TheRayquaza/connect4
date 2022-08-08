using System;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Puissance4
{

public class Game
{

    public enum GameMode
    {
        PvP,
        PvAI,
        AIvAI
    }
    
    public enum GameState
    {
        RED,
        YELLOW,
        TIE,
        WAITING
    }

    public GameState State;
    public GameMode Mode;
    public Board board;
    
    public Game(GameMode mode)
    {
        board = new Board();
        Mode = mode;
        State = GameState.WAITING;
    }

    #region Play
    public void Play()
    {
        switch (Mode)
        {
            case GameMode.PvP :
                PlayPvP();
                break;
            case GameMode.PvAI :
                PlayPvAI();
                break;
            case GameMode.AIvAI:
                PlayAIvAI();
                break;
        }
    }

    private void PlayPvP()
    {
        bool turn = true;
        while (State == GameState.WAITING)
        {
            Console.ForegroundColor = turn ? ConsoleColor.Red : ConsoleColor.Yellow;
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine("                     ▼   ");

            if (turn)
            {
                // Print
                board.Print();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Red Player, It's your turn !");
                Console.ForegroundColor = ConsoleColor.Gray;
                // Selection of the column
                int column = -1;
                do
                {
                    column = Selection(turn);
                } while (!board.PlacePiece(column, Cell.Type.Red));
                // Checker
                if (board.CheckMove(column, Cell.Type.Red)) State = GameState.RED;
                if (board.IsFull()) State = GameState.TIE;
            }
            else
            {
                // Print
                board.Print();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Yellow Player, It's your turn !");
                Console.ForegroundColor = ConsoleColor.Gray;
                // Selection of the column
                int column = -1;
                do
                {
                    column = Selection(turn);
                } while (!board.PlacePiece(column, Cell.Type.Yellow));
                // Checker
                if (board.CheckMove(column, Cell.Type.Yellow)) State = GameState.YELLOW;
                if (board.IsFull()) State = GameState.TIE;
            }

            Console.Clear();
            turn = !turn;
        }
        if (State == GameState.TIE) DisplayTie();
        else DisplayWin(turn);
    }

    private void PlayPvAI()
    {
        bool turn = true;
        while (State == GameState.WAITING)
        {
            if (turn)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("It's your turn Red Player !");
                Console.ForegroundColor = ConsoleColor.Gray;
                int column = -1;
                while (!board.PlacePiece(column, Cell.Type.Red))
                {
                    var raw = Console.ReadLine();
                    try
                    {
                        column = Int32.Parse(raw);
                    }
                    catch (Exception)
                    {
                        column = -1;
                    }
                }
            }
            else
            {
                
            }
        }
    }

    private void PlayAIvAI()
    {
        
    }
    
    #endregion

    public int Selection(bool color)
    {
        int choice = 3;
        while (true)
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.RightArrow:
                    choice = (choice + 1) % 7;
                    break;
                case ConsoleKey.LeftArrow:
                    choice = (choice - 1 + 7) % 7;
                    break;
                case ConsoleKey.Enter:
                    return choice;
                default:
                    break;
            }

            Console.Clear();
            
            Console.ForegroundColor = color ? ConsoleColor.Red : ConsoleColor.Yellow;
            
            for (int i = 0; i < choice; i++)
                Console.Write("      ");
            
            Console.OutputEncoding = Encoding.Unicode;
            Console.WriteLine("   ▼   ");
            
            board.Print();
            
            Console.ForegroundColor = color ? ConsoleColor.Red : ConsoleColor.Yellow;
            Console.WriteLine(color ? "Red Player, It's your turn !" : "Yellow Player, It's your turn !"); 
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        return choice;
    }
    
    #region End

    private void DisplayTie()
    {
        Console.Clear();
        board.Print();
        Console.WriteLine("The game is a tie !");
        Thread.Sleep(2000);
    }
    private void DisplayWin(bool player)
    {
        Console.Clear();
        Console.ForegroundColor = !player ? ConsoleColor.Red : ConsoleColor.Yellow;
        Console.WriteLine(player ? $@"
██╗   ██╗███████╗██╗     ██╗      ██████╗ ██╗    ██╗    ██╗    ██╗ ██████╗ ███╗   ██╗
╚██╗ ██╔╝██╔════╝██║     ██║     ██╔═══██╗██║    ██║    ██║    ██║██╔═══██╗████╗  ██║
 ╚████╔╝ █████╗  ██║     ██║     ██║   ██║██║ █╗ ██║    ██║ █╗ ██║██║   ██║██╔██╗ ██║
  ╚██╔╝  ██╔══╝  ██║     ██║     ██║   ██║██║███╗██║    ██║███╗██║██║   ██║██║╚██╗██║
   ██║   ███████╗███████╗███████╗╚██████╔╝╚███╔███╔╝    ╚███╔███╔╝╚██████╔╝██║ ╚████║
   ╚═╝   ╚══════╝╚══════╝╚══════╝ ╚═════╝  ╚══╝╚══╝      ╚══╝╚══╝  ╚═════╝ ╚═╝  ╚═══╝
" : $@"
██████╗ ███████╗██████╗     ██╗    ██╗ ██████╗ ███╗   ██╗
██╔══██╗██╔════╝██╔══██╗    ██║    ██║██╔═══██╗████╗  ██║
██████╔╝█████╗  ██║  ██║    ██║ █╗ ██║██║   ██║██╔██╗ ██║
██╔══██╗██╔══╝  ██║  ██║    ██║███╗██║██║   ██║██║╚██╗██║
██║  ██║███████╗██████╔╝    ╚███╔███╔╝╚██████╔╝██║ ╚████║
╚═╝  ╚═╝╚══════╝╚═════╝      ╚══╝╚══╝  ╚═════╝ ╚═╝  ╚═══╝
");
        board.Print();
        Thread.Sleep(2000);
    }
    
    #endregion
}
}
