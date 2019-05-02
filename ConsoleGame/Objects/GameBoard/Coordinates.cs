using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Objects.GameBoard
{
    class Coordinates
    {
        public int Row { get; set; }
        public int Column { get ; set; }

        public Coordinates(int column, int row)
        {
            Row = row;
            Column = column;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                return false;

            else
            {
                Coordinates c = (Coordinates)obj;
                return (Column == c.Column) && (Row == c.Row);
            }
        }

        public override int GetHashCode() => new { Row, Column }.GetHashCode();
    }
}
