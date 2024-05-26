using PCPDFengineCore.RecordReader;
using PCPDFengineCore.RecordReader.RecordReaderOptions;
using PCPDFengineCoreTests;

namespace PCPDFengineCore.Composition.Tests
{
    [TestClass()]
    public class PdfControllerTests
    {
        [TestMethod()]
        public void ComposePdfTest()
        {
            TextDelimitedRecordReaderOptions options = new TextDelimitedRecordReaderOptions(1, new Field(PCPDFengineCore.Models.Enums.FieldType.STRING, "Header 0", "HEAD"),
            new TextDelimitedDataField[]
            {
                            new TextDelimitedDataField("Header 0", PCPDFengineCore.Models.Enums.FieldType.STRING),
                            new TextDelimitedDataField("Header 1", PCPDFengineCore.Models.Enums.FieldType.STRING),
                            new TextDelimitedDataField("Header 2", PCPDFengineCore.Models.Enums.FieldType.INT),
                            new TextDelimitedDataField("Header 3", PCPDFengineCore.Models.Enums.FieldType.BOOLEAN),
                            new TextDelimitedDataField("Header 4", PCPDFengineCore.Models.Enums.FieldType.STRING),
                            new TextDelimitedDataField("Header 5", PCPDFengineCore.Models.Enums.FieldType.STRING)
            }.ToList());

            TextDelimitedRecordReader reader = new TextDelimitedRecordReader(options);
            List<Record> results = reader.LoadRecordsFromFile(TestResources.DataFiles.DELIMITED_CSV);

            PdfController pdfController = new PdfController();

            DocumentCollection documentCollection = new DocumentCollection();
            Document document = new Document("test");
            document.Add(new Page("testPage"));
            documentCollection.Add(document);

            pdfController.ComposePdf(results, documentCollection, TestResources.TEST_OUT_PDF);

            Assert.IsTrue(File.Exists(TestResources.TEST_OUT_PDF));
        }
    }
}