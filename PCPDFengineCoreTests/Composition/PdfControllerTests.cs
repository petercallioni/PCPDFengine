using PCPDFengineCore.Composition.PageElements;
using PCPDFengineCore.Composition.Units;
using PCPDFengineCore.RecordReader;
using PCPDFengineCore.RecordReader.RecordReaderOptions;
using PCPDFengineCoreTests;
using System.Drawing;

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
        public void PdfPageElementTest()
        {
            TextDelimitedRecordReader reader = new TextDelimitedRecordReader(options);

            MasterController masterController = new MasterController();
            DocumentCollection documentCollection = new DocumentCollection();
            Document document = new Document("test");
            Page page = new Page("testPage");

            PageElements.PageElement element = new PageElements.PageElement();

            element.Name = "testText";
            element.InitialX = new Unit(10, UnitTypes.Centimeter);
            element.InitialY = new Unit(5, UnitTypes.Centimeter);

            page.PageElements.Add(element);

            document.Add(page);
            documentCollection.Add(document);

            masterController.PdfController.SaveDocumentCollectionToState(documentCollection);

            masterController.PersistenceController.SaveState(TestResources.TEST_SAVE_FILE);
            masterController.PersistenceController.LoadState(TestResources.TEST_SAVE_FILE);

            //Assert.AreEqual(10, masterController.PersistenceController.State.DocumentCollection.Documents[0].Pages[0].PageElements[0].InitialX.Value);

            masterController.PdfController.ComposePdf(results, masterController.PersistenceController.State.DocumentCollection, TestResources.TEST_OUT_PDF);

            Assert.IsTrue(File.Exists(TestResources.TEST_OUT_PDF));
        }

        [TestMethod()]
        public void PdfPageLineTest()
        {
            TextDelimitedRecordReader reader = new TextDelimitedRecordReader(options);

            MasterController masterController = new MasterController();
            DocumentCollection documentCollection = new DocumentCollection();
            Document document = new Document("test");
            Page page = new Page("testPage");

            PageElements.Line element = new PageElements.Line();

            element.Name = "testText";
            element.InitialX = new Unit(10, UnitTypes.Centimeter);
            element.InitialY = new Unit(5, UnitTypes.Centimeter);
            element.Width = new Unit(10, UnitTypes.Centimeter);
            element.Thickness = new Unit(3, UnitTypes.Millimeter);
            element.BorderColor = new Colour(Color.Blue);

            page.PageElements.Add(element);

            document.Add(page);
            documentCollection.Add(document);

            masterController.PdfController.SaveDocumentCollectionToState(documentCollection);

            masterController.PersistenceController.SaveState(TestResources.TEST_SAVE_FILE);
            masterController.PersistenceController.LoadState(TestResources.TEST_SAVE_FILE);

            //Assert.AreEqual(10, masterController.PersistenceController.State.DocumentCollection.Documents[0].Pages[0].PageElements[0].Width.Value);

            masterController.PdfController.ComposePdf(results, masterController.PersistenceController.State.DocumentCollection, TestResources.TEST_OUT_PDF);

            Assert.IsTrue(File.Exists(TestResources.TEST_OUT_PDF));
        }
    }
}