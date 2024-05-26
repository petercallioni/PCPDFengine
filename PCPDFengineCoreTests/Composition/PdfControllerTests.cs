using PCPDFengineCore.RecordReader;
using PCPDFengineCore.RecordReader.RecordReaderOptions;
using PCPDFengineCoreTests;

namespace PCPDFengineCore.Composition.Tests
{
    [TestClass()]
    public class PdfControllerTests
    {
        private TextDelimitedRecordReaderOptions options;
        private List<Record> results = new List<Record>();

        public PdfControllerTests()
        {
            try
            {
                File.Delete(TestResources.TEST_OUT_PDF);
            }
            catch { }

            options = new TextDelimitedRecordReaderOptions(1, new Field(PCPDFengineCore.Models.Enums.FieldType.STRING, "Header 0", "HEAD"),
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
            results = reader.LoadRecordsFromFile(TestResources.DataFiles.DELIMITED_CSV);
        }

        [TestMethod()]
        public void ComposePdfTest()
        {
            TextDelimitedRecordReader reader = new TextDelimitedRecordReader(options);

            MasterController masterController = new MasterController();
            DocumentCollection documentCollection = new DocumentCollection();
            Document document = new Document("test");
            document.Add(new Page("testPage"));
            documentCollection.Add(document);

            masterController.PdfController.SaveDocumentCollectionToState(documentCollection);
            masterController.PdfController.ComposePdf(results, documentCollection, TestResources.TEST_OUT_PDF);

            masterController.PersistenceController.SaveState(TestResources.TEST_SAVE_FILE);

            Assert.IsTrue(File.Exists(TestResources.TEST_OUT_PDF));
        }

        [TestMethod()]
        public void ComposeSavedPdfTest()
        {
            TextDelimitedRecordReader reader = new TextDelimitedRecordReader(options);

            MasterController masterController = new MasterController();
            DocumentCollection documentCollection = new DocumentCollection();
            Document document = new Document("test");
            document.Add(new Page("testPage"));
            documentCollection.Add(document);

            masterController.PdfController.SaveDocumentCollectionToState(documentCollection);

            masterController.PersistenceController.SaveState(TestResources.TEST_SAVE_FILE);
            masterController.PersistenceController.LoadState(TestResources.TEST_SAVE_FILE);

            Assert.IsTrue(masterController.PersistenceController.State.DocumentCollection.Documents.Count > 0);

            masterController.PdfController.ComposePdf(results, masterController.PersistenceController.State.DocumentCollection, TestResources.TEST_OUT_PDF);

            Assert.IsTrue(File.Exists(TestResources.TEST_OUT_PDF));
        }
    }
}