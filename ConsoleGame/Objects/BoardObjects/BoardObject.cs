using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Objects.GameBoard;
using System.Diagnostics;

namespace ConsoleGame.Objects.BoardObjects
{
    public abstract class BoardObject
    {
        protected Coordinates coordinates;

        public Coordinates PreviousCoordinates { get; set; }
        public OccupationType OccupationType { get; set; }
        public Quadrant Quadrant { get; set; }
        public Style Style { get; set; }
        public Board Board { get; set; }
        public Stopwatch SW { get; set; }
        public int Speed { get; set; }

        public Coordinates Coordinates
        {
            get { return coordinates; }

            set
            {
                PreviousCoordinates = coordinates;

                coordinates = value;
            }
        }

        protected BoardObject(Board board, Style style)
        {
            Board = board;
            Style = style;
        }

        protected BoardObject(int column, int row, Board board, Style style)
        {
            if (column < 0)
                column = 0;

            else if (column > board.Columns - style.Width - 1)
                column = board.Columns - style.Width - 1;

            if (row < 0)
                row = 0;

            else if (row > board.Rows - style.Height - 1)
                row = board.Rows - style.Height - 1;

            Coordinates = new Coordinates(column, row);
            Board = board;
            Style = style;
        }

        virtual public void Move() { }

        protected void RandomizeQuadrantPosition(int quadrantRatio)
        {
            Random rng = new Random();

            int xMinValue = 0;
            int xMaxValue = 0;

            int yMinValue = 0;
            int yMaxValue = 0;

            switch (Quadrant)
            {
                case Quadrant.UpperLeft:

                    xMinValue = 0;
                    xMaxValue = Board.Columns / quadrantRatio - Style.Width;

                    yMinValue = 0;
                    yMaxValue = Board.Rows / quadrantRatio - Style.Height;

                    break;

                case Quadrant.UpperRight:

                    xMinValue = Board.Columns / quadrantRatio * (quadrantRatio - 1);
                    xMaxValue = Board.Columns - Style.Width;

                    yMinValue = 0;
                    yMaxValue = Board.Rows / quadrantRatio - Style.Height;

                    break;

                case Quadrant.LowerLeft:

                    xMinValue = 0;
                    xMaxValue = Board.Columns / quadrantRatio - Style.Width;

                    yMinValue = Board.Rows / quadrantRatio * (quadrantRatio - 1);
                    yMaxValue = Board.Rows - Style.Height;

                    break;

                case Quadrant.LowerRight:

                    xMinValue = Board.Columns / quadrantRatio * (quadrantRatio - 1);
                    xMaxValue = Board.Columns - Style.Width;

                    yMinValue = Board.Rows / quadrantRatio * (quadrantRatio - 1);
                    yMaxValue = Board.Rows - Style.Height;

                    break;
            }

            int column = rng.Next(xMinValue, xMaxValue);
            int row = rng.Next(yMinValue, yMaxValue);

            Coordinates = new Coordinates(column, row);
        }
    }
}
