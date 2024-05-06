using PCPDFengineCore.RecordReader;

namespace PCPDFengineCore.Interfaces
{
    public interface IRecordReader
    {
        public List<Record> LoadRecordsFromFile(string filename);
    }
}
