using System;
using ConsoleGame.Objects.GameEngine;
using System.Diagnostics;
using System.Collections.Generic;

namespace ConsoleGame.Objects.GameObjects
{
    public abstract class GameObject
    {
        //public int ID_Code => 
        public List<Coordinates> Tracks = new List<Coordinates>(2) { null, null};

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
                if (Tracks[Tracks.Count - 1] != value)
                {
                    Tracks.Add(value);

                    if (Game != null)
                    Game.AlteredObjects.Add(this);
                }
            }
        }

        public Coordinates PreviousCoordinates
        {
            get
            {
                if (Tracks.Count > 1)
                    return Tracks[Tracks.Count - 2];

                else
                    return null;
            }
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
            if (column < 0)
                column = 0;

            else if (column > game.Columns - style.Width - 1)
                column = game.Columns - style.Width - 1;

            if (row < 0)
                row = 0;

            else if (row > game.Rows - style.Height - 1)
                row = game.Rows - style.Height - 1;

            Coordinates = new Coordinates(column, row);

            Game = game;
            Style = style;
            Movable = movable;

            initialize();
        }

        private void initialize()
        {
            Game.AddObject(this);
        }

        public virtual void Move()
        {
            //Goard.AlteredObjects.Add(this);
        }

        virtual public void ResetLastMove()
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