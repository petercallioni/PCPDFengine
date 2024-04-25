using PCPDFengineCore.Models;

namespace PCPDFengineCore.Interfaces
{
    public interface IRecordReader
    {
        public List<Record> LoadRecordsFromFile(string filename);
    }
}
