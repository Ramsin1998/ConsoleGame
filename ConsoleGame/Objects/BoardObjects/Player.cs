using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Objects.GameBoard;
using System.Windows.Input;
using System.Diagnostics;

namespace ConsoleGame.Objects.BoardObjects
{
    class Player : BoardObject
    {
        public Stopwatch SW { get; set; }
        public int Speed { get; set; }

        public Player(int column, int row, int speed, Board board, Style style) : base(board, style)
        {
            Coordinates = new Coordinates(column, row);
            SW = new Stopwatch();
            SW.Start();
            Speed = speed;
            OccupationType = OccupationType.Player;
        }

        public Player(int speed, Board board, Style style) : base(board, style)
        {
            Coordinates = new Coordinates(column, row);
            SW = new Stopwatch();
            SW.Start();
            Speed = speed;
            OccupationType = OccupationType.Player;
        }

        public override void Move()
        {
            if (!(SW.ElapsedMilliseconds > Speed))
                return;

            SW.Restart();

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
