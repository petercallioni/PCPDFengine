using System.Text.Json;

namespace PCPDFengineCore.Extensions
{
    public static class ObjectExtensions
    {
        public static string DumpObject(this object obj)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(obj, options);
            return json;
        }
    }
}
