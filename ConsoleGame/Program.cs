using System;
using System.Threading.Tasks;
using ConsoleGame.Objects.GameBoard;
using ConsoleGame.Objects;
using ConsoleGame.Objects.BoardObjects;
using System.Runtime.Caching;
using ConsoleGame.Extensions;
using System.Threading;

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

            //Board board = new Board();

            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            //while (sw.Elapsed.Seconds < 4)
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

            //    board.Render();
            //}
        }

        static void game()
        {
            intro(true);

            Style styleP = new Style("  *  " +
                                     "     " +
                                     "*   *" +
                                     "     " +
                                     "  *  ", 5, 5);

            Style styleE = new Style("  **  " +
                                     " **** " +
                                     "**  **" +
                                     " **** " +
                                     "  **  ", 6, 5);

            Style styleG = new Style("  **  " +
                                     " **** " +
                                     "******" +
                                     "******" +
                                     " **** " +
                                     "  **  ", 6, 6);

            Style styleB = new Style("*", 1, 1);

            Random rng = new Random();

            Board board = new Board();

            board.Render(true);

            Quadrant quadrant = (Quadrant)rng.Next(0, 4);

            Goal goal = new Goal(10, quadrant, board, styleG);
            Player player = new Player(50, 10, goal.Quadrant, board, styleP);
            Enemy enemy = new Enemy(50, 50, 500, board, styleE, player);

            board.AddBlocks(styleB, 500);

            board.Objects.Add(goal);
            board.Objects.Add(player);
            board.Objects.Add(enemy);

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

                object collision = cache.Get("collision");

                if (collision != null)
                    break;

                wait = 16 - elapsed.Milliseconds;

                if (!(wait < 0))
                    Task.Delay(wait).Wait();

                board.Render();
            }
        }

        static void kk()
        {
            char[] text = "hello world!".ToCharArray();

            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text[i]);
                Task.Delay(1000).Wait();
            }
        }

        static void Main(string[] args)
        {
            //game();

            ThreadStart start = new ThreadStart(kk);

            Thread thread = new Thread(start);

            thread.Start();

            Console.WriteLine();

            kk();

            Console.ReadLine();
        }
    }
}
