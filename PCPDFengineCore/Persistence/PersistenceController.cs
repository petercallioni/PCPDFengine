using System.Text.Json;

namespace PCPDFengineCore.Persistence
{
    public class PersistenceController
    {
        JsonSerializerOptions serializeOptions;
        public PersistenceController(bool indent = false)
        {
            serializeOptions = new JsonSerializerOptions();
            serializeOptions.WriteIndented = indent;
            serializeOptions.Converters.Add(new RecordReaderInterfaceConverter());
        }
        public void SaveState(PersistanceState state, string filePath)
        {
            string json = JsonSerializer.Serialize(state, serializeOptions);
            File.WriteAllText(filePath, json);

        }

        public PersistanceState LoadState(string filePath)
        {
            string json = File.ReadAllText(filePath);
            PersistanceState state = JsonSerializer.Deserialize<PersistanceState>(json, serializeOptions)!;
            return state;
        }
    }
}