using PCPDFengineCore.Extensions;
using PCPDFengineCore.Fonts;
using PCPDFengineCore.Images;
using System.IO.Compression;
using System.Text.Json;

namespace PCPDFengineCore.Persistence
{
    public class PersistenceController
    {
        JsonSerializerOptions serializeOptions;
        private PersistanceState state;
        private FontController? fontController;
        private ImageController? imageController;

        private byte[]? loadedSaveFile;
        public PersistanceState State { get => state; set => state = value; }

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

        private void LoadEmbededImages()
        {
            foreach (ImageInfo image in state.EmbeddedImages)
            {
                image.Bytes = GetFileFromLoadedState(Path.Combine(SaveFileLayout.IMAGES_FOLDER, image.Filename));
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

        public void SetFontController(FontController fontController)
        {
            this.fontController = fontController;
        }

        public void SetImageController(ImageController imageController)
        {
            this.imageController = imageController;
        }

        public void SaveState(string filePath, bool overwrite = true)
        {
            DirectoryInfo tempDirectory = Directory.CreateTempSubdirectory();

            string json = JsonSerializer.Serialize(state, serializeOptions);
            File.WriteAllText(Path.Combine(tempDirectory.FullName, SaveFileLayout.STATE_JSON), json);

            DirectoryInfo fontsDirectory = Directory.CreateDirectory(Path.Combine(tempDirectory.FullName, SaveFileLayout.FONTS_FOLDER));
            DirectoryInfo imagesDirectory = Directory.CreateDirectory(Path.Combine(tempDirectory.FullName, SaveFileLayout.IMAGES_FOLDER));

            if (state.EmbedFonts)
            {
                foreach (FontInfo font in state.EmbeddedFonts)
                {
                    File.WriteAllBytes(Path.Combine(fontsDirectory.FullName, font.Filename), font.Bytes!);
                }
            }

            foreach (ImageInfo image in state.EmbeddedImages)
            {
                File.WriteAllBytes(Path.Combine(imagesDirectory.FullName, image.Filename), image.Bytes!);
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
            LoadEmbededImages();
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