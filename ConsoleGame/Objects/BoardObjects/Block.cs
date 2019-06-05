using ConsoleGame.Objects.GameBoard;

namespace ConsoleGame.Objects.BoardObjects
{
    public class Block : BoardObject
    {
        public Block(int column, int row, Board board, Style style) : base(column, row, board, style)
        {
            OccupationType = OccupationType.Block;
        }
    }
}
