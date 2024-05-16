namespace PCPDFengineCore.Extensions
{
    public static class FileStreamExtensions
    {
        public static byte[] ReadFully(this FileStream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
