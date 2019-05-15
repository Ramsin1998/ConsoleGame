using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Objects.GameBoard;
using System.Windows.Input;

namespace ConsoleGame.Objects
{
    class Player : BoardObject
    {


        public Player(int column, int row, Board board, Style style) : base(board, style)
        {
            OccupationType = OccupationType.Player;
            Column = column * Style.Width;
            Row = row * Style.Height;
        }

        [STAThread]
        public override void Move()
        {
            if (Keyboard.IsKeyDown(Key.Up))
                Row -= 1;

            else if (Keyboard.IsKeyDown(Key.Down))
                Row += 1;

            else if (Keyboard.IsKeyDown(Key.Left))
                Column -= 1;

            else if (Keyboard.IsKeyDown(Key.Right))
                Column += 1;

            //while (Console.KeyAvailable)
            //    switch (Console.ReadKey(true).Key)
            //    {
            //        case ConsoleKey.UpArrow:
            //            Row -= Style.Height;
            //            break;

            //        case ConsoleKey.DownArrow:
            //            Row += Style.Height;
            //            break;

            //        case ConsoleKey.LeftArrow:
            //            Column -= Style.Width;
            //            break;

            //        case ConsoleKey.RightArrow:
            //            Column += Style.Width;
            //            break;
            //    }
        }
    }
}
