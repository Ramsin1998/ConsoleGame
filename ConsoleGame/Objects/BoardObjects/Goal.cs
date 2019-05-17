using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Objects.GameBoard;

namespace ConsoleGame.Objects.BoardObjects
{
    class Goal : BoardObject
    {
        public Goal(Board board, Style style) : base(board, style)
        {
            OccupationType = OccupationType.Goal;
            Random rng = new Random();
            Quadrant = (Quadrant)(rng.Next(4));
            RandomizeQuadrant(2);
        }
    }
}
