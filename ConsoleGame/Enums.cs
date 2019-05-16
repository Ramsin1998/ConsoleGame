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
        [ConsoleOutputFormat("  ", ConsoleColor.Black)]
        Neutral,

        [ConsoleOutputFormat("  ", ConsoleColor.White)]
        Player,

        [ConsoleOutputFormat("  ", ConsoleColor.Green)]
        Goal,

        [ConsoleOutputFormat("  ", ConsoleColor.DarkGray)]
        BlockedField,

        [ConsoleOutputFormat("  ", ConsoleColor.Red)]
        Enemy
    }

    public enum Quadrant
    {
        UpperLeft,

        UpperRight,

        LowerRight,

        LowerLeft
    }
}
