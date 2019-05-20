using System;

namespace ConsoleGame
{
    public class ConsoleOutputFormat : Attribute
    {
        public string Text { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public ConsoleColor ForegroundColor { get; set; }

        public ConsoleOutputFormat(string text, ConsoleColor backgroundColor, ConsoleColor foregroundColor = ConsoleColor.White)
        {
            Text = text;
            BackgroundColor = backgroundColor;
            ForegroundColor = foregroundColor;
        }
    }
}
