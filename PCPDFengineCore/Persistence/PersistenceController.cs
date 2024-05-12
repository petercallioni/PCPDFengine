using PCPDFengineCore.Fonts;
using System.IO.Compression;
using System.Text.Json;

namespace PCPDFengineCore.Persistence
{
    public class PersistenceController
    {
        JsonSerializerOptions serializeOptions;
        private PersistanceState state;
        public PersistanceState State { get => state; set => state = value; }

        public PersistenceController(bool indent = false)
        {
            serializeOptions = new JsonSerializerOptions();
            serializeOptions.WriteIndented = indent;
            serializeOptions.Converters.Add(new RecordReaderInterfaceConverter());
            state = new PersistanceState();
        }

        public void AddFont(FontController fontController, string familyString, string style)
        {
            fontController.InstalledFonts.TryGetValue(familyString, out List<FontInfo>? family);

            if (family != null)
            {
                foreach (FontInfo? font in family.Where(x => x.Style == style))
                {
                    state.EmbeddedFonts.Add(font);
                }
            }
        }

        public void RemoveFont(string family, string style)
        {
            state.EmbeddedFonts.RemoveAll(x => x.Family == family && x.Style == style);
        }

        public void SaveState(string filePath, bool overwrite = true)
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

        public void LoadState(string filePath)
        {
            state = new PersistanceState();

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