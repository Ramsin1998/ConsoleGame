using System;
using ConsoleGame.Objects.GameEngine;
using System.Diagnostics;

namespace ConsoleGame.Objects.GameObjects
{
    public class Enemy : GameObject
    {
        public Player Player { get; set; }
        public int DeltaX { get; set; }
        public int DeltaY { get; set; }

        public Enemy(int speed, Game game, Style style, Player player) : base(game, style, true)
        {
            Player = player;
            Speed = speed;

            initialize();
        }

        public Enemy(int column, int row, int speed, Game game, Style style, Player player) : base(column, row, game, style, true)
        {
            Player = player;
            Speed = speed;

            initialize();
        }

        private void initialize()
        {
            SW = new Stopwatch();
            OccupationType = OccupationType.Enemy;

            SW.Start();
        }

        public override void Move()
        {
            if (!(SW.ElapsedMilliseconds > Speed))
                return;

            DeltaX = Player.Coordinates.Column - Coordinates.Column;
            DeltaY = Player.Coordinates.Row - Coordinates.Row;
            int absDeltaX = Math.Abs(DeltaX);
            int absDeltaY = Math.Abs(DeltaY);

            int column = Coordinates.Column;
            int row = Coordinates.Row;

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

            SW.Restart();
        }
    }
}