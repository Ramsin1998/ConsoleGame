using ConsoleGame.Utilities;
using System.Linq;
using System;

namespace ConsoleGame.Objects
{
    public struct Style
    {
        public string Sprite { get; }
        public int Width { get; }
        public int Height { get; }

        public Style(string texture, int width, int height)
        {
            Sprite = texture;
            Width = width;
            Height = height;
        }

        public char this[int x, int y]
        {
            get { return Sprite[y * Width + x]; }
        }
    }
}