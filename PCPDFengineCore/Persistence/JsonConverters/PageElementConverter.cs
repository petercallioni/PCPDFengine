using PCPDFengineCore.Composition.PageElements;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PCPDFengineCore.Persistence.JsonConverters
{
    public class PageElementConverter : JsonConverter<PageElement>
    {
        public override PageElement Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonElement jsonObject = JsonDocument.ParseValue(ref reader).RootElement;
            JsonSerializerOptions newOptions = new JsonSerializerOptions();
            try
            {
                string type = jsonObject.GetProperty("ClassTypeString").GetString() ?? "";

                if (type.Equals(typeof(Line).FullName))
                {
                    return JsonSerializer.Deserialize<Line>(jsonObject.GetRawText(), newOptions)!;
                }
                else if (type.Equals(typeof(PageElement).FullName))
                {
                    return JsonSerializer.Deserialize<PageElement>(jsonObject.GetRawText(), newOptions)!;
                }
                else if (type.Equals(typeof(Polygon).FullName))
                {
                    return JsonSerializer.Deserialize<Polygon>(jsonObject.GetRawText(), newOptions)!;
                }
                else
                {
                    throw new NotImplementedException($"Page Element {type} not implemented in PageElementConverter");
                }
            }
            catch
            {
                throw new NotImplementedException("Page Element object missing ClassTypeString");
            }
        }

        public override void Write(Utf8JsonWriter writer, PageElement value, JsonSerializerOptions options)
        {
            JsonSerializerOptions newOptions = new JsonSerializerOptions();
            JsonSerializer.Serialize(writer, (object)value, newOptions);
        }
    }
}
