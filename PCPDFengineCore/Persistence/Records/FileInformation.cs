namespace PCPDFengineCore.Persistence.Records
{
    public class FileInformation : BaseRecord
    {
        public string DatabaseVersion { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
    }
}
