using PCPDFengineCore.RecordReader;

namespace PCPDFengineCore.RecordReader.RecordReaderOptions
{
    public class TextDelimitedRecordReaderOptions
    {
        private List<TextDelimitedDataField> fields;
        private string delimiter = ",";
        private string quote = "\"";
        private int headerLines;
        private List<Field?> sectionIdentifiers;
        private Field? recordHeader;

        public TextDelimitedRecordReaderOptions(int headerLines, Field? recordHeader = null, List<TextDelimitedDataField>? fields = null)
        {
            this.fields = new List<TextDelimitedDataField>();

            if (fields != null)
            {
                Fields.AddRange(fields);
            }

            HeaderLines = headerLines;
            sectionIdentifiers = new List<Field?>();
            sectionIdentifiers.Add(recordHeader);
        }

        public List<TextDelimitedDataField> Fields
        {
            get => fields;
        }
        public string Delimiter { get => delimiter; set => delimiter = value; }
        public string Quote { get => quote; set => quote = value; }
        public Field? RecordHeader { get => recordHeader; }
        public List<Field?> SectionIdentifiers { get => sectionIdentifiers; }
        public int HeaderLines { get => headerLines; set => headerLines = value; }
    }
}
