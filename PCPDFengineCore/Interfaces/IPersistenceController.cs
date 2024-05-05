using PCPDFengineCore.Persistence.Records;

namespace PCPDFengineCore.Interfaces
{
    public interface IPersistenceController
    {
        void LoadDatabase(string dataBasePath, bool clearExisting = false);
        void SaveDatabase();
        void UpdateFileInformation(FileInformation fileInformation);
        FileInformation? GetFileInformation();
        void CloseDatabase();
    }
}