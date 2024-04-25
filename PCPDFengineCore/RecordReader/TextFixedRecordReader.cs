using PCPDFengineCore.Interfaces;
using PCPDFengineCore.Models;
using PCPDFengineCore.Models.RecordReaderOptions;

namespace PCPDFengineCore.RecordReader
{
    public class TextFixedRecordReader : IRecordReader
    {
        private TextFixedRecordReaderOptions _options;

        public TextFixedRecordReader(TextFixedRecordReaderOptions options)
        {
            _options = options;
        }

        public List<Record> LoadRecordsFromFile(string filename)
        {
            List<Record> records = new List<Record>();

            FileInfo file = new FileInfo(filename);

            bool hasSections = false;
            Record? record = null;
            Section? section = null;

            using (StreamReader fileStream = file.OpenText())
            {
                string? line;
                long fileLine = 0;

                while ((line = fileStream.ReadLine()) != null)
                {
                    int cursor = 0;
                    fileLine++;

                    // Skip the header lines.
                    if (fileLine <= _options.HeaderLines)
                    {
                        continue;
                    }

                    // There is no primary section identifier, assume each row is a new record.
                    if (_options.SectionIdentifiers.First() == null)
                    {
                        if (record != null)
                        {
                            records.Add(record);
                        }
                        record = new Record();
                        section = record.GetSection();
                    }
                    else
                    {
                        hasSections = true;
                    }

                    List<Field> extractedFields = new List<Field>();

                    foreach (TextDataField field in _options.Fields)
                    {
                        string columnValue = line.Substring(cursor, field.Size);
                        if (_options.Trim)
                        {
                            columnValue = columnValue.Trim();
                        }

                        cursor += field.Size;
                        Field extractedField = new Field(field.FieldType, columnValue, field.Name);

                        if (hasSections)
                        {
                            for (int i = _options.SectionIdentifiers.Count - 1; i >= 0; i--)
                            {
                                if (field.Name.Equals(extractedField.Name)
                                    && _options.SectionIdentifiers[i].Value.Equals(extractedField.Value))
                                {
                                    if (i == 0) // Is a the header section
                                    {
                                        string sectionValue = extractedField.Value.ToString();
                                        if (record != null)
                                        {
                                            records.Add(record);
                                        }

                                        record = new Record(sectionValue);
                                        section = record.GetSection(sectionValue);
                                    }
                                    else
                                    {
                                        section = record.AddSection(_options.SectionIdentifiers[i].Value.ToString());
                                    }

                                    break;
                                }
                            }
                        }

                        record.GetSection(section).Fields.Add(extractedField);
                    }
                }

            }

            if (record.HasContent())
            {
                records.Add(record);
            }

            return records;
        }
    }
}
