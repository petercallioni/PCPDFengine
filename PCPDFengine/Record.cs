namespace PCPDFengineCore.RecordReader
{
    internal class Record
    {
        private long id;
        private List<Section> sections;

        public Record(long id, List<Section> sections)
        {
            this.id = id;
            this.sections = sections;
        }

        public Record()
        {
            this.id = 1;
            this.sections = new List<Section>();
        }

        public long Id { get => id; set => id = value; }
        internal List<Section> Sections { get => sections; set => sections = value; }
    }

    internal class Section
    {
    }
}
