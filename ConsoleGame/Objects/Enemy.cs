using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.Objects.GameBoard;

namespace ConsoleGame.Objects
{
    class Enemy : BoardObject
    {
        public Player Player { get; set; }
        public int DeltaX { get; set; }
        public int DeltaY { get; set; }

        public Enemy(Board board, Style style, Player player) : base(board, style)
        {
            OccupationType = OccupationType.Enemy;

            Player = player;
            

            Column = 100;
            Row = 50;
        }

        public override void Move()
        {
            DeltaX = Player.Column - column;
            DeltaY = Player.Row - row;
            int absDeltaX = Math.Abs(DeltaX);
            int absDeltaY = Math.Abs(DeltaY);

            void moveX()
            {
                if (DeltaX > 0)
                    Column += Style.Width;

                else
                    Column -= Style.Width;
            }

            void moveY()
            {
                if (DeltaY > 0)
                    Row += Style.Height;

                else
                    Row -= Style.Height;
            }

            if (absDeltaX == absDeltaY)
            {
                bool coinFlip = new Random().Next(0, 1000) < 500 ? true : false;

                if (coinFlip)
                    moveX();

                else
                    moveY();
            }

            else if (absDeltaX > absDeltaY)
                moveX();

            else
                moveY();
        }
    }
}
