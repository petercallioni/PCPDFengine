﻿using FreeTypeSharp;
using PCPDFengineCore.Persistence;
using System.Runtime.InteropServices;
using static FreeTypeSharp.FT;

namespace PCPDFengineCore.Fonts
{
    public class FontController
    {
        private Dictionary<string, List<FontInfo>>? installedFonts;

        private PersistenceController? persistenceController;
        private PersistenceController PersistenceController
        {
            get
            {
                if (persistenceController == null)
                {
                    throw new NullReferenceException("PersistenceController called before it is set.");
                }
                return persistenceController;
            }
        }

        public FontController()
        {
            installedFonts = null;
        }

        public void SetPersistenceController(PersistenceController persistenceController)
        {
            this.persistenceController = persistenceController;
        }

        public Dictionary<string, List<FontInfo>> InstalledFonts
        {
            get
            {
                if (installedFonts == null)
                {
                    throw new NullReferenceException("InstalledFonts called before loading installed fonts.");
                }
                return installedFonts;
            }
        }

        public FontInfo GetFontInfo(string fontName, string style)
        {
            if (PersistenceController.State != null)
            {
                List<FontInfo> embeddedFont = PersistenceController.State.EmbeddedFonts.Where(x => x.Bytes != null
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

        private FontInfo GetFontInformation(string ttfFilePath, bool? loadFontBytesOverride = null)
        {
            string family = "";
            string style = "";

            bool loadBytes = loadFontBytesOverride ?? PersistenceController.State.EmbedFonts;

            byte[]? bytes = loadBytes ? File.ReadAllBytes(ttfFilePath) : null;

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

            FontInfo font = new FontInfo(family, style, new FileInfo(ttfFilePath).Name);
            font.Bytes = bytes;
            return font;
        }

        public byte[] GetFont(string family, string style)
        {
            installedFonts!.TryGetValue(family, out List<FontInfo>? installedStyles);

            FontInfo? embeddedFont = persistenceController?.State.EmbeddedFonts.Where(x => x.Family == family && x.Style == style && x.Bytes != null).ToList().FirstOrDefault();

            if (embeddedFont != null)
            {
                return embeddedFont.Bytes!;
            }

            FontInfo? installedFont = installedStyles?.Where(x => x.Style == style).FirstOrDefault();

            if (installedFont != null)
            {
                return installedFont.Bytes!;
            }

            throw new ArgumentException($"Font: {family} {style} is not found.");
        }

        public void LoadInstalledTtfFonts()
        {
            string fontsfolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

            DirectoryInfo installedFontsFolder = new DirectoryInfo(fontsfolder);
            Dictionary<string, List<FontInfo>> fonts = new Dictionary<string, List<FontInfo>>();

            foreach (FileInfo fontFile in installedFontsFolder.EnumerateFiles().Where(x => x.Name.ToUpper().EndsWith(".TTF")))
            {
                FontInfo fontInfo = GetFontInformation(fontFile.FullName);
                bool exists = fonts.TryGetValue(fontInfo.Family, out List<FontInfo>? list);

                if (!exists)
                {
                    list = new List<FontInfo>();
                    fonts.Add(fontInfo.Family, list);
                }

                list!.Add(fontInfo);
            }

            installedFonts = fonts;
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

        public void AddFontToState(string familyString, string style)
        {
            InstalledFonts.TryGetValue(familyString, out List<FontInfo>? family);

            if (family != null)
            {
                foreach (FontInfo? font in family.Where(x => x.Style == style))
                {
                    PersistenceController.State.EmbeddedFonts.Add(font);
                }
            }
        }

        public void RemoveFontFromState(string family, string style)
        {
            PersistenceController.State.EmbeddedFonts.RemoveAll(x => x.Family == family && x.Style == style);
        }

        public void SetEmbedFonts(bool embedFonts)
        {
            if (PersistenceController.State.EmbedFonts != embedFonts)
            {
                PersistenceController.State.EmbedFonts = embedFonts;

                LoadInstalledTtfFonts();
            }
        }
    }
}
