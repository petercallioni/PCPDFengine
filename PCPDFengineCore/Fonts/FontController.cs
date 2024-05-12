using FreeTypeSharp;
using System.Runtime.InteropServices;
using static FreeTypeSharp.FT;

namespace PCPDFengineCore.Fonts
{
    public partial class FontController
    {

        public FontInfo GetFontInformation(string ttfFilePath)
        {
            string family = "";
            string style = "";
            byte[] bytes = File.ReadAllBytes(ttfFilePath);

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

        public Dictionary<string, List<FontInfo>> GetInstalledTtfFonts()
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

            return fonts;
        }
    }
}
