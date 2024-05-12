using System.IO.Compression;
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

        public void SaveState(PersistanceState state, string filePath, bool overwrite = true)
        {
            DirectoryInfo tempDirectory = Directory.CreateTempSubdirectory();

            string json = JsonSerializer.Serialize(state, serializeOptions);
            File.WriteAllText(Path.Combine(tempDirectory.FullName, SaveFileLayout.State), json);

            if (overwrite && File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            ZipFile.CreateFromDirectory(tempDirectory.FullName, filePath);


            tempDirectory.Delete(true);
        }

        public PersistanceState LoadState(string filePath)
        {
            PersistanceState state = new PersistanceState();

            using (FileStream zipToOpen = new FileStream(filePath, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries.Where(x => x.Name == SaveFileLayout.State))
                    {
                        using (Stream entryStream = entry.Open())
                        {
                            StreamReader reader = new StreamReader(entryStream);
                            string json = reader.ReadToEnd();
                            state = JsonSerializer.Deserialize<PersistanceState>(json, serializeOptions)!;
                        }
                    }
                }
            }

            return state;
        }

        public string GetRawPersistantStateJson(string filePath)
        {
            string state = "";

            using (FileStream zipToOpen = new FileStream(filePath, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries.Where(x => x.Name == SaveFileLayout.State))
                    {
                        using (Stream entryStream = entry.Open())
                        {
                            StreamReader reader = new StreamReader(entryStream);
                            state = reader.ReadToEnd();
                        }
                    }
                }
            }

            return state;
        }
    }
}