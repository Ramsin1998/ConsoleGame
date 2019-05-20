using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Objects.GameBoard
{
    public struct Coordinates
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
            if ((obj == null) || !GetType().Equals(obj.GetType()))
                return false;

            else
            {
                Coordinates c = (Coordinates)obj;
                return GetHashCode() == obj.GetHashCode();
            }
        }

        public static bool operator ==(Coordinates coordinates1, Coordinates coordinates2)
        {
            return coordinates1.Equals(coordinates2);
        }

        public static bool operator !=(Coordinates coordinates1, Coordinates coordinates2)
        {
            return !coordinates1.Equals(coordinates2);
        }

        public override int GetHashCode() => new { Row, Column }.GetHashCode();
    }
}
