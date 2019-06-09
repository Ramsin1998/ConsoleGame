using ConsoleGame.Objects.GameEngine;

namespace ConsoleGame.Objects.GameObjects
{
    public class Block : GameObject
    {
        public Block(int column, int row, Game game, Style style) : base(column, row, game, style, false)
        {
            OccupationType = OccupationType.Block;
        }
    }
}