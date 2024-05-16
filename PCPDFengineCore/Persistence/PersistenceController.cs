using PCPDFengineCore.Extensions;
using PCPDFengineCore.Fonts;
using System.IO.Compression;
using System.Text.Json;

namespace PCPDFengineCore.Persistence
{
    public class PersistenceController
    {
        JsonSerializerOptions serializeOptions;
        private PersistanceState state;
        private FontController? fontController;

        private byte[]? loadedSaveFile;
        public PersistanceState State { get => state; set => state = value; }
        private FontController FontController
        {
            get
            {
                if (fontController == null)
                {
                    throw new NullReferenceException("FontController called before it is set.");
                }
                return fontController;
            }
        }

        public byte[]? LoadedStateFile { get => loadedSaveFile; }

        private void LoadEmbededFonts()
        {
            if (state.EmbedFonts)
            {
                foreach (FontInfo font in state.EmbeddedFonts)
                {
                    font.Bytes = GetFileFromLoadedState(Path.Combine(SaveFileLayout.FONTS_FOLDER, font.Filename));
                }
            }
        }

        public byte[] GetFileFromLoadedState(string path)
        {
            if (loadedSaveFile == null)
            {
                throw new NullReferenceException("Tried to read from non-existent save file.");
            }

            using (MemoryStream memoryStream = new MemoryStream(loadedSaveFile))
            {
                using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries.Where(x => x.FullName == path.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)))
                    {
                        using (Stream entryStream = entry.Open())
                        {
                            StreamReader reader = new StreamReader(entryStream);
                            return entryStream.ReadFully();
                        }
                    }
                }
            }

            return null!;
        }


        public PersistenceController(bool indent = false)
        {
            serializeOptions = new JsonSerializerOptions();
            serializeOptions.WriteIndented = indent;
            serializeOptions.Converters.Add(new RecordReaderInterfaceConverter());
            state = new PersistanceState();
        }

        public void SetEmbedFonts(bool embedFonts)
        {
            if (State.EmbedFonts != embedFonts)
            {
                State.EmbedFonts = embedFonts;

                FontController.LoadInstalledTtfFonts();
            }

        }

        public void SetFontController(FontController fontController)
        {
            this.fontController = fontController;
        }

        public void AddFont(string familyString, string style)
        {
            FontController.InstalledFonts.TryGetValue(familyString, out List<FontInfo>? family);

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
            File.WriteAllText(Path.Combine(tempDirectory.FullName, SaveFileLayout.STATE_JSON), json);

            DirectoryInfo fontsDirectory = Directory.CreateDirectory(Path.Combine(tempDirectory.FullName, SaveFileLayout.FONTS_FOLDER));

            if (state.EmbedFonts)
            {
                foreach (FontInfo font in state.EmbeddedFonts)
                {
                    File.WriteAllBytes(Path.Combine(fontsDirectory.FullName, font.Filename), font.Bytes!);
                }
            }


            if (overwrite && File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            ZipFile.CreateFromDirectory(tempDirectory.FullName, filePath, CompressionLevel.SmallestSize, false);

            tempDirectory.Delete(true);
        }

        public void LoadState(string filePath)
        {
            state = new PersistanceState();

            using (FileStream zipToOpen = new FileStream(filePath, FileMode.Open))
            {
                loadedSaveFile = zipToOpen.ReadFully();
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries.Where(x => x.Name == SaveFileLayout.STATE_JSON))
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

            LoadEmbededFonts();
        }

        public string GetRawPersistantStateJson(string filePath)
        {
            string state = "";

            using (FileStream zipToOpen = new FileStream(filePath, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries.Where(x => x.Name == SaveFileLayout.STATE_JSON))
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