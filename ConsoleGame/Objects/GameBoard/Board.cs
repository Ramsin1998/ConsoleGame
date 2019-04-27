using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Extensions;
using ConsoleGame.Utilities;
using System.Runtime.Caching;

namespace ConsoleGame.Objects.GameBoard
{
    class Board
    {
        private int left;

        public List<Panel> Panels { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Left
        {
            get { return left; }
            set { left = value * 2; }
        }
        public int Top;

        public Board(int columns, int rows)
        {
            Rows = rows;
            Columns = columns;

            Panels = new List<Panel>();

            for (int y = 0; y < rows; y++)
                for (int x = 0; x < columns; x++)
                    Panels.Add(new Panel(x, y));

            var enumValues = Enum.GetValues(typeof(OccupationType));
            Dictionary<OccupationType, ConsoleOutputFormat> formats = new Dictionary<OccupationType, ConsoleOutputFormat>(enumValues.Length);

            for (int i = 0; i < enumValues.Length; i++)
            {
                OccupationType occupationType = ((OccupationType)enumValues.GetValue(i));

                formats.Add(occupationType, occupationType.GetAttributeOfType<ConsoleOutputFormat>());
            }

            ObjectCache cache = MemoryCache.Default;

            cache.Add("formats", formats, new CacheItemPolicy());
        }

        public void Print(int left, int top)
        {
            this.left = left;
            Top = top;

            ObjectCache cache = MemoryCache.Default;
            CacheItem contents = cache.GetCacheItem("formats");

            var formats = (contents.Value as Dictionary<OccupationType, ConsoleOutputFormat>);

            StringBuilder stringBuilder = new StringBuilder(Columns * 2);
            Panel currentPanel;
            Panel nextPanel;
            int count = 1;

            for (int y = 0; y < Rows; y++)
            {
                Console.SetCursorPosition(left * 2, top + y);

                for (int x = 0; x < Columns; x++)
                {
                    currentPanel = this[x, y];
                    nextPanel = x == Columns + 1 ? null : this[x + 1, y];

                    if (nextPanel != null && currentPanel.OccupationType == nextPanel.OccupationType)
                        count++;

                    else
                    {
                        var format = formats[currentPanel.OccupationType];

                        for (; count != 0; count--)
                            stringBuilder.Append(format.Text);

                        ConsoleOutput.ColorWrite(stringBuilder.ToString(), format.BackgroundColor);

                        stringBuilder.Clear();

                        count = 1;
                    }                 
                }
            }
        }

        public Panel this[int column, int row]
        {
            get
            {
                Panel tmp = Panels.Find(p => p.Coordinates.Equals(new Coordinates(column, row)));

                return Panels.Find(p => p.Coordinates.Equals(new Coordinates(column, row)));
            }
        }
    }
}
