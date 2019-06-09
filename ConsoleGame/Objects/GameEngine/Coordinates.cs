using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Objects.GameEngine
{
    public class Coordinates
    {
        public int Row { get; }
        public int Column { get; }

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

        public override int GetHashCode() => (Row, Column).GetHashCode();
    }
}