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
        public GameObject ObjFiredFrom { get; set; }

        public Bullet(GameObject objFiredFrom, int speed, Direction direction, Game game, Style style) : base(game, style, true)
        {
            ObjFiredFrom = objFiredFrom;
            Speed = speed;
            OccupationType = OccupationType.Bullet;
            Direction = direction;
            Avoidables = new List<OccupationType>() { OccupationType.Block };

            calculateAndSetCoordinates();
        }

        private void calculateAndSetCoordinates()
        {
            int column = ObjFiredFrom.Coordinates.Column + (ObjFiredFrom.Style.Width - Style.Width) / 2;
            int row = ObjFiredFrom.Coordinates.Row + (ObjFiredFrom.Style.Height - Style.Height) / 2;

            if ((Direction & Direction.Up) == Direction.Up)
                row -= row + Style.Height - ObjFiredFrom.Coordinates.Row;

            else if ((Direction & Direction.Down) == Direction.Down)
                row += ObjFiredFrom.Coordinates.Row + ObjFiredFrom.Style.Height - row;

            if ((Direction & Direction.Left) == Direction.Left)
                column -= column + Style.Width - ObjFiredFrom.Coordinates.Column;

            else if ((Direction & Direction.Right) == Direction.Right)
                column += ObjFiredFrom.Coordinates.Column + ObjFiredFrom.Style.Width - column;

            Coordinates = new Coordinates(column, row, Game.Columns - Style.Width, Game.Rows - Style.Height);
        }

        public override void Move()
        {
            if (!(SW.ElapsedMilliseconds > Speed))
                return;

            PreviousCoordinates = Utilities.SystemObjectManipulation.DeepClone(Coordinates);

            if ((Direction & Direction.Up) == Direction.Up)
                Coordinates.Row--;

            else if ((Direction & Direction.Down) == Direction.Down)
                Coordinates.Row++;

            if ((Direction & Direction.Left) == Direction.Left)
                Coordinates.Column--;

            else if ((Direction & Direction.Right) == Direction.Right)
                Coordinates.Column++;

            base.Move();

            SW.Restart();
        }
    }
}
