using PCPDFengineCore.Interfaces;
using PCPDFengineCore.RecordReader.RecordReaderOptions;

namespace PCPDFengineCore.RecordReader
{
    public class TextFixedRecordReader : IRecordReader
    {
        private TextFixedRecordReaderOptions options;
        private string classTypeString;

        public TextFixedRecordReader(TextFixedRecordReaderOptions options, string classTypeString = "")
        {
            this.options = options;
            this.classTypeString = this.GetType().ToString();
        }

        public TextFixedRecordReaderOptions Options { get => options; }
        public string ClassTypeString { get => classTypeString; }

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
                    if (fileLine <= Options.HeaderLines)
                    {
                        continue;
                    }

                    // There is no primary section identifier, assume each row is a new record.
                    if (Options.SectionIdentifiers.First() == null)
                    {
                        if (record != null)
                        {
                            records.Add(record);
                        }
                        record = new Record();
                        section = record.Sections.First();
                    }
                    else
                    {
                        hasSections = true;
                    }

                    List<Field> extractedFields = new List<Field>();


                    // Fixed width specific -----------------------------
                    foreach (TextFixedWidthDataField field in Options.Fields)
                    {
                        string columnValue = line.Substring(cursor, field.Size);
                        if (Options.Trim)
                        {
                            columnValue = columnValue.Trim();
                        }

                        cursor += field.Size;
                        Field extractedField = new Field(field.FieldType, field.Name, columnValue);
                        extractedFields.Add(extractedField);
                    }
                    // -----------------------------

                    if (hasSections)
                    {
                        for (int i = 0; i < Options.SectionIdentifiers.Count; i++)
                        {
                            Field? headerField = null;
                            // On the header section
                            if (i == 0)
                            {
                                headerField = extractedFields.Where(x => x.Name == Options.SectionIdentifiers[i]!.Name
                                && (x.Value!.Equals(Options.SectionIdentifiers[i]!.Value))).FirstOrDefault();

                                if (headerField != null)
                                {
                                    string sectionValue = headerField.Value!.ToString()!;
                                    if (record != null)
                                    {
                                        records.Add(record);
                                    }

                                    record = new Record(sectionValue);
                                    section = record.Sections.First();
                                    break;
                                }
                            }
                            else
                            {
                                headerField = extractedFields.Where(x => x.Name == Options.SectionIdentifiers[i]!.Name
                                                                && (x.Value!.Equals(Options.SectionIdentifiers[i]!.Value) || Options.SectionIdentifiers[i]!.Value == null)).FirstOrDefault();
                                if (headerField != null)
                                {
                                    section = record!.AddSection(headerField.Value!.ToString()!);
                                    break;
                                }
                            }
                        }
                    }

                    if (section != null)
                    {
                        section.Fields.AddRange(extractedFields);
                    }
                }

            }

            if (record != null && record.HasContent())
            {
                records.Add(record);
            }

            return records;
        }
    }
}
