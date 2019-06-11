using ConsoleGame.Objects.GameEngine;
using System.Windows.Input;
using System.Diagnostics;
using System.Threading;
using System;
using System.Collections.Generic;

namespace ConsoleGame.Objects.GameObjects
{
    public class Player : GameObject
    {
        public Player(int speed, int quadrantRatio, Quadrant opposingQuadrant, Game game, Style style) : base(game, style, true)
        {
            Speed = speed;

            Quadrant = (Quadrant)(((int)opposingQuadrant + 2) % 4);

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
            OccupationType = OccupationType.Player;
            Avoidables = new List<OccupationType>() { OccupationType.Block };
        }

        public override void Move()
        {
            if (!(SW.ElapsedMilliseconds > Speed))
                return;

            PreviousCoordinates = Utilities.SystemObjectManipulation.DeepClone(Coordinates);
            Direction direction = Direction.None;

            if (Keyboard.IsKeyDown(Key.Up))
            {
                Coordinates.Row--;
                direction = Direction.Up;
            }

            else if (Keyboard.IsKeyDown(Key.Down))
            {
                Coordinates.Row++;
                direction = Direction.Down;
            }

            if (Keyboard.IsKeyDown(Key.Left))
            {
                Coordinates.Column--;
                direction = direction | Direction.Left;
            }

            else if (Keyboard.IsKeyDown(Key.Right))
            {
                Coordinates.Column++;
                direction = direction | Direction.Right;
            }

            if (Keyboard.IsKeyDown(Key.Space))
            {
                //Bullet bullet = new Bullet(this, 25, direction, Game, new Style("*", 1, 1));
                Bullet bullet1 = new Bullet(this, 12, Direction.Up, Game, new Style("*", 1, 1));
                Bullet bullet2 = new Bullet(this, 12, Direction.Down, Game, new Style("*", 1, 1));
                Bullet bullet3 = new Bullet(this, 12, Direction.Left, Game, new Style("*", 1, 1));
                Bullet bullet4 = new Bullet(this, 12, Direction.Right, Game, new Style("*", 1, 1));
                Bullet bullet5 = new Bullet(this, 12, Direction.Up | Direction.Left, Game, new Style("*", 1, 1));
                Bullet bullet6 = new Bullet(this, 12, Direction.Up | Direction.Right, Game, new Style("*", 1, 1));
                Bullet bullet7 = new Bullet(this, 12, Direction.Down | Direction.Left, Game, new Style("*", 1, 1));
                Bullet bullet8 = new Bullet(this, 12, Direction.Down | Direction.Right, Game, new Style("*", 1, 1));
            }

            base.Move();

            SW.Restart();
        }
    }
}
