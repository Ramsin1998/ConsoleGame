
namespace ConsoleGame.Objects
{
    public class Style
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
    }
}
