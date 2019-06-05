using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Objects.GameBoard;
using System.Windows.Input;
using System.Diagnostics;
using System.Threading;

namespace ConsoleGame.Objects.BoardObjects
{
    public class Ghost : BoardObject
    {
        public Ghost(Board board, BoardObject obj, int space) : base(board, obj.Style.AddSpaceOnAllSides(space)) 
        {

        }
    }
}
