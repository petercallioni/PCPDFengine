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

            if (GetSection(sectionName) != null)
            {
                throw new ArgumentException($"Section named ${sectionName} already exists.");
            }

            _sections.Add(section);

            return section;
        }

        public long Id { get => _id; set => _id = value; }

        public List<Section> Sections { get => _sections; set => _sections = value; }
        /// <summary>
        /// Gets the default section.
        /// </summary>
        public Section GetSection()
        {
            return GetSection(DEFAULT_SECTION_NANE);
        }
        public Section GetSection(Section section)
        {
            return GetSection(section.Name);
        }
        public Section GetSection(string sectionName)
        {
            Section? section = _sections.Where(x => x.Name == sectionName).FirstOrDefault();
            return section;
        }
    }
}
