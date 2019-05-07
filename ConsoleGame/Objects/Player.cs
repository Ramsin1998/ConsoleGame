using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Objects.GameBoard;

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

        public override void Move()
        {
            while (Console.KeyAvailable)
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        Row -= Style.Height;
                        break;

                    case ConsoleKey.DownArrow:
                        Row += Style.Height;
                        break;

                    case ConsoleKey.LeftArrow:
                        Column -= Style.Width;
                        break;

                    case ConsoleKey.RightArrow:
                        Column += Style.Width;
                        break;
                }
        }
    }
}
