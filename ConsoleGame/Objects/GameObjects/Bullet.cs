using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Objects.GameEngine;
using System.Diagnostics;

namespace ConsoleGame.Objects.GameObjects
{
    public class Bullet : GameObject
    {
        public Direction Direction { get; set; }
        public GameObject Obj { get; set; }

        public Bullet(GameObject obj, int speed, Direction direction, Game game, Style style) : base(game, style, true)
        {
            Obj = obj;
            SW = new Stopwatch();
            Speed = speed;
            OccupationType = OccupationType.Bullet;
            Direction = direction;

            calculateAndSetCoordinates();

            SW.Start();
        }

        private void calculateAndSetCoordinates()
        {
            int column = Obj.Coordinates.Column + (Obj.Style.Width - Style.Width) / 2;
            int row = Obj.Coordinates.Row + (Obj.Style.Height - Style.Height) / 2;

            if ((Direction & Direction.Up) == Direction.Up)
                row -= row + Style.Height - Obj.Coordinates.Row;

            else if ((Direction & Direction.Down) == Direction.Down)
                row += Obj.Coordinates.Row + Obj.Style.Height - row;

            if ((Direction & Direction.Left) == Direction.Left)
                column -= column + Style.Width - Obj.Coordinates.Column;

            else if ((Direction & Direction.Right) == Direction.Right)
                column += Obj.Coordinates.Column + Obj.Style.Width - column;

            Coordinates = new Coordinates(column, row);
        }

        public override void Move()
        {
            if (!(SW.ElapsedMilliseconds > Speed))
                return;

            int column = Coordinates.Column;
            int row = Coordinates.Row;

            if ((Direction & Direction.Up) == Direction.Up)
                row--;

            else if ((Direction & Direction.Down) == Direction.Down)
                row++;

            if ((Direction & Direction.Left) == Direction.Left)
                column--;

            else if ((Direction & Direction.Right) == Direction.Right)
                column++;

            Coordinates = new Coordinates(column, row);

            base.Move();

            SW.Restart();
        }
    }
}
