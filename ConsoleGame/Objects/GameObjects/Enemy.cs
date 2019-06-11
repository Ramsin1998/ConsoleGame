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
            OccupationType = OccupationType.Enemy;
        }

        public override void Move()
        {
            if (!(SW.ElapsedMilliseconds > Speed))
                return;

            PreviousCoordinates = Utilities.SystemObjectManipulation.DeepClone(Coordinates);

            DeltaX = Player.Coordinates.Column - Coordinates.Column;
            DeltaY = Player.Coordinates.Row - Coordinates.Row;
            int absDeltaX = Math.Abs(DeltaX);
            int absDeltaY = Math.Abs(DeltaY);

            void moveX()
            {
                if (DeltaX > 0)
                    Coordinates.Column++;

                else
                    Coordinates.Column--;
            }

            void moveY()
            {
                if (DeltaY > 0)
                    Coordinates.Row++;

                else
                    Coordinates.Row--;
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

            base.Move();

            SW.Restart();
        }
    }
}