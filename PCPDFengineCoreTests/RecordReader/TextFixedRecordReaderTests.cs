using PCPDFengineCore.Models;
using PCPDFengineCore.Models.RecordReaderOptions;
using PCPDFengineCore.RecordReader;
using PCPDFengineCoreTests.Extensions;
using System.Diagnostics;

namespace PCPDFengineCoreTests.RecordReader
{
    [TestClass()]
    public class TextFixedRecordReaderTests
    {
        [TestMethod()]
        public void LoadRecordsFromFixedWidthFileTest()
        {
            TextFixedRecordReaderOptions options = new TextFixedRecordReaderOptions(1, recordHeader: new Field(PCPDFengineCore.Models.Enums.FieldType.STRING, "Header 0", "HEAD"));
            options.SectionIdentifiers.Add(new Field(PCPDFengineCore.Models.Enums.FieldType.STRING, "Header 0", null));

            options.Fields.Add(new TextDataField("Header 0", 5, TextFixedRecordReaderOptions.FixedWidthAligment.LEFT, PCPDFengineCore.Models.Enums.FieldType.STRING));
            options.Fields.Add(new TextDataField("Header 1", 10, TextFixedRecordReaderOptions.FixedWidthAligment.LEFT, PCPDFengineCore.Models.Enums.FieldType.STRING));
            options.Fields.Add(new TextDataField("Header 2", 10, TextFixedRecordReaderOptions.FixedWidthAligment.LEFT, PCPDFengineCore.Models.Enums.FieldType.INT));
            options.Fields.Add(new TextDataField("Header 3", 20, TextFixedRecordReaderOptions.FixedWidthAligment.LEFT, PCPDFengineCore.Models.Enums.FieldType.BOOLEAN));
            options.Fields.Add(new TextDataField("Header 4", 10, TextFixedRecordReaderOptions.FixedWidthAligment.LEFT, PCPDFengineCore.Models.Enums.FieldType.STRING));
            options.Fields.Add(new TextDataField("Header 5", 10, TextFixedRecordReaderOptions.FixedWidthAligment.RIGHT, PCPDFengineCore.Models.Enums.FieldType.STRING));

            TextFixedRecordReader reader = new TextFixedRecordReader(options);
            List<Record> results = reader.LoadRecordsFromFile("./TestResources/TestDataFiles/FixedWidth.txt");

            // First section is always the header.
            Assert.AreEqual(results[0].Sections.First().GetField("Header 1").ConvertToActualType<string>(), "test 1");
            Assert.AreEqual(results[0].Sections.First().GetField("Header 2").ConvertToActualType<int>(), 1);
            Assert.AreEqual(results[0].Sections.First().GetField("Header 3").ConvertToActualType<bool>(), true);
            Assert.AreEqual(results[1].Sections.First().GetField("Header 3").ConvertToActualType<bool>(), true);
            Assert.AreEqual(results[2].Sections.First().GetField("Header 3").ConvertToActualType<bool>(), true);
            Assert.AreEqual(results[3].Sections.First().GetField("Header 3").ConvertToActualType<bool>(), false);
            Assert.AreEqual(results[4].Sections.First().GetField("Header 3").ConvertToActualType<bool>(), false);
            Assert.AreEqual(results[5].Sections.First().GetField("Header 3").ConvertToActualType<bool>(), false);

            Trace.WriteLine(results.DumpObject());
        }
    }
}