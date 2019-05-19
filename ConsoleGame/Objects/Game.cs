using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Objects.GameBoard;
using System.Diagnostics;
using ConsoleGame.Utilities;

namespace ConsoleGame.Objects
{
    class Game
    {
        static void intro(bool fullscreen = false)
        {
            if (fullscreen)
            {
                Console.WriteLine("psst! ...press Alt+Enter!");

                while (true)
                    if (Console.WindowHeight == Console.LargestWindowHeight)
                        break;
            }

            else
            {
                Console.WriteLine("Adjust the window size to your liking and then press any key :)");
                Console.ReadKey(true);
            }

            Console.Clear();
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;

            Board board = new Board();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (sw.Elapsed.Seconds < 4)
            {
                Random rng = new Random();

                for (int y = 0; y < board.Rows / 2; y++)
                {
                    for (int x = 0; x < board.Columns / 2; x++)
                    {
                        int k = rng.Next(1, 5);

                        for (int Y = 0; Y < 1; Y++)
                        {
                            for (int X = 0; X < 1; X++)
                            {
                                board[x * 2 + X, y * 2 + Y].OccupationType = (OccupationType)(k);
                            }
                        }
                    }
                }

                board.Render();
            }
        }

        public Game()
        {

        }
    }
}
