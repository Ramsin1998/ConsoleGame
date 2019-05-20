using System;

namespace ConsoleGame.Utilities
{
    public static class ConsoleOutput
    {
        public static void ColorWrite(string text, ConsoleColor backgroundColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.Write(text);
        }

        public static void ColorWrite(string text, ConsoleColor backgroundColor, ConsoleColor foregroundColor)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(text);
        }
    }
}
