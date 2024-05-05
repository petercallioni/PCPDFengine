namespace PCPDFengineCore.Persistence.Records
{
    public class FileInformation : BaseRecord
    {
        public int DatabaseVersionMajor { get; set; }
        public int DatabaseVersionMinor { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
    }
}
