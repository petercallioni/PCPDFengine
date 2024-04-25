using PCPDFengineCore.Models.Enums;
using System.Text.RegularExpressions;

namespace PCPDFengineCore.Models
{
    public class Field
    {
        private string _name;
        private FieldType _type;
        private object _value;
        private bool _isHeader;
        public FieldType Type { get => _type; }
        public object Value { get => _value; }
        public string Name { get => _name; }

        public Field(FieldType type, string name, string? value)
        {
            _type = type;
            _name = name;

            switch (_type)
            {
                case FieldType.STRING:
                    _value = value;
                    break;
                case FieldType.INT:
                    {
                        int convertedValue;
                        bool success = int.TryParse(value, out convertedValue);
                        _value = success ? convertedValue : "NaN";
                    }
                    break;
                case FieldType.BIG_INT:
                    {
                        long convertedValue;
                        bool success = long.TryParse(value, out convertedValue);
                        _value = success ? convertedValue : "NaN";
                    }
                    break;
                case FieldType.DOUBLE:
                    {
                        double convertedValue;
                        bool success = double.TryParse(value, out convertedValue);
                        _value = success ? convertedValue : "NaN";
                    }
                    break;
                case FieldType.BOOLEAN:
                    {
                        string pattern = @"^(true|yes|y|[1-9]+)$";
                        RegexOptions options = RegexOptions.IgnoreCase;

                        _value = Regex.IsMatch(value, pattern, options);
                    }
                    break;
                case FieldType.INSERT_IMAGE:
                case FieldType.INSERT_PDF:
                    _value = new FileInfo(value);
                    break;
                default:
                    throw new NotImplementedException($"The data type ${_type} is not implemented yet");
            }
        }
    }
    public static class FieldExtensions
    {
        public static T ConvertToActualType<T>(this Field field)
        {
            Dictionary<FieldType, Type> typeMap = new Dictionary<FieldType, Type>();
            typeMap.Add(FieldType.STRING, typeof(string));
            typeMap.Add(FieldType.INT, typeof(int));
            typeMap.Add(FieldType.BIG_INT, typeof(long));
            typeMap.Add(FieldType.DOUBLE, typeof(double));
            typeMap.Add(FieldType.BOOLEAN, typeof(bool));
            typeMap.Add(FieldType.INSERT_PDF, typeof(FileInfo));
            typeMap.Add(FieldType.INSERT_IMAGE, typeof(FileInfo));

            bool fieldMapped = typeMap.TryGetValue(field.Type, out Type? actualType);
            if (fieldMapped)
            {
                if (typeof(T) != actualType)
                {
                    throw new InvalidCastException($"Casting FieldType {field.Type.ToString()} to {typeof(T).ToString()} is invalid.");
                }

                T convertedValue = (T)field.Value;
                return convertedValue;
            }
            else
            {
                throw new NotImplementedException($"The data type {field.Type} is not implemented yet");
            }
        }
    }
}
