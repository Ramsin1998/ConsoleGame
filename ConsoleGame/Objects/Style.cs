using ConsoleGame.Utilities;

namespace ConsoleGame.Objects
{
    public struct Style
    {
        public string Sprite { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

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

        public void AddSpaceOnAllSides(int space)
        {
            string spaceOnTopAndBelow = new string(' ', Width);
            for (int i = 0; i < space; i++)
            {
                Sprite.Insert(0, spaceOnTopAndBelow);
                Sprite += spaceOnTopAndBelow;
            }

            string spaceOnSides = new string(' ', space);
            string[] spriteLines = (string[])Sprite.Split(Width);

            for (int i = 0; i < spriteLines.Length; i++)
            {
                spriteLines[i].Insert(0, spaceOnSides);
                spriteLines[i] += spaceOnSides;
            }

            Sprite = string.Join("", spriteLines);

            Height += space * 2;
            Width += space * 2;
        }
    }
}
