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

            // Initialise directory

            if (!Directory.Exists(TestResources.TestPDFs.DIRECTORY))
            {
                Directory.CreateDirectory(TestResources.TestPDFs.DIRECTORY);
            }
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
            masterController.PdfController.ComposePdf(results, documentCollection, TestResources.TestPDFs.TEST_BASE_PDF);

            masterController.PersistenceController.SaveState(TestResources.TEST_SAVE_FILE);

            Assert.IsTrue(File.Exists(TestResources.TestPDFs.TEST_BASE_PDF));
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

            masterController.PdfController.ComposePdf(results, masterController.PersistenceController.State.DocumentCollection, TestResources.TestPDFs.TEST_BASE_PDF);

            Assert.IsTrue(File.Exists(TestResources.TestPDFs.TEST_BASE_PDF));
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

            PageElements.Line element2 = new PageElements.Line();
            element2.Name = "testText2";
            element2.InitialX = new Unit(10, UnitTypes.Centimeter);
            element2.InitialY = new Unit(5, UnitTypes.Centimeter);

            element2.Height = new Unit(2, UnitTypes.Centimeter);
            element2.Thickness = new Unit(3, UnitTypes.Millimeter);
            element2.BorderColor = new Colour(Color.Blue);

            page.PageElements.Add(element2);

            document.Add(page);
            documentCollection.Add(document);

            masterController.PdfController.SaveDocumentCollectionToState(documentCollection);

            masterController.PersistenceController.SaveState(TestResources.TEST_SAVE_FILE);
            masterController.PersistenceController.LoadState(TestResources.TEST_SAVE_FILE);

            //Assert.AreEqual(10, masterController.PersistenceController.State.DocumentCollection.Documents[0].Pages[0].PageElements[0].Width.Value);

            masterController.PdfController.ComposePdf(results, masterController.PersistenceController.State.DocumentCollection, TestResources.TestPDFs.TEST_LINE_PDF);

            Assert.IsTrue(File.Exists(TestResources.TestPDFs.TEST_LINE_PDF));
        }

        [TestMethod()]
        public void PdfPageSquareTest()
        {
            TextDelimitedRecordReader reader = new TextDelimitedRecordReader(options);

            MasterController masterController = new MasterController();
            DocumentCollection documentCollection = new DocumentCollection();
            Document document = new Document("test");
            Page page = new Page("testPage");

            PageElements.Polygon element = new PageElements.Polygon();

            element.Name = "testText";
            element.InitialX = new Unit(5, UnitTypes.Centimeter);
            element.InitialY = new Unit(10, UnitTypes.Centimeter);
            element.Width = new Unit(5, UnitTypes.Centimeter);
            element.Height = new Unit(5, UnitTypes.Centimeter);
            element.Thickness = new Unit(3, UnitTypes.Millimeter);
            element.BorderColor = new Colour(Color.Blue);
            element.InitialiseRectangle();

            page.PageElements.Add(element);

            document.Add(page);
            documentCollection.Add(document);

            masterController.PdfController.SaveDocumentCollectionToState(documentCollection);

            masterController.PersistenceController.SaveState(TestResources.TEST_SAVE_FILE);
            masterController.PersistenceController.LoadState(TestResources.TEST_SAVE_FILE);

            //Assert.AreEqual(10, masterController.PersistenceController.State.DocumentCollection.Documents[0].Pages[0].PageElements[0].Width.Value);

            masterController.PdfController.ComposePdf(results, masterController.PersistenceController.State.DocumentCollection, TestResources.TestPDFs.TEST_SQUARE_PDF);

            Assert.IsTrue(File.Exists(TestResources.TestPDFs.TEST_SQUARE_PDF));
        }
    }
}