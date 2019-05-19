using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Objects.GameBoard;

namespace ConsoleGame.Objects.BoardObjects
{
    class Block : BoardObject
    {
        public Block(int column, int row, Board board, Style style) : base(column, row, board, style)
        {
            OccupationType = OccupationType.Block;
        }
    }
}
