using PCPDFengineCore.Fonts;
using PCPDFengineCore.Interfaces;

namespace PCPDFengineCore.Persistence
{
    public class PersistanceState
    {
        private FileInformation? fileInformation;
        private IRecordReader? recordReader;
        private Dictionary<string, List<FontInfo>> addedFonts;

        public PersistanceState()
        {
            fileInformation = null;
            recordReader = null;
            addedFonts = new Dictionary<string, List<FontInfo>>();
        }

        public FileInformation? FileInformation { get => fileInformation; set => fileInformation = value; }

        public IRecordReader? RecordReader { get => recordReader; set => recordReader = value; }
        public Dictionary<string, List<FontInfo>> AddedFonts { get => addedFonts; set => addedFonts = value; }
    }
}
