using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Objects
{
    public class Style
    {
        public string Texture { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Style(string texture, int width, int height)
        {
            Texture = texture;
            Width = width;
            Height = height;
        }
    }
}
