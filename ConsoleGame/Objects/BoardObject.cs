using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Objects.GameBoard;

namespace ConsoleGame.Objects
{
    abstract class BoardObject
    {
        public int PreviousColumn;
        public int PreviousRow;

        protected int column;
        protected int row;

        public int Column
        {
            get { return column; }

            set
            {
                if (value < 0)
                    Column = 0;

                else if (value + Style.Width > Board.Columns)
                    Column = Board.Columns - Style.Width;

                else
                {
                    PreviousColumn = column;
                    PreviousRow = row;

                    column = value;

                    Board.ObjectsToBeUpdated.Add(this);
                }
            }
        }

        public int Row
        {
            get { return row; }

            set
            {
                if (value < 0)
                    Row = 0;

                else if (value + Style.Height > Board.Rows)
                    Row = Board.Rows - Style.Height;

                else
                {
                    PreviousRow = row;
                    PreviousColumn = column;

                    row = value;

                    Board.ObjectsToBeUpdated.Add(this);
                }
            }
        }

        public OccupationType OccupationType { get; set; }
        public Style Style { get; set; }
        public Board Board { get; set; }

        protected BoardObject(Board board, Style style)
        {
            Board = board;
            Style = style;
            Board.ObjectsToBeUpdated.Add(this);
        }

        virtual public void Move() { }
    }
}
