namespace PCPDFengineCore.Models.RecordReaderOptions
{
    public class TextDelimitedRecordReaderOptions : TextReaderOptions
    {
        private List<TextDelimitedDataField> _fields;
        private string _delimiter = ",";
        private string _quote = "\"";

        public TextDelimitedRecordReaderOptions(int headerLines, Field? recordHeader = null, IEnumerable<TextDelimitedDataField>? fields = null) : base(headerLines, recordHeader)
        {
            _fields = new List<TextDelimitedDataField>();

            if (fields != null)
            {
                _fields.AddRange(fields);
            }
        }

        public List<TextDelimitedDataField> Fields
        {
            get => _fields;
        }
        public string Delimiter { get => _delimiter; set => _delimiter = value; }
        public string Quote { get => _quote; set => _quote = value; }
    }
}
