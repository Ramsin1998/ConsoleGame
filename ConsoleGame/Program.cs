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
using System.IO;
using ConsoleGame.Objects.BoardObjects;
using System.Runtime.Caching;

namespace ConsoleGame
{
    class Program
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

        [STAThread]
        static void Main(string[] args)
        {
            //object co = Collision.PlayerEnemy;

            //Collision k;

            //k = (Collision)co;

            //Console.WriteLine();

            //Console.ReadLine();

            intro(true);

            Board board = new Board();

            board.Render(true);

            Style styleP = new Style("*****" +
                                     "*   *" +
                                     "* * *" +
                                     "*   *" +
                                     "*****", 5, 5);

            Style styleE = new Style("*****" +
                                     "*   *" +
                                     "* * *" +
                                     "*   *" +
                                     "*****", 5, 5);

            Style styleG = new Style("****" +
                                     "****" +
                                     "****" +
                                     "****", 4, 4);

            Random rng = new Random();

            Quadrant quadrant = (Quadrant)rng.Next(0, 4);

            Goal goal = new Goal(4, quadrant, board, styleG);
            Player player = new Player(50, 4, goal, board, styleP);
            Enemy enemy = new Enemy(50, 50, 500, board, styleE, player);
            Block block = new Block(40, 40, board, styleG);

            board.Objects.Add(goal);
            board.Objects.Add(player);
            board.Objects.Add(enemy);
            board.Objects.Add(block);

            DateTime now = new DateTime();
            DateTime previous = new DateTime();
            TimeSpan elapsed = new TimeSpan();
            int wait = 0;

            MemoryCache cache = MemoryCache.Default;

            while (true)
            {
                previous = now;
                now = DateTime.Now;
                elapsed = now - previous;

                player.Move();
                enemy.Move();

                board.UpdateObjects();

                //object collision = cache.Get("collision");

                //if (collision != null)
                //    break;

                wait = 16 - elapsed.Milliseconds;

                if (!(wait < 0))
                    Task.Delay(wait); 

                board.Render();
            }


            Console.ResetColor();

            Console.Clear();

            Console.WriteLine("NOOOOOOOOOOOOOOOOOOOOOO");

            Console.ReadLine();
        }
    }
}
