using PCPDFengineCore.Interfaces;

namespace PCPDFengineCore.Persistence
{
    public class PersistanceState
    {
        private FileInformation _fileInformation;
        private IRecordReader _recordReader;

        public FileInformation FileInformation { get => _fileInformation; set => _fileInformation = value; }

        public IRecordReader RecordReader { get => _recordReader; set => _recordReader = value; }
    }
}
