using System.Drawing;

namespace PCPDFengineCore.Composition.PageElements
{
    public class Colour
    {
        private byte r;
        private byte g;
        private byte b;

        public byte R { get => r; set => r = value; }
        public byte G { get => g; set => g = value; }
        public byte B { get => b; set => b = value; }
        public Colour() { }
        public Colour(Color color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
        }

        public Colour(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }
}
