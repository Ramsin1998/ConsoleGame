using System;
using ConsoleGame.Objects.GameEngine;

namespace ConsoleGame.Objects.GameObjects
{
    public class Goal : GameObject
    {
        public Goal(int quadrantRatio, Quadrant quadrant, Game game, Style style) : base(game, style, false)
        {
            Random rng = new Random();
            Quadrant = quadrant;
            RandomizeQuadrantPosition(quadrantRatio);
            initialize();
        }

        public Goal(int column, int row, Game game, Style style) : base(column, row, game, style, false)
        {
            initialize();
        }

        private void initialize()
        {
            OccupationType = OccupationType.Goal;
        }
    }
}
