using ConsoleGame.Objects.GameBoard;
using System.Windows.Input;
using System.Diagnostics;
using System.Threading;
using System;

namespace ConsoleGame.Objects.BoardObjects
{
    public class Player : BoardObject
    {
        public Player(int speed, int quadrantRatio, Quadrant opposingQuadrant, Board board, Style style) : base(board, style)
        {
            SW = new Stopwatch();
            SW.Start();
            Speed = speed;
            OccupationType = OccupationType.Player;

            switch (opposingQuadrant)
            {
                case Quadrant.UpperLeft:
                    Quadrant = Quadrant.LowerRight;
                    break;

                case Quadrant.UpperRight:
                    Quadrant = Quadrant.LowerLeft;
                    break;

                case Quadrant.LowerRight:
                    Quadrant = Quadrant.UpperLeft;
                    break;

                case Quadrant.LowerLeft:
                    Quadrant = Quadrant.UpperRight;
                    break;
            }

            RandomizeQuadrantPosition(quadrantRatio);
            Ghost = new Ghost(this, 5);
        }

        public Player(int column, int row, int speed, Board board, Style style) : base(column, row, board, style)
        {
            Coordinates = new Coordinates(column, row);
            SW = new Stopwatch();
            SW.Start();
            Speed = speed;
            OccupationType = OccupationType.Player;
            Ghost = new Ghost(this, 5);
        }

        public override void Move()
        {
            int column = coordinates.Column;
            int row = coordinates.Row;

            if (Keyboard.IsKeyDown(Key.Up) && row != 0)
            {
                row -= 1;

                if (!Board.Project(this, new Coordinates(column, row)))
                    row += 1;
            }

            if (Keyboard.IsKeyDown(Key.Down) && row != Board.Rows - Style.Height - 1)
            {
                row += 1;

                if (!Board.Project(this, new Coordinates(column, row)))
                    row -= 1;
            }

            if (Keyboard.IsKeyDown(Key.Left) && column != 0)
            {
                column -= 1;

                if (!Board.Project(this, new Coordinates(column, row)))
                    column += 1;
            }

            if (Keyboard.IsKeyDown(Key.Right) && column != Board.Columns - Style.Width - 1)
            {
                column += 1;

                if (!Board.Project(this, new Coordinates(column, row)))
                    column -= 1;
            }

            Coordinates = new Coordinates(column, row);

            Board.Objects.Add(this);
        }
    }
}
