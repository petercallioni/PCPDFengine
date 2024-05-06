using PCPDFengineCore.Models.Enums;
using PCPDFengineCore.Models.RecordReaderOptions;

namespace PCPDFengineCore.Models
{
    public class TextFixedWidthDataField
    {
        private readonly string name;
        private readonly int size;
        private readonly FixedWidthAligment alignment;
        private readonly FieldType fieldType;

        public TextFixedWidthDataField(string name, int size, FixedWidthAligment alignment, FieldType fieldType)
        {
            this.name = name;
            this.size = size;
            this.alignment = alignment;
            this.fieldType = fieldType;
        }

        public FixedWidthAligment Alignment => alignment;
        public int Size => size;
        public string Name => name;

        public FieldType FieldType => fieldType;
    }
}
