using System;

namespace Puissance4
{

public static class Program {
    
    public static void Main() {
    
        bool stop = false;
        int selector = 0;
        var mode = Game.GameMode.PvP;

        while (!stop)
        {
            Console.Clear();
            Console.WriteLine("Choose a Mode :");
            Console.WriteLine(selector == 0 ? "> 1. Player vs Player" : "  1. Player vs Player");
            Console.WriteLine(selector == 1 ? "> 2. AI vs Player" : "  2. AI vs Player");
            Console.WriteLine(selector == 2 ? "> 3. AI vs AI" : "  3. AI vs AI");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.DownArrow :
                    selector = (selector + 1) % 3;
                    break;
                case ConsoleKey.UpArrow :
                    selector -= 1;
                    if (selector < 0) selector = 2;
                    break;
                case ConsoleKey.Enter :
                    switch (selector)
                    {
                        case 0 : 
                            mode = Game.GameMode.PvP;
                            break;
                        case 1 : 
                            mode = Game.GameMode.PvAI;
                            break;
                        case 2 : 
                            mode = Game.GameMode.AIvAI;
                            break;
                    }
                    stop = true;
                    break;
            }
        }

    Game game = new Game(mode);
    game.Play();
    }
}
}
