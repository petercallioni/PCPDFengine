using PCPDFengineCore.Models.Enums;
using System.Text.RegularExpressions;

namespace PCPDFengineCore.RecordReader
{
    public class Field
    {
        private string name;
        private FieldType type;
        private object? value;
        public FieldType Type { get => type; }
        public object? Value { get => value; }
        public string Name { get => name; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Field() { }  // Required for json serialisation.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Field(FieldType type, string name, string? value)
        {
            this.type = type;
            this.name = name;

            if (value != null)
            {
                switch (this.type)
                {
                    case FieldType.STRING:
                        this.value = value;
                        break;
                    case FieldType.INT:
                        {
                            int convertedValue;
                            bool success = int.TryParse(value, out convertedValue);
                            this.value = success ? convertedValue : "NaN";
                        }
                        break;
                    case FieldType.BIG_INT:
                        {
                            long convertedValue;
                            bool success = long.TryParse(value, out convertedValue);
                            this.value = success ? convertedValue : "NaN";
                        }
                        break;
                    case FieldType.DOUBLE:
                        {
                            double convertedValue;
                            bool success = double.TryParse(value, out convertedValue);
                            this.value = success ? convertedValue : "NaN";
                        }
                        break;
                    case FieldType.BOOLEAN:
                        {
                            string pattern = @"^(true|yes|y|[1-9]+)$";
                            RegexOptions options = RegexOptions.IgnoreCase;

                            this.value = Regex.IsMatch(value, pattern, options);
                        }
                        break;
                    case FieldType.INSERT_IMAGE:
                    case FieldType.INSERT_PDF:
                        this.value = new FileInfo(value);
                        break;
                    default:
                        throw new NotImplementedException($"The data type ${this.type} is not implemented yet");
                }
            }
            else
            {
                this.value = value;
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

                return field.Value != null ? (T)field.Value : throw new NullReferenceException("Tried to convert a value that is null.");
            }
            else
            {
                throw new NotImplementedException($"The data type {field.Type} is not implemented yet");
            }
        }
    }
}
