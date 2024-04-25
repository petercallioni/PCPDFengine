namespace PCPDFengineCore.Models.RecordReaderOptions
{
    public class TextReaderOptions
    {
        private int _headerLines;
        private List<Field?> _sectionIdentifiers;

        public TextReaderOptions(int headerLines = 0, Field? recordHeader = null)
        {
            _headerLines = headerLines;
            _sectionIdentifiers = new List<Field?>();
            SectionIdentifiers.Add(recordHeader);
        }

        public int HeaderLines { get => _headerLines; set => _headerLines = value; }
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

        public List<Field?> SectionIdentifiers { get => _sectionIdentifiers; }
    }
}
