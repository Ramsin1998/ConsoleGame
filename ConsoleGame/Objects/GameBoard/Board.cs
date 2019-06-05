using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Caching;
using ConsoleGame.Extensions;
using ConsoleGame.Utilities;
using ConsoleGame.Objects.BoardObjects;

namespace ConsoleGame.Objects.GameBoard
{
    public class Board
    {
        public static readonly int MaxRows = Console.WindowHeight - 5;
        public static readonly int MaxColumns = (Console.WindowWidth - 7) / 2;
        public static Dictionary<OccupationType, ConsoleOutputFormat> Formats { get; set; }

        public List<Panel> AlteredPanels { get; set; }
        public List<BoardObject> Objects { get; set; }
        public List<Panel> Panels { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }

        public Panel this[int column, int row]
        {
            get { return Panels[row * Columns + column]; }
        }
        
        public Board()
        {
            Rows = MaxRows;
            Columns = MaxColumns;
            Left = 3;
            Top = 3;

            constructor();
        }

        public Board(int columns, int rows, int left, int top)
        {
            Rows = rows;
            Columns = columns;
            Left = left;
            Top = top;

            constructor();
        }

        private void constructor()
        {
            Panels = new List<Panel>();
            AlteredPanels = new List<Panel>();
            Objects = new List<BoardObject>();

            for (int y = 0; y < Rows; y++)
                for (int x = 0; x < Columns; x++)
                    Panels.Add(new Panel(x, y, this));

            var enumValues = Enum.GetValues(typeof(OccupationType));
            Formats = new Dictionary<OccupationType, ConsoleOutputFormat>(enumValues.Length);

            for (int i = 0; i < enumValues.Length; i++)
            {
                OccupationType occupationType = ((OccupationType)enumValues.GetValue(i));

                Formats.Add(occupationType, occupationType.GetAttributeOfType<ConsoleOutputFormat>());
            }
        }

        public void Render(bool firstTime = false)
        {
            StringBuilder stringBuilder = new StringBuilder(Columns * 2);
            Panel currentPanel;
            int count = 1;

            if (firstTime)
            {
                Console.BackgroundColor = Formats[OccupationType.Neutral].BackgroundColor;
                Console.Clear();
                drawBorder();
            }

            for (int i = 0; i < AlteredPanels.Count; i++)
            {
                currentPanel = AlteredPanels[i];

                if (i != AlteredPanels.Count - 1 
                    && AlteredPanels[i + 1].Coordinates.Column == currentPanel.Coordinates.Column + 1 
                    && AlteredPanels[i + 1].Coordinates.Row == currentPanel.Coordinates.Row
                    && AlteredPanels[i + 1].OccupationType == currentPanel.OccupationType)
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

        private void drawBorder()
        {
            string border = new string(' ', Columns * 2);

            ConsoleColor borderColor = ConsoleColor.Yellow;

            Console.SetCursorPosition((Left - 1) * 2, Top - 1);
            ConsoleOutput.ColorWrite(border, borderColor);

            Console.SetCursorPosition((Left - 1) * 2 , Top - 1 + Rows);
            ConsoleOutput.ColorWrite(border, borderColor);

            for (int i = 0; i <= Rows; i++)
            {
                Console.SetCursorPosition((Left - 1) * 2, Top - 1 + i);
                ConsoleOutput.ColorWrite("  ", borderColor);
            }

            for (int i = 0; i <= Rows; i++)
            {
                Console.SetCursorPosition((Left - 1 + Columns) * 2, Top - 1 + i);
                ConsoleOutput.ColorWrite("  ", borderColor);
            }
        }

        public void AddObject(BoardObject obj, bool clearSurroundings = false)
        {
            if (clearSurroundings)
            {

            }

            Ghost tmpObj = new Ghost()
        }

        public void UpdateObjects()
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                BoardObject obj = Objects[i];

                UpdateObject(obj, true);

                UpdateObject(obj);
            }

            Objects.Clear();
        }

        public void UpdateObject(BoardObject obj, bool clear = false)
        {
            for (int y = 0; y < obj.Style.Height; y++)
            {
                for (int x = 0; x < obj.Style.Width; x++)
                {
                    int actualX = x + obj.Coordinates.Column;
                    int actualY = y + obj.Coordinates.Row;

                    if (obj.Style[x, y] != '*' || actualX < 0 || actualY < 0 || actualX > Columns - 1 || actualY > Rows - 1)
                        continue;

                    else
                    {
                        Panel currentPanel = this[actualX, actualY];

                        if (clear)
                            this[x + obj.PreviousCoordinates.Column, y + obj.PreviousCoordinates.Row].OccupationType = OccupationType.Neutral;

                        else
                        {
                            Collision collision = checkCollision(currentPanel.OccupationType, obj.OccupationType);

                            if (collision != Collision.Nothing)
                            {
                                MemoryCache cache = MemoryCache.Default;

                                CacheItemPolicy cip = new CacheItemPolicy()
                                {
                                    AbsoluteExpiration = DateTime.Now.AddMinutes(5)
                                };

                                cache.Add("collision", collision, cip);

                                return;
                            }

                            else
                                currentPanel.OccupationType = obj.OccupationType;
                        }
                    }
                }
            }
        }

        private static Collision checkCollision(OccupationType collider1, OccupationType collider2)
        {
            if (collider1 == OccupationType.Player)
            {
                if (collider2 == OccupationType.Enemy)
                    return Collision.PlayerXEnemy;

                else if (collider2 == OccupationType.Goal)
                    return Collision.PlayerXGoal;
            }

            else if (collider1 == OccupationType.Enemy)
            {
                if (collider2 == OccupationType.Player)
                    return Collision.PlayerXEnemy;
            }

            else if (collider1 == OccupationType.Goal)
            {
                if (collider2 == OccupationType.Player)
                    return Collision.PlayerXGoal;
            }

            return Collision.Nothing;
        }

        public bool Project(BoardObject obj, Coordinates coordinates)
        {
            for (int y = 0; y < obj.Style.Height; y++)
                for (int x = 0; x < obj.Style.Width; x++)
                {
                    if (obj.Style[x, y] == ' ')
                        continue;

                    else if (this[x + coordinates.Column, y + coordinates.Row].OccupationType == OccupationType.Block)
                        return false;
                }

            return true;
        }

        public bool Project(BoardObject obj, Coordinates coordinates, OccupationType collider)
        {
            for (int y = 0; y < obj.Style.Height; y++)
                for (int x = 0; x < obj.Style.Width; x++)
                {
                    if (obj.Style[x, y] == ' ')
                        continue;

                    else if (this[x + coordinates.Column, y + coordinates.Row].OccupationType == collider)
                        return false;
                }

            return true;
        }

        public void AddBlocks(Style style, int amount)
        {
            Random rng = new Random();

            for (int i = 0; i < amount; i++)
            {
                int column = rng.Next(0, Columns - style.Width);
                int row = rng.Next(0, Rows - style.Height);

                Block block = new Block(column, row, this, style);

                Objects.Add(block);
            }

            UpdateObjects();
        }
    }
}
