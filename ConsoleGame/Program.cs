using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Objects.GameBoard;
using ConsoleGame.Extensions;

namespace ConsoleGame
{
    class Program
    {
        static void intro()
        {
            Console.WriteLine("psst! ...press Alt+Enter!");

            while (true)
                if (Console.WindowHeight == Console.LargestWindowHeight)
                    break;

            Console.Clear();
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;
        }

        static void Main(string[] args)
        {
            intro();

            //while (true)
            //{
            //    if (Console.KeyAvailable)
            //        switch (Console.ReadKey(true).Key)
            //        {
            //            case ConsoleKey.UpArrow:
            //                Console.WriteLine("UP");
            //                break;

            //            case ConsoleKey.DownArrow:
            //                Console.WriteLine("DOWN");
            //                break;

            //            case ConsoleKey.LeftArrow:
            //                Console.WriteLine("LEFT");
            //                break;

            //            case ConsoleKey.RightArrow:
            //                Console.WriteLine("RIGHT");
            //                break;
            //        }
            //}

            Board board = new Board(100, 50);

            board[2, 3].OccupationType = OccupationType.Player;
            board[3, 3].OccupationType = OccupationType.Player;
            board[4, 3].OccupationType = OccupationType.Player;
            board[5, 3].OccupationType = OccupationType.Player;
            board[6, 3].OccupationType = OccupationType.Player;

            board[4, 6].OccupationType = OccupationType.Enemy;
            board[4, 7].OccupationType = OccupationType.Enemy;
            board[4, 8].OccupationType = OccupationType.Enemy;
            board[99, 49].OccupationType = OccupationType.Enemy;

            Console.ReadLine();

            board.Print(5, 5);

            Console.ReadLine();
        }
    }
}
