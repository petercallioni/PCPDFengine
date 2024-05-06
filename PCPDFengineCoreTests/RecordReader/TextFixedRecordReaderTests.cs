using PCPDFengineCore.Models;
using PCPDFengineCore.Models.RecordReaderOptions;
using PCPDFengineCore.RecordReader;

namespace PCPDFengineCoreTests.RecordReader.Tests
{
    [TestClass()]
    public class TextFixedRecordReaderTests
    {
        private TextFixedRecordReaderOptions options;
        private List<Record> results = new List<Record>();

        public TextFixedRecordReaderTests()
        {
            options = new TextFixedRecordReaderOptions(1, true, new Field(PCPDFengineCore.Models.Enums.FieldType.STRING, "Header 0", "HEAD"),
            new TextFixedWidthDataField[]
            {
                new TextFixedWidthDataField("Header 0", 5, FixedWidthAligment.LEFT, PCPDFengineCore.Models.Enums.FieldType.STRING),
                new TextFixedWidthDataField("Header 1", 10, FixedWidthAligment.LEFT, PCPDFengineCore.Models.Enums.FieldType.STRING),
                new TextFixedWidthDataField("Header 2", 10, FixedWidthAligment.LEFT, PCPDFengineCore.Models.Enums.FieldType.INT),
                new TextFixedWidthDataField("Header 3", 20, FixedWidthAligment.LEFT, PCPDFengineCore.Models.Enums.FieldType.BOOLEAN),
                new TextFixedWidthDataField("Header 4", 10, FixedWidthAligment.LEFT, PCPDFengineCore.Models.Enums.FieldType.STRING),
                new TextFixedWidthDataField("Header 5", 10, FixedWidthAligment.RIGHT, PCPDFengineCore.Models.Enums.FieldType.STRING)
            }.ToList());

            TextFixedRecordReader reader = new TextFixedRecordReader(options);
            results = reader.LoadRecordsFromFile(TestResources.DataFiles.FIXED_WIDTH);
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