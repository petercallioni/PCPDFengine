using PCPDFengineCore.Fonts;
using PCPDFengineCore.Interfaces;

namespace PCPDFengineCore.Persistence
{
    /// <summary>
    /// Information that needs to be persisted goes here.
    /// </summary>
    public class PersistanceState
    {
        private FileInformation? fileInformation;
        private IRecordReader? recordReader;
        private List<FontInfo> embeddedFonts;
        private bool embedFonts;

        public PersistanceState()
        {
            fileInformation = null;
            recordReader = null;
            embeddedFonts = new List<FontInfo>();
            embedFonts = true;
        }

        public FileInformation? FileInformation { get => fileInformation; set => fileInformation = value; }

        public IRecordReader? RecordReader { get => recordReader; set => recordReader = value; }
        public List<FontInfo> EmbeddedFonts { get => embeddedFonts; set => embeddedFonts = value; }
        public bool EmbedFonts { get => embedFonts; set => embedFonts = value; }
    }
}
