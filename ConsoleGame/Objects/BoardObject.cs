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
        protected Coordinates coordinates;

        public Coordinates PreviousCoordinates { get; set; }
        public OccupationType OccupationType { get; set; }
        public Style Style { get; set; }
        public Board Board { get; set; }

        public Coordinates Coordinates
        {
            get { return coordinates; }

            set
            {
                PreviousCoordinates = coordinates;

                coordinates = value;
            }
        }

        protected BoardObject(int column, int row, Board board, Style style)
        {
            Coordinates = new Coordinates(column, row);
            Board = board;
            Style = style;
            Board.ObjectsToBeUpdated.Add(this);
        }

        virtual public void Move()
        {
            Board.ObjectsToBeUpdated.Add(this);
        }
    }
}
