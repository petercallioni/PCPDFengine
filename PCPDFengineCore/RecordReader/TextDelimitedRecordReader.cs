using CsvHelper;
using PCPDFengineCore.Interfaces;
using PCPDFengineCore.Models;
using PCPDFengineCore.Models.RecordReaderOptions;
using System.Globalization;

namespace PCPDFengineCore.RecordReader
{
    public class TextDelimitedRecordReader : IRecordReader
    {
        private TextDelimitedRecordReaderOptions options;
        private string classTypeString;

        public TextDelimitedRecordReader(TextDelimitedRecordReaderOptions options, string classTypeString = "")
        {
            this.options = options;
            this.classTypeString = this.GetType().ToString();
        }

        public TextDelimitedRecordReaderOptions Options { get => options; set => options = value; }
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
                using (CsvReader csv = new CsvReader(fileStream, CultureInfo.InvariantCulture))
                {
                    while (csv.Read())
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

                        // Delimited specific -----------------------------
                        string[] fields = csv.Parser.Record;

                        foreach (TextDelimitedDataField field in Options.Fields)
                        {
                            Field extractedField = new Field(field.FieldType, field.Name, fields[cursor]);
                            extractedFields.Add(extractedField);
                            cursor++;
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
            }

            if (record != null && record.HasContent())
            {
                records.Add(record);
            }

            return records;
        }
    }
}

