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
        public Ghost(BoardObject obj, int space) 
        {
            Style = new Style(obj.Style.Sprite.Replace(' ', '*'), obj.Style.Width, obj.Style.Height);
            Style.AddSpaceOnAllSides(space);
            Coordinates = new Coordinates(obj.Coordinates.Column - space, obj.Coordinates.Row - space);
        }
    }
}
