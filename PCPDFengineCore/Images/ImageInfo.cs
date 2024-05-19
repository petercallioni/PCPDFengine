using System.Text.Json.Serialization;

namespace PCPDFengineCore.Images
{
    public class ImageInfo
    {
        private readonly string filename;
        private byte[]? bytes;

        public ImageInfo(string filename)
        {
            this.filename = filename;
        }

        [JsonIgnore]
        public byte[]? Bytes { get => bytes; set => bytes = value; }
        public string Filename { get => filename; }
    }
}
