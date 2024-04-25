using PCPDFengineCore.Interfaces;
using PCPDFengineCore.Models;
using PCPDFengineCore.Models.RecordReaderOptions;

namespace PCPDFengineCore.RecordReader
{
    internal class TextDelimitedRecordReader : IRecordReader
    {
        private TextReaderOptions _options;

        public TextDelimitedRecordReader(TextReaderOptions options)
        {
            _options = options;
        }

        public List<Record> LoadRecordsFromFile(string filename)
        {
            throw new NotImplementedException();
        }

        //public List<Record> LoadRecordsFromFile(string filename)
        //{
        //    List<Record> records = new List<Record>();
        //    using (TextFieldParser parser = new TextFieldParser(filename))
        //    {
        //        parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
        //        parser.SetDelimiters(",");
        //        while (!parser.EndOfData)
        //        {
        //            //Processing row
        //            string[] fields = parser.ReadFields();
        //            foreach (string field in fields)
        //            {
        //                //TODO: Process field
        //            }
        //        }
        //    }

        //    return records;
        //}
    }
}
