namespace PCPDFengineCore.Models.RecordReaderOptions
{
    public partial class TextFixedRecordReaderOptions : TextReaderOptions
    {
        private List<TextFixedWidthDataField> _fields;
        private bool _trim = false;

        public TextFixedRecordReaderOptions(int headerLines, bool trim = true, Field? recordHeader = null, IEnumerable<TextFixedWidthDataField>? fields = null) : base(headerLines, recordHeader)
        {
            _fields = new List<TextFixedWidthDataField>();
            _trim = trim;

            if (fields != null)
            {
                _fields.AddRange(fields);
            }
        }

        public bool Trim { get => _trim; set => _trim = value; }
        public List<TextFixedWidthDataField> Fields
        {
            get => _fields;
        }
    }
}
