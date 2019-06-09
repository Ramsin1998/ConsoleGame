using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ConsoleGame
{
    public enum OccupationType
    {
        [ConsoleOutputFormat(' ', ConsoleColor.Black)]
        Neutral,

        [ConsoleOutputFormat(' ', ConsoleColor.White)]
        Player,

        [ConsoleOutputFormat(' ', ConsoleColor.Green)]
        Goal,

        [ConsoleOutputFormat(' ', ConsoleColor.DarkYellow)]
        Block,

        [ConsoleOutputFormat(' ', ConsoleColor.DarkRed)]
        Enemy,

        [ConsoleOutputFormat(' ', ConsoleColor.Magenta)]
        Bullet
    }

    public enum Quadrant
    {
        UpperLeft,

        UpperRight,

        LowerRight,

        LowerLeft
    }

    public enum Collision
    {
        Nothing,

        PlayerXEnemy,

        PlayerXGoal,

        BlockXPlayer
    }

    [Flags]
    public enum Direction
    {
        None = 0,
        Up = 1,
        Right = 2,
        Down = 4,
        Left = 8,
    }
}
