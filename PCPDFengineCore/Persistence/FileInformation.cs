namespace PCPDFengineCore.Persistence
{
    public class FileInformation
    {
        public string DatabaseVersion { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }

        public FileInformation()
        {
            DatabaseVersion = DatabaseInformation.Version;
        }
        public FileInformation(string databaseVersion, DateTime timeCreated, DateTime timeUpdated)
        {
            DatabaseVersion = databaseVersion;
            TimeCreated = timeCreated;
            TimeUpdated = timeUpdated;
        }
    }
}
