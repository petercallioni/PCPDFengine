using System.Text.Json;

namespace PCPDFengineCore.Persistence
{
    public class PersistenceController
    {
        public void SaveState(PersistanceState state, string filePath)
        {
            JsonSerializerOptions serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters =
                {
                    new RecordReaderInterfaceConverter()
                }
            };
            string json = JsonSerializer.Serialize(state, serializeOptions);
            File.WriteAllText(filePath, json);

        }

        public PersistanceState LoadState(string filePath)
        {
            JsonSerializerOptions serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters =
                {
                    new RecordReaderInterfaceConverter()
                }
            };
            string json = File.ReadAllText(filePath);
            PersistanceState state = JsonSerializer.Deserialize<PersistanceState>(json, serializeOptions)!;
            return state;
        }
    }
}