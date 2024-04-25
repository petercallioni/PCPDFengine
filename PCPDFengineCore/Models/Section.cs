namespace PCPDFengineCore.Models
{
    public class Section
    {
        private string _name;
        private List<Field> _fields;

        public List<Field> Fields { get => _fields; }
        public string Name { get => _name; }

        public Section(string sectionName)
        {
            _name = sectionName;
            _fields = new List<Field>();
        }

        public Field GetField(string fieldName)
        {
            Field? field = _fields.Where(x => x.Name == fieldName).FirstOrDefault();

            if (field == null)
            {
                throw new NullReferenceException($"Field named {fieldName} does not exist.");
            }
            else
            {
                return field;
            }
        }
    }
}
