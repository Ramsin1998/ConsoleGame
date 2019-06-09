using System;
using ConsoleGame.Objects.GameEngine;
using System.Diagnostics;
using System.Collections.Generic;

namespace ConsoleGame.Objects.GameObjects
{
    public abstract class GameObject
    {
        public int ID_Code => GetHashCode();
        public List<Coordinates> Tracks = new List<Coordinates>() { null, null};

        public OccupationType OccupationType { get; set; }
        public Quadrant Quadrant { get; set; }
        public Style Style { get; set; }
        public Game Game{ get; set; }
        public Stopwatch SW { get; set; }
        public int Speed { get; set; }
        public bool Movable { get; set; }

        public Coordinates Coordinates
        {
            get { return Tracks[Tracks.Count - 1]; }

            set
            {
                if (Tracks[Tracks.Count - 1] == null)
                {
                    int column = value.Column;
                    int row = value.Row;

                    int columnLimit = Game.Columns - Style.Width;
                    if (column < 0)
                        column = 0;

                    else if (column > columnLimit)
                        column = columnLimit;

                    int rowLimit = Game.Rows - Style.Height;
                    if (row < 0)
                        row = 0;

                    else if (row > rowLimit)
                        row = rowLimit;

                    Tracks.Add(new Coordinates(column, row));
                }

                else if (Tracks[Tracks.Count - 1] != value)
                {
                    Tracks.Add(value);

                    if (Game != null)
                        Game.AlteredObjects.Add(this);
                }
            }
        }

        public Coordinates PreviousCoordinates
        {
            get { return Tracks[Tracks.Count - 2]; }
        }

        protected GameObject()
        {
            initialize();
        }

        protected GameObject(Game game, Style style, bool movable)
        {
            Game = game;
            Style = style;
            Movable = movable;

            initialize();
        }

        protected GameObject(int column, int row, Game game, Style style, bool movable)
        {
            Game = game;
            Style = style;
            Movable = movable;
            Coordinates = new Coordinates(column, row);

            initialize();
        }

        private void initialize()
        {
            Game.AddObject(this);
        }

        public virtual void Move() { }

        virtual public void RevertLastMove()
        {
            Tracks.RemoveAt(Tracks.Count - 1);
        }

        protected void RandomizeQuadrantPosition(int quadrantRatio)
        {
            Random rng = new Random();

            int xMinValue = 0;
            int xMaxValue = 0;

            int yMinValue = 0;
            int yMaxValue = 0;

            switch (Quadrant)
            {
                case Quadrant.UpperLeft:

                    xMinValue = 0;
                    xMaxValue = Game.Columns / quadrantRatio - Style.Width;

                    yMinValue = 0;
                    yMaxValue = Game.Rows / quadrantRatio - Style.Height;

                    break;

                case Quadrant.UpperRight:

                    xMinValue = Game.Columns / quadrantRatio * (quadrantRatio - 1);
                    xMaxValue = Game.Columns - Style.Width;

                    yMinValue = 0;
                    yMaxValue = Game.Rows / quadrantRatio - Style.Height;

                    break;

                case Quadrant.LowerLeft:

                    xMinValue = 0;
                    xMaxValue = Game.Columns / quadrantRatio - Style.Width;

                    yMinValue = Game.Rows / quadrantRatio * (quadrantRatio - 1);
                    yMaxValue = Game.Rows - Style.Height;

                    break;

                case Quadrant.LowerRight:

                    xMinValue = Game.Columns / quadrantRatio * (quadrantRatio - 1);
                    xMaxValue = Game.Columns - Style.Width;

                    yMinValue = Game.Rows / quadrantRatio * (quadrantRatio - 1);
                    yMaxValue = Game.Rows - Style.Height;

                    break;
            }

            int column = rng.Next(xMinValue, xMaxValue);
            int row = rng.Next(yMinValue, yMaxValue);

            Coordinates = new Coordinates(column, row);
        }
    }
}