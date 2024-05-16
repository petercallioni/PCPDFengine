using System.Text.Json.Serialization;

namespace PCPDFengineCore.Fonts
{
    public class FontInfo
    {
        private readonly string family;
        private readonly string style;
        private readonly string filename;
        private byte[]? bytes;

        public FontInfo(string family, string style, string filename)
        {
            this.family = family;
            this.style = style;
            this.filename = filename;
        }

        public string Style { get => style; }
        public string Family { get => family; }

        [JsonIgnore]
        public byte[]? Bytes { get => bytes; set => bytes = value; }
        public string Filename { get => filename; }
    }
}
