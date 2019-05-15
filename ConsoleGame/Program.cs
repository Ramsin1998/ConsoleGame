using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Objects.GameBoard;
using ConsoleGame.Objects;
using ConsoleGame.Extensions;
using System.Timers;
using System.Diagnostics;
using System.Windows.Input;

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

        static bool isEven(int num)
        {
            return num % 2 == 0 ? true : false;
        }

        [STAThread]
        static void Main(string[] args)
        {
            intro();

            Board board = new Board();

            board.Render(true);

            Style styleP = new Style("***" +
                                     "* *" +
                                     "***", 3, 3);

            Style styleE = new Style("*", 1, 1);

            Player player = new Player(0, 0, board, styleP);
            Enemy enemy = new Enemy(board, styleE, player);

            while (true)
            {

                player.Move();
                enemy.Move();

                board.UpdateObjects();

                System.Threading.Thread.Sleep(1); Console.WriteLine();

                board.Render();

                System.Threading.Thread.Sleep(40);
            }

            //while (true)
            //{
            //    for (int j = 0; j < 116; j++)
            //        for (int k = 0; k < j; k++)
            //        {
            //            for (int i = 0; i < 64; i++)
            //                board[j, i].OccupationType = OccupationType.Enemy;

            //            board.Print();
            //        }
            //}

            //while (true)
            //{
            //    Random rng = new Random();

            //    for (int y = 0; y < board.Rows / 2; y++)
            //    {
            //        for (int x = 0; x < board.Columns / 2; x++)
            //        {
            //            int k = rng.Next(1, 5);

            //            for (int Y = 0; Y < 1; Y++)
            //            {
            //                for (int X = 0; X < 1; X++)
            //                {
            //                    board[x * 2 + X, y * 2 + Y].OccupationType = (OccupationType)(k);
            //                }
            //            }
            //        }
            //    }

            //    board.Print();
            //}

            //00 10 20 30 40 50
            //01 11 21 31 41 51
            //02 12 22 32 42 52
            //03 13 23 33 42 53
        }
    }
}
