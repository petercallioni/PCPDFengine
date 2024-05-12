namespace PCPDFengineCore.Fonts
{
    public class FontInfo
    {
        private readonly string family;
        private readonly string style;
        private readonly byte[] bytes;

        public FontInfo(string family, string style, byte[] bytes)
        {
            this.family = family;
            this.style = style;
            this.bytes = bytes;
        }

        public string Style { get => style; }
        public string Family { get => family; }
        public byte[] Bytes { get => bytes; }
    }
}
