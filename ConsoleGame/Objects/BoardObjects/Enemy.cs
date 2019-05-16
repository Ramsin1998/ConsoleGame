using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Objects.GameBoard;
using System.Diagnostics;

namespace ConsoleGame.Objects.BoardObjects
{
    class Enemy : BoardObject
    {
        public Stopwatch SW { get; set; }
        public Player Player { get; set; }
        public int DeltaX { get; set; }
        public int DeltaY { get; set; }
        public int Speed { get; set; }

        public Enemy(int column, int row, int speed, Board board, Style style, Player player) : base(board, style)
        {
            Coordinates = new Coordinates(column, row);
            SW = new Stopwatch();
            SW.Start();
            Player = player;
            Speed = speed;
            OccupationType = OccupationType.Enemy;
        }

        public override void Move()
        {
            if (!(SW.ElapsedMilliseconds > Speed))
                return;

            SW.Restart();

            DeltaX = Player.Coordinates.Column - coordinates.Column;
            DeltaY = Player.Coordinates.Row - coordinates.Row;
            int absDeltaX = Math.Abs(DeltaX);
            int absDeltaY = Math.Abs(DeltaY);

            int column = coordinates.Column;
            int row = coordinates.Row;

            void moveX()
            {
                if (DeltaX > 0)
                    column += 1;

                else
                    column -= 1;
            }

            void moveY()
            {
                if (DeltaY > 0)
                    row += 1;

                else
                    row -= 1;
            }

            if (absDeltaX == absDeltaY)
            {
                moveX();
                moveY();
            }

            else if (absDeltaX > absDeltaY)
                moveX();

            else
                moveY();

            Coordinates = new Coordinates(column, row);

            base.Move();
        }
    }
}
