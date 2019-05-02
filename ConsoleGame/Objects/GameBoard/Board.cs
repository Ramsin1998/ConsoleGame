
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
        public Dictionary<OccupationType, ConsoleOutputFormat> Formats { get; set; }
        public List<Panel> Panels { get; set; }
        public List<Panel> AlteredPanels { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }

        public Board(int columns, int rows)
        {
            Rows = rows;
            Columns = columns;

            Panels = new List<Panel>();
            AlteredPanels = new List<Panel>();

            for (int y = 0; y < rows; y++)
                for (int x = 0; x < columns; x++)
                    Panels.Add(new Panel(x, y));

            var enumValues = Enum.GetValues(typeof(OccupationType));
            Formats = new Dictionary<OccupationType, ConsoleOutputFormat>(enumValues.Length);

            for (int i = 0; i < enumValues.Length; i++)
            {
                OccupationType occupationType = ((OccupationType)enumValues.GetValue(i));

                Formats.Add(occupationType, occupationType.GetAttributeOfType<ConsoleOutputFormat>());
            }
        }

        public Panel this[int column, int row]
        {
            get
            {
                Panel tmp = Panels.Find(p => p.Coordinates.Equals(new Coordinates(column, row)));

                AlteredPanels.Add(tmp);

                return Panels.Find(p => p.Coordinates.Equals(new Coordinates(column, row)));
            }
        }

        public void Print(int left, int top)
        {
            Left = left;
            Top = top;

            ObjectCache cache = MemoryCache.Default;
            CacheItem contents = cache.GetCacheItem("formats");

            StringBuilder stringBuilder = new StringBuilder(Columns * 2);
            Panel currentPanel;
            Panel nextPanel;
            int count = 1;

            Console.BackgroundColor = Formats[OccupationType.Neutral].BackgroundColor;
            Console.Clear();

            for (int y = 0; y < Rows; y++)
            {
                Console.SetCursorPosition(left * 2, top + y);

                for (int x = 0; x < Columns; x++)
                {
                    currentPanel = this[x, y];

                    if (currentPanel.OccupationType == OccupationType.Neutral)
                        continue;

                    nextPanel = x == Columns + 1 ? null : this[x + 1, y];

                    if (nextPanel != null && currentPanel.OccupationType == nextPanel.OccupationType)
                        count++;

                    else
                    {
                        var format = Formats[currentPanel.OccupationType];

                        for (; count != 0; count--)
                            stringBuilder.Append(format.Text);

                        ConsoleOutput.ColorWrite(stringBuilder.ToString(), format.BackgroundColor);

                        stringBuilder.Clear();

                        count = 1;
                    }                 
                }
            }
        }

        public void Update()
        {
            for (int i = 0; i < AlteredPanels.Count; i++)
            {
                Panel currentPanel = AlteredPanels[i];
                var format = Formats[currentPanel.OccupationType];

                Console.SetCursorPosition((Left + currentPanel.Coordinates.Column) * 2, Top + currentPanel.Coordinates.Row);
                ConsoleOutput.ColorWrite(format.Text, format.BackgroundColor, format.BackgroundColor);

                AlteredPanels.Clear();
            }
        }
    }
}
