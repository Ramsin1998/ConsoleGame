using ConsoleGame.Objects.GameEngine;
using System.Windows.Input;
using System.Diagnostics;
using System.Threading;
using System;

namespace ConsoleGame.Objects.GameObjects
{
    public class Player : GameObject
    {
        public Player(int speed, int quadrantRatio, Quadrant opposingQuadrant, Game game, Style style) : base(game, style, true)
        {
            Speed = speed;

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

            initialize();
        }

        public Player(int column, int row, int speed, Game game, Style style) : base(column, row, game, style, true)
        {
            Speed = speed;

            initialize();
        }

        private void initialize()
        {
            SW = new Stopwatch();
            OccupationType = OccupationType.Player;
            SW.Start();
        }

        public override void Move()
        {
            if (!(SW.ElapsedMilliseconds > Speed))
                return;

            int column = Coordinates.Column;
            int row = Coordinates.Row;
            Direction direction = Direction.None;

            if (Keyboard.IsKeyDown(Key.Up))
            {
                row -= 1;
                direction = Direction.Up;
            }

            else if (Keyboard.IsKeyDown(Key.Down))
            {
                row += 1;
                direction = Direction.Down;
            }

            if (Keyboard.IsKeyDown(Key.Left))
            {
                column -= 1;
                direction = direction | Direction.Left;
            }

            else if (Keyboard.IsKeyDown(Key.Right))
            {
                column += 1;
                direction = direction | Direction.Right;
            }

            Coordinates = new Coordinates(column, row);

            if (Keyboard.IsKeyDown(Key.Space) && direction != Direction.None)
            {
                Bullet bullet = new Bullet(this, 25, direction, Game, new Style("*", 1, 1));
            }

            base.Move();

            SW.Restart();
        }
    }
}
