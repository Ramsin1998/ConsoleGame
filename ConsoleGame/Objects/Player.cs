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
        public Player(int column, int row, Board board, Style style) : base(column, row, board, style)
        {
            OccupationType = OccupationType.Player;
        }

        public override void Move()
        {
            int column = coordinates.Column;
            int row = coordinates.Row;

            if (Keyboard.IsKeyDown(Key.Up) && row != 0)
                row -= 1;

            if (Keyboard.IsKeyDown(Key.Down) && row != Board.Rows - Style.Height - 1)
                row += 1;

            if (Keyboard.IsKeyDown(Key.Left) && column != 0)
                column -= 1;

            if (Keyboard.IsKeyDown(Key.Right) && column != Board.Columns - Style.Width - 1)
                column += 1;

            Coordinates = new Coordinates(column, row);

            base.Move();
        }
    }
}
