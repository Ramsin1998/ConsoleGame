using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Objects.GameEngine
{
    [Serializable]
    public class Coordinates
    {
        private int row;
        private int column;
        private int maxColumn = Int32.MaxValue;
        private int maxRow = Int32.MaxValue;


        public int Row
        {
            get { return row; }

            set
            {
                if (value < 0)
                    row = 0;

                else if (value > maxRow)
                    row = maxRow;

                else
                    row = value;
            }
        }

        public int Column
        {
            get { return column; }

            set
            {
                if (value < 0)
                    column = 0;

                else if (value > maxColumn)
                    column = maxColumn;

                else
                    column = value;
            }
        }

        public Coordinates(int column, int row)
        {
            Row = row;
            Column = column;
        }

        public Coordinates(int column, int row, int maxColumn, int maxRow)
        {
            this.maxColumn = maxColumn;
            this.maxRow = maxRow;
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