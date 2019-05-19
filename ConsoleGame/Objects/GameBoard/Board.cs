
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Extensions;
using ConsoleGame.Utilities;
using System.Runtime.Caching;
using ConsoleGame.Objects.BoardObjects;
using ConsoleGame.Extensions;

namespace ConsoleGame.Objects.GameBoard
{
    public class Board
    {
        public static readonly int MaxRows = Console.WindowHeight - 4;
        public static readonly int MaxColumns = (Console.WindowWidth - 5) / 2;
        public static Dictionary<OccupationType, ConsoleOutputFormat> Formats { get; set; }
        public static List<Panel> AlteredPanels { get; set; }

        public List<BoardObject> Objects { get; set; }
        public List<Panel> Panels { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        
        public Board()
        {
            Rows = MaxRows;
            Columns = MaxColumns;
            Left = 2;
            Top = 2;

            constructorScript();
        }

        public Board(int columns, int rows, int left, int top)
        {
            Rows = rows;
            Columns = columns;
            Left = left;
            Top = top;

            constructorScript();
        }

        private void constructorScript()
        {
            Panels = new List<Panel>();
            AlteredPanels = new List<Panel>();
            Objects = new List<BoardObject>();

            for (int y = 0; y < Rows; y++)
                for (int x = 0; x < Columns; x++)
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
            get { return Panels[row * Columns + column]; }
        }

        public void Render(bool firstTime = false)
        {
            StringBuilder stringBuilder = new StringBuilder(AlteredPanels.Count);
            Panel currentPanel;
            int count = 1;

            if (firstTime)
            {
                Console.BackgroundColor = Formats[OccupationType.Neutral].BackgroundColor;
                Console.Clear();
            }

            for (int i = 0; i < AlteredPanels.Count; i++)
            {
                currentPanel = AlteredPanels[i];

                if (i != AlteredPanels.Count - 1 && AlteredPanels[i + 1].Coordinates.Column == currentPanel.Coordinates.Column + 1 && AlteredPanels[i + 1].OccupationType == currentPanel.OccupationType)
                        count++;

                else
                {
                    var format = Formats[currentPanel.OccupationType];

                    Console.SetCursorPosition((Left + currentPanel.Coordinates.Column - count + 1) * 2, Top + currentPanel.Coordinates.Row);

                    for (; count > 0; count--)
                        stringBuilder.Append(format.Text);

                    ConsoleOutput.ColorWrite(stringBuilder.ToString(), format.BackgroundColor, format.ForegroundColor);

                    stringBuilder.Clear();
                    count = 1;
                }
            }

            AlteredPanels.Clear();
        }

        public void UpdateObjects()
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                BoardObject obj = Objects[i];

                this.UpdateObject(obj, true);

                this.UpdateObject(obj);
            }

            Objects.Clear();
        }

    }
}