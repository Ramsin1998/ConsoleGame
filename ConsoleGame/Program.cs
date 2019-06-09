using System;
using System.Threading.Tasks;
using ConsoleGame.Objects.GameEngine;
using ConsoleGame.Objects;
using ConsoleGame.Objects.GameObjects;
using System.Runtime.Caching;
using ConsoleGame.Extensions;
using System.Threading;
using System.Diagnostics;
using System.Media;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleGame
{
    class Program
    {
        static void intro(bool fullscreen = true)
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
            Task.Delay(100).Wait();

            //SoundPlayer sp = new SoundPlayer(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Pulsar.wav");
            //sp.PlayLooping();

            //Game game = new Game();

            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            //while (sw.Elapsed.Seconds < 5)
            //{
            //    Random rng = new Random();

            //    for (int y = 0; y < game.Rows / 2; y++)
            //    {
            //        for (int x = 0; x < game.Columns / 2; x++)
            //        {
            //            int random = rng.Next(1, Enum.GetNames(typeof(OccupationType)).Length);

            //            for (int Y = 0; Y < 1; Y++)
            //            {
            //                for (int X = 0; X < 1; X++)
            //                {
            //                    game[x * 2 + X, y * 2 + Y].OccupationType = (OccupationType)(random);
            //                }
            //            }
            //        }
            //    }

            //    game.Render();
            //}
        }

        static void engine()
        {
            intro();

            ////////////////////prerequisites////////////////////////////

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

            Game game = new Game();

            Task.Delay(100).Wait();

            game.Render(true);

            Quadrant quadrant = (Quadrant)rng.Next(0, 4);

            Goal goal = new Goal(30, 50, game, styleG);
            Block block = new Block(4000, 4000, game, new Style(new string('*', 15 * 15), 15, 15));
            Player player = new Player(1, 1, 50, game, styleP);
            Enemy enemy = new Enemy(50, 50, 500, game, styleE, player);

            game.AddBlocks(3);

            ////////////////////////////////////////////////////////////////////

            /////////////////////////GoardLoop/Engine///////////////////////////////////

            MemoryCache cache = MemoryCache.Default;
            DateTime now = new DateTime();
            DateTime previous = new DateTime();
            TimeSpan elapsed = new TimeSpan();
            int wait = 0;

            while (true)
            {
                previous = now;
                now = DateTime.Now;
                elapsed = now - previous;

                ///////////////LoopCode///////////////
                game.ProcessInputs();
                game.Update();

                object collision = cache.Get("collision");
                if (collision != null)
                    break;

                game.Render();

                ////////////////////////////////////////

                wait = 16 - elapsed.Milliseconds;

                if (!(wait < 0))
                    Task.Delay(wait).Wait();
            }

            //////////////////////////////////////////////////////////////////
        }

        [STAThread]
        static void Main(string[] args) 
        {
            engine();

            //GoardObject obj = new Goal(5, 6, new Goard(), new Style("*", 1, 1));
            //GoardObject obj10 = new Goal(5, 6, new Goard(), new Style("*", 1, 1));
            //GoardObject obj11 = new Goal(5, 6, new Goard(), new Style("*", 1, 1));

            //Console.WriteLine(obj.GetHashCode());
            //Console.WriteLine(obj10.GetHashCode());
            //Console.WriteLine(obj11.GetHashCode());

            //List<GoardObject> k = new List<GoardObject>();

            //k.Add(obj);

            //k[0].OccupationType = OccupationType.Block;

            //Console.WriteLine(k[0].GetHashCode());

            //Console.ReadLine();
        }
    }
}
