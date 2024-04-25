namespace PCPDFengineCore.Models
{
    public class Record
    {
        private long _id;
        private List<Section> _sections;
        public const string DEFAULT_SECTION_NANE = "Main";

        public Record(long id, List<Section> sections)
        {
            this._id = id;
            this._sections = sections;
        }

        public Record(string sectionName = DEFAULT_SECTION_NANE)
        {
            _id = 1;
            _sections = new List<Section>();
            AddSection(sectionName);
        }

        public bool HasContent()
        {
            foreach (Section section in _sections)
            {
                if (section.Fields.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public Section AddSection(string sectionName)
        {
            Section section = new Section(sectionName);

            _sections.Add(section);

            return section;
        }

        public long Id { get => _id; set => _id = value; }

        public List<Section> Sections { get => _sections; set => _sections = value; }
    }
}
