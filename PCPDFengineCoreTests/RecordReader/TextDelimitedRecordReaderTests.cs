using PCPDFengineCore.Models;
using PCPDFengineCore.Models.RecordReaderOptions;
using PCPDFengineCore.RecordReader;

namespace PCPDFengineCoreTests.RecordReader.Tests
{
    [TestClass()]
    public class TextDelimitedRecordReaderTests
    {
        private TextDelimitedRecordReaderOptions _options;
        private List<Record> results = new List<Record>();

        public TextDelimitedRecordReaderTests()
        {
            _options = new TextDelimitedRecordReaderOptions(1, new Field(PCPDFengineCore.Models.Enums.FieldType.STRING, "Header 0", "HEAD"),
            new TextDelimitedDataField[]
            {
                new TextDelimitedDataField("Header 0", PCPDFengineCore.Models.Enums.FieldType.STRING),
                new TextDelimitedDataField("Header 1", PCPDFengineCore.Models.Enums.FieldType.STRING),
                new TextDelimitedDataField("Header 2", PCPDFengineCore.Models.Enums.FieldType.INT),
                new TextDelimitedDataField("Header 3", PCPDFengineCore.Models.Enums.FieldType.BOOLEAN),
                new TextDelimitedDataField("Header 4", PCPDFengineCore.Models.Enums.FieldType.STRING),
                new TextDelimitedDataField("Header 5", PCPDFengineCore.Models.Enums.FieldType.STRING)
            });

            TextDelimitedRecordReader reader = new TextDelimitedRecordReader(_options);
            results = reader.LoadRecordsFromFile(TestResources.DataFiles.DELIMITED_CSV);
        }

        [TestMethod()]
        public void LoadRecordsFromFixedWidthReadStringFileTest()
        {
            Assert.AreEqual("test 1", results[0].Sections.First().GetField("Header 1").ConvertToActualType<string>());
        }
        [TestMethod()]
        public void LoadRecordsFromFixedWidthIntTest()
        {
            Assert.AreEqual(1, results[0].Sections.First().GetField("Header 2").ConvertToActualType<int>());
        }
        [TestMethod()]
        public void LoadRecordsFromFixedWidthFileBooleanTest()
        {
            Assert.AreEqual(true, results[0].Sections.First().GetField("Header 3").ConvertToActualType<bool>());    // TRUE
            Assert.AreEqual(true, results[1].Sections.First().GetField("Header 3").ConvertToActualType<bool>());    // yes
            Assert.AreEqual(true, results[2].Sections.First().GetField("Header 3").ConvertToActualType<bool>());    // 1
            Assert.AreEqual(false, results[3].Sections.First().GetField("Header 3").ConvertToActualType<bool>());   // false
            Assert.AreEqual(false, results[4].Sections.First().GetField("Header 3").ConvertToActualType<bool>());   // 0 
            Assert.AreEqual(false, results[5].Sections.First().GetField("Header 3").ConvertToActualType<bool>());   // no
        }
    }
}