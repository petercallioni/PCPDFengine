using PCPDFengineCore.Models.Enums;

namespace PCPDFengineCore.Models
{
    public class TextDelimitedDataField
    {
        private readonly string _name;
        private readonly FieldType _fieldType;
        public TextDelimitedDataField(string name, FieldType fieldType)
        {
            _name = name;
            _fieldType = fieldType;
        }

        public string Name => _name;

        public FieldType FieldType => _fieldType;
    }
}
