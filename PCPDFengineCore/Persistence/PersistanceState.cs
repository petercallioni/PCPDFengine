using PCPDFengineCore.Interfaces;
using PCPDFengineCore.Persistence.Records;
using PCPDFengineCore.RecordReader;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PCPDFengineCore.Persistence
{
    public class PersistanceState
    {
        private FileInformation _fileInformation;
        private IRecordReader _recordReader;

        public FileInformation FileInformation { get => _fileInformation; set => _fileInformation = value; }

        public IRecordReader RecordReader { get => _recordReader; set => _recordReader = value; }
    }

    public class RecordReaderInterfaceConverter : JsonConverter<IRecordReader>
    {
        public override IRecordReader Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            JsonElement jsonObject = JsonDocument.ParseValue(ref reader).RootElement;
            JsonSerializerOptions newOptions = new JsonSerializerOptions();
            try
            {
                string type = jsonObject.GetProperty("ClassTypeString").GetString() ?? "";

                if (type.Equals(typeof(TextDelimitedRecordReader).FullName))
                {
                    return JsonSerializer.Deserialize<TextDelimitedRecordReader>(jsonObject.GetRawText(), newOptions)!;
                }
                else if (type.Equals(typeof(TextFixedRecordReader).FullName))
                {
                    return JsonSerializer.Deserialize<TextFixedRecordReader>(jsonObject.GetRawText(), newOptions)!;
                }
                else
                {
                    throw new NotImplementedException($"Loading {type} data ready type not implemented");
                }
            }
            catch
            {
                throw new NotImplementedException("Data reader object missing ClassTypeString");
            }
        }

        public override void Write(Utf8JsonWriter writer, IRecordReader value, JsonSerializerOptions options)
        {
            JsonSerializerOptions newOptions = new JsonSerializerOptions();
            JsonSerializer.Serialize(writer, (object)value, newOptions);
        }
    }
}
