using System;
using ConsoleGame.Objects.GameBoard;

namespace ConsoleGame.Objects.BoardObjects
{
    public class Goal : BoardObject
    {
        public Goal(int quadrantRatio, Quadrant quadrant, Board board, Style style) : base(board, style)
        {
            OccupationType = OccupationType.Goal;
            Random rng = new Random();
            Quadrant = quadrant;
            RandomizeQuadrantPosition(quadrantRatio);
            Ghost = new Ghost(this, 5);
        }

        public Goal(int column, int row, Board board, Style style) : base(column, row, board, style)
        {
            OccupationType = OccupationType.Goal;
            Ghost = new Ghost(this, 5);
        }
    }
}
