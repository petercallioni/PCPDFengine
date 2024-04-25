using PCPDFengineCore.Models.Enums;
using static PCPDFengineCore.Models.RecordReaderOptions.TextFixedRecordReaderOptions;

namespace PCPDFengineCore.Models
{
    public class TextDataField
    {
        private readonly string _name;
        private readonly int _size;
        private readonly FixedWidthAligment _alignment;
        private readonly FieldType _fieldType;
        public TextDataField(string name, int size, FixedWidthAligment aligment, FieldType fieldType)
        {
            _name = name;
            _size = size;
            _alignment = aligment;
            _fieldType = fieldType;
        }

        public FixedWidthAligment Alignment => _alignment;
        public int Size => _size;
        public string Name => _name;

        public FieldType FieldType => _fieldType;
    }
}
