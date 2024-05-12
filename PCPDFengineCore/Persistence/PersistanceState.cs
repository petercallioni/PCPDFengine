using PCPDFengineCore.Interfaces;

namespace PCPDFengineCore.Persistence
{
    public class PersistanceState
    {
        private FileInformation fileInformation;
        private IRecordReader recordReader;
        private Dictionary<string, List<byte[]>> addedFonts;

        public FileInformation FileInformation { get => fileInformation; set => fileInformation = value; }

        public IRecordReader RecordReader { get => recordReader; set => recordReader = value; }
        public Dictionary<string, List<byte[]>> AddedFonts { get => addedFonts; set => addedFonts = value; }
    }
}
