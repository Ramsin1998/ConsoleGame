using System;
using ConsoleGame.Objects.GameEngine;
using System.Diagnostics;
using System.Collections.Generic;
using ConsoleGame.Utilities;

namespace ConsoleGame.Objects.GameObjects
{
    public abstract class GameObject
    {
        public int ID_Code => GetHashCode();

        public List<OccupationType> Avoidables { get; set; }
        public OccupationType OccupationType { get; set; }
        public Quadrant Quadrant { get; set; }
        public Style Style { get; set; }
        public Game Game{ get; set; }
        public Stopwatch SW { get; set; }
        public int Speed { get; set; }
        public bool Movable { get; set; }
        public Coordinates Coordinates { get; set; }
        public Coordinates PreviousCoordinates { get; set; }

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
            Coordinates = new Coordinates(column, row, game.Columns - style.Width, game.Rows - style.Height);

            initialize();
        }

        private void initialize()
        {
            if (Movable)
            {
                SW = new Stopwatch();
                SW.Start();
            }

            Game.AddObject(this);
        }

        public virtual void Move()
        {
            if (!PreviousCoordinates.Equals(Coordinates))
                Game.AlteredObjects.Add(this);
        }

        public void RevertLastMove()
        {
            Coordinates = PreviousCoordinates;
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

            Coordinates = new Coordinates(column, row, Game.Columns - Style.Width, Game.Rows - Style.Height);
        }
    }
}