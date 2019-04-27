using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Extensions;
using ConsoleGame.Utilities;

namespace ConsoleGame.Objects.GameBoard
{
    class Panel
    {
        public OccupationType OccupationType { get; set; }
        public Coordinates Coordinates { get; set; }

        public Panel(int column, int row)
        {
            Coordinates = new Coordinates(column, row);
            OccupationType = OccupationType.Neutral;
        }
    }
}
