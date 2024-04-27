namespace PCPDFengineCore.Models.RecordReaderOptions
{
    public partial class TextFixedRecordReaderOptions : TextReaderOptions
    {
        private List<TextDataField> _fields;
        private bool _trim = false;

        public TextFixedRecordReaderOptions(int headerLines, bool trim = true, Field? recordHeader = null, IEnumerable<TextDataField>? fields = null) : base(headerLines, recordHeader)
        {
            _fields = new List<TextDataField>();
            _trim = trim;

            if (fields != null)
            {
                _fields.AddRange(fields);
            }
        }

        public bool Trim { get => _trim; set => _trim = value; }
        public List<TextDataField> Fields
        {
            get => _fields;
        }
    }
}
