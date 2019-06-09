using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Caching;
using ConsoleGame.Extensions;
using ConsoleGame.Utilities;
using ConsoleGame.Objects.GameObjects;

namespace ConsoleGame.Objects.GameEngine
{
    public class Game
    {
        public static readonly int MaxRows = Console.WindowHeight - 6;
        public static readonly int MaxColumns = (Console.WindowWidth - 8) / 2;
        public static Dictionary<OccupationType, ConsoleOutputFormat> Formats { get; set; }

        public List<Panel> Panels { get; set; }
        public List<Panel> AlteredPanels { get; set; }
        public List<GameObject> StaticObjects { get; set; }
        public List<GameObject> MovableObjects { get; set; }
        public List<GameObject> AlteredObjects { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }

        public Game()
        {
            Rows = MaxRows;
            Columns = MaxColumns;
            Left = 3;
            Top = 3;

            initialize();
        }

        public Game(int columns, int rows, int left, int top)
        {
            Rows = rows;
            Columns = columns;
            Left = left;
            Top = top;

            initialize();
        }

        private void initialize()
        {
            Panels = new List<Panel>();
            AlteredPanels = new List<Panel>();
            StaticObjects = new List<GameObject>();
            MovableObjects = new List<GameObject>();
            AlteredObjects = new List<GameObject>();

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

        public Panel this[int column, int row]
        {
            get
            {
                if (column < 0 ||
                    row < 0 ||
                    column > Columns - 1 ||
                    row > Rows - 1)
                    return null;

                else
                    return Panels[row * Columns + column];
            }
        }

        public void AddObject(GameObject obj, int space = 0)
        {
            if (space != 0)
                ClearArea(obj, space);

            if (obj.Movable)
                MovableObjects.Add(obj);

            else
                StaticObjects.Add(obj);

            AlteredObjects.Add(obj);
        }

        public void ClearArea(GameObject obj, int space)
        {
            int xLoopLimit = space * 2 + obj.Style.Width;
            int yLoopLimit = space * 2 + obj.Style.Height;

            for (int y = obj.Coordinates.Row - space; y < yLoopLimit; y++)
                for (int x = obj.Coordinates.Column - space; x < xLoopLimit; x++)
                {
                    Panel currentPanel = this[x, y];

                    if (this[x, y] != null)
                        currentPanel.OccupationType = OccupationType.Neutral;
                }

        }

        public void Render(bool firstTime = false)
        {
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
                    Console.SetCursorPosition((Left + currentPanel.Coordinates.Column - count + 1) * 2, Top + currentPanel.Coordinates.Row);

                    var format = Formats[currentPanel.OccupationType];
                    ConsoleOutput.ColorWrite(new string(format.Texture, count * 2), format.BackgroundColor, format.ForegroundColor);

                    count = 1;
                }
            }

            AlteredPanels.Clear();
        }

        private void drawBorder()
        {
            string border = new string(' ', Columns * 2 + 4);

            ConsoleColor borderColor = ConsoleColor.DarkYellow;

            Console.SetCursorPosition((Left - 1) * 2, Top - 1);
            ConsoleOutput.ColorWrite(border, borderColor);

            Console.SetCursorPosition((Left - 1) * 2, Top + Rows);
            ConsoleOutput.ColorWrite(border, borderColor);

            for (int i = 1; i <= Rows; i++)
            {
                Console.SetCursorPosition((Left - 1) * 2, Top - 1 + i);
                ConsoleOutput.ColorWrite("  ", borderColor);
            }

            for (int i = 1; i <= Rows; i++)
            {
                Console.SetCursorPosition((Left + Columns) * 2, Top - 1 + i);
                ConsoleOutput.ColorWrite("  ", borderColor);
            }
        }

        public void ProcessInputs()
        {
            for (int i = 0; i < MovableObjects.Count; i++)
            {
                var currenObj = MovableObjects[i];

                currenObj.Move();
                if (!Project(currenObj))
                    currenObj.RevertLastMove();
            }
        }

        public void Update()
        {
            for (int i = 0; i < AlteredObjects.Count; i++)
            {
                GameObject obj = AlteredObjects[i];

                if (obj.Movable)
                    UpdateObject(obj, true);

                UpdateObject(obj);
            }

            AlteredObjects.Clear();
        }

        public void UpdateObject(GameObject obj, bool clear = false)
        {
            for (int y = 0; y < obj.Style.Height; y++)
            {
                for (int x = 0; x < obj.Style.Width; x++)
                {
                    if (obj.Style[x, y] == ' ')
                        continue;

                    else
                    {
                        int actualX = 0;
                        int actualY = 0;

                        if (clear)
                        {
                            if (obj.PreviousCoordinates != null)
                            {
                                actualX = x + obj.PreviousCoordinates.Column;
                                actualY = y + obj.PreviousCoordinates.Row;

                                this[actualX, actualY].OccupationType = OccupationType.Neutral;
                            }
                        }

                        else
                        {
                            actualX = x + obj.Coordinates.Column;
                            actualY = y + obj.Coordinates.Row;

                            Panel currentPanel = this[actualX, actualY];

                            Collision collision = checkCollision(currentPanel.OccupationType, obj.OccupationType);

                            if (collision != Collision.Nothing)
                            {
                                MemoryCache cache = MemoryCache.Default;

                                CacheItemPolicy cip = new CacheItemPolicy()
                                {
                                    AbsoluteExpiration = DateTime.Now.AddMinutes(1)
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

        public bool Project(GameObject obj, params OccupationType[] colliders)
        {
            for (int y = 0; y < obj.Style.Height; y++)
                for (int x = 0; x < obj.Style.Width; x++)
                {
                    if (obj.Style[x, y] == ' ')
                        continue;

                    int actualX = x + obj.Coordinates.Column;
                    int actualY = y + obj.Coordinates.Row;

                    if (actualX < 0 ||
                        actualX > Columns - 1 ||
                        actualY < 0 ||
                        actualY > Rows - 1)
                            return false;

                    for (int i = 0; i < colliders.Length; i++)
                        if (this[actualX, actualY].OccupationType == colliders[i])
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
            }
        }

        public void AddBlocks(int amount)
        {
            int width = Columns / 3;
            int height = Rows / 3;
            string sprite = new string('*', height * width);

            Style style = new Style(sprite, width, height);

            Block block = new Block(Columns / 3, Rows / 3, this, style);
        }

        public void KillObject(GameObject obj)
        {

        }
    }
}
