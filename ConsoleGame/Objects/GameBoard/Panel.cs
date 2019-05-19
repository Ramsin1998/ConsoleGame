using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Extensions;
using ConsoleGame.Utilities;

namespace ConsoleGame.Objects.GameBoard
{
    public class Panel
    {
        private OccupationType occupationType;

        public OccupationType OccupationType
        {
            get { return occupationType; }
            
            set
            {
                if (value != occupationType)
                {
                    occupationType = value;
                    Board.AlteredPanels.Add(this);
                }
            }
        }

        public Coordinates Coordinates { get; set; }
        public Board Board { get; set; }

        public Panel(int column, int row, Board board)
        {
            Coordinates = new Coordinates(column, row);
            Board = board;
            OccupationType = OccupationType.Neutral;
        }
    }
}
