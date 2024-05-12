using FreeTypeSharp;
using PCPDFengineCore.Persistence;
using System.Runtime.InteropServices;
using static FreeTypeSharp.FT;

namespace PCPDFengineCore.Fonts
{
    public class FontController
    {
        private Dictionary<string, List<FontInfo>> installedFonts;
        private bool embedFonts;

        public FontController(bool embedFonts = true)
        {
            this.embedFonts = embedFonts;
            this.installedFonts = LoadInstalledTtfFonts(embedFonts);
        }

        public Dictionary<string, List<FontInfo>> InstalledFonts { get => installedFonts; }
        public bool EmbedFonts
        {
            get => embedFonts;
            set
            {
                if (embedFonts != value)
                {
                    embedFonts = value;
                    installedFonts = LoadInstalledTtfFonts(embedFonts);
                }
            }
        }

        public FontInfo GetFontInfo(string fontName, string style, PersistanceState? state = null)
        {
            if (state != null)
            {
                List<FontInfo> embeddedFont = state.EmbeddedFonts.Where(x => x.IsEmbedded == true
                    && x.Family == fontName
                    && x.Style == style).ToList();

                if (embeddedFont.Count > 0)
                {
                    return embeddedFont.First();
                }
            }

            // either no state or embedded font not found

            FontInfo installedFont = LoadSpecificFont(fontName, style);

            return installedFont;
        }

        private FontInfo GetFontInformation(string ttfFilePath, bool loadFontBytes)
        {
            string family = "";
            string style = "";
            byte[]? bytes = embedFonts ? File.ReadAllBytes(ttfFilePath) : null;

            unsafe
            {
                FT_LibraryRec_* lib;
                FT_FaceRec_* face;

                FT_Error error = FT_Init_FreeType(&lib);

                byte* ttfFileBytes = (byte*)Marshal.StringToHGlobalAnsi(ttfFilePath);

                error = FT_New_Face(lib, ttfFileBytes, 0, &face);

                if (error == 0)
                {
                    // The font was loaded successfully.
                    // You can now access the properties of the font.
                    style = Marshal.PtrToStringAnsi((nint)face->style_name)!;
                    family = Marshal.PtrToStringAnsi((nint)face->family_name)!;
                }

                Marshal.FreeHGlobal((IntPtr)error);
                Marshal.FreeHGlobal((IntPtr)lib);
                Marshal.FreeHGlobal((IntPtr)ttfFileBytes);
            }

            return new FontInfo(family, style, bytes);
        }

        private Dictionary<string, List<FontInfo>> LoadInstalledTtfFonts(bool embedFonts)
        {
            string fontsfolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

            DirectoryInfo installedFontsFolder = new DirectoryInfo(fontsfolder);
            Dictionary<string, List<FontInfo>> fonts = new Dictionary<string, List<FontInfo>>();

            foreach (FileInfo fontFile in installedFontsFolder.EnumerateFiles().Where(x => x.Name.ToUpper().EndsWith(".TTF")))
            {
                FontInfo fontInfo = GetFontInformation(fontFile.FullName, embedFonts);
                bool exists = fonts.TryGetValue(fontInfo.Family, out List<FontInfo>? list);

                if (!exists)
                {
                    list = new List<FontInfo>();
                    fonts.Add(fontInfo.Family, list);
                }

                list!.Add(fontInfo);
            }

            return fonts;
        }

        private FontInfo LoadSpecificFont(string family, string style)
        {
            string fontsfolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

            DirectoryInfo installedFontsFolder = new DirectoryInfo(fontsfolder);

            foreach (FileInfo fontFile in installedFontsFolder.EnumerateFiles().Where(x => x.Name.ToUpper().EndsWith(".TTF")))
            {
                FontInfo fontInfo = GetFontInformation(fontFile.FullName, false);

                return fontInfo;
            }

            throw new ArgumentException($"Font: {family} {style} is not found.");
        }
    }
}
