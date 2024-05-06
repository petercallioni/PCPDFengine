namespace PCPDFengineCore.Models.RecordReaderOptions
{
    public partial class TextFixedRecordReaderOptions
    {
        private List<TextFixedWidthDataField> fields;
        private bool trim = false;
        private int headerLines;
        private List<Field?> sectionIdentifiers;
        private Field? recordHeader;

        public TextFixedRecordReaderOptions(int headerLines, bool trim = true, Field? recordHeader = null, List<TextFixedWidthDataField>? fields = null)
        {
            this.fields = new List<TextFixedWidthDataField>();
            this.trim = trim;
            this.recordHeader = recordHeader;
            if (fields != null)
            {
                Fields.AddRange(fields);
            }

            this.HeaderLines = headerLines;
            this.sectionIdentifiers = new List<Field?>();
            sectionIdentifiers.Add(recordHeader);
        }

        public Field? RecordHeader { get => recordHeader; }
        public List<Field?> SectionIdentifiers { get => sectionIdentifiers; }
        public int HeaderLines { get => headerLines; set => headerLines = value; }
        public bool Trim { get => trim; set => trim = value; }
        public List<TextFixedWidthDataField> Fields { get => fields; }

        public void AddSectionIdentifier(Field field)
        {
            if (SectionIdentifiers.First() == null)
            {
                throw new ArgumentException("To have multiple sections, the first Field in SectionIdentifiers must not be null.");
            }
            else
            {
                SectionIdentifiers.Add(field);
            }
        }
    }
}
