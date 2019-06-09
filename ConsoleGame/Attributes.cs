using System;

namespace ConsoleGame
{
    public class ConsoleOutputFormat : Attribute
    {
        public char Texture { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ForegroundColor { get; set; }

        public ConsoleOutputFormat(char text, ConsoleColor backgroundColor, ConsoleColor foregroundColor = ConsoleColor.White)
        {
            Texture = text;
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
        }
    }
}
