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
        private OccupationType occupationType;

        public OccupationType OccupationType
        {
            get { return occupationType; }
            
            set
            {
                //if (occupationType == OccupationType.Enemy && value == OccupationType.Player || occupationType == OccupationType.Player && value == OccupationType.Enemy)
                //{

                //}

                if (value != occupationType)
                {
                    occupationType = value;
                    Board.AlteredPanels.Add(this);
                }
            }
        }

        public Coordinates Coordinates { get; set; }

        public Panel(int column, int row)
        {
            Coordinates = new Coordinates(column, row);
            OccupationType = OccupationType.Neutral;
        }
    }
}
