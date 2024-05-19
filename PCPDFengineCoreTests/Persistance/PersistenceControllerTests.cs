using PCPDFengineCore.Extensions;
using PCPDFengineCore.Fonts;
using PCPDFengineCore.Models.RecordReaderOptions;
using PCPDFengineCore.RecordReader;
using PCPDFengineCore.RecordReader.RecordReaderOptions;
using PCPDFengineCoreTests;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UglyToad.PdfPig.Writer;

namespace PCPDFengineCore.Persistence.Tests
{
    [TestClass()]
    public class PersistenceControllerTests
    {
        [TestMethod()]
        public void CanSaveFile()
        {
            PersistenceController controller = new PersistenceController();
            controller.SaveState(TestResources.TEST_SAVE_FILE);

            Assert.IsTrue(new FileInfo(TestResources.TEST_SAVE_FILE).Exists);
        }

        [TestMethod()]
        public void CanLoadFileInformation()
        {
            PersistenceController controller = new PersistenceController();

            FileInformation fileInformation = new FileInformation();

            controller.State.FileInformation = fileInformation;

            controller.SaveState(TestResources.TEST_SAVE_FILE);

            controller.LoadState(TestResources.TEST_SAVE_FILE);

            Trace.WriteLine(controller.State.DumpObject());
            Assert.AreEqual(controller.State.FileInformation.DatabaseVersion, DatabaseInformation.Version);
        }

        [TestMethod()]
        public void CanLoadDataReaderFixedWdith()
        {
            PersistenceController controller = new PersistenceController();

            TextFixedRecordReaderOptions _options = new TextFixedRecordReaderOptions(1, true, new Field(PCPDFengineCore.Models.Enums.FieldType.STRING, "Header 0", "HEAD"),
                new TextFixedWidthDataField[]
                {
                    new TextFixedWidthDataField("Header 0", 5, FixedWidthAligment.LEFT, PCPDFengineCore.Models.Enums.FieldType.STRING),
                }.ToList());

            TextFixedRecordReader reader = new TextFixedRecordReader(_options);
            controller.State.RecordReader = reader;

            controller.SaveState(TestResources.TEST_SAVE_FILE);

            controller.LoadState(TestResources.TEST_SAVE_FILE);

            Assert.AreEqual(((TextFixedRecordReader)controller.State.RecordReader).Options.Fields.First().Name, "Header 0");
        }

        [TestMethod()]
        public void CanLoadDataReaderDelimited()
        {
            PersistenceController controller = new PersistenceController();

            TextDelimitedRecordReaderOptions options = new TextDelimitedRecordReaderOptions(1, new Field(PCPDFengineCore.Models.Enums.FieldType.STRING, "Header 0", "HEAD"),
                new TextDelimitedDataField[]
                {
                    new TextDelimitedDataField("Header 0", PCPDFengineCore.Models.Enums.FieldType.STRING),
                }.ToList());

            TextDelimitedRecordReader reader = new TextDelimitedRecordReader(options);
            controller.State.RecordReader = reader;

            controller.SaveState(TestResources.TEST_SAVE_FILE);

            controller.LoadState(TestResources.TEST_SAVE_FILE);

            Assert.AreEqual(((TextDelimitedRecordReader)controller.State.RecordReader).Options.Fields.First().Name, "Header 0");
        }

        [TestMethod()]
        public void CreatePersistentStateIndent()
        {
            MasterController masterController = new MasterController();

            PersistenceController controller = new PersistenceController(true);
            controller.SaveState(TestResources.TEST_SAVE_FILE);
            string json = controller.GetRawPersistantStateJson(TestResources.TEST_SAVE_FILE);

            Assert.IsTrue(Regex.Match(json, "^\\{[\\s]").Success);
        }

        [TestMethod()]
        public void CreatePersistentStateNoIndent()
        {
            PersistenceController controller = new PersistenceController();
            controller.SaveState(TestResources.TEST_SAVE_FILE);

            string json = controller.GetRawPersistantStateJson(TestResources.TEST_SAVE_FILE);

            Assert.IsTrue(Regex.Match(json, "^\\{[^\\s]").Success);
        }

        [TestMethod()]
        public void AddFontToState()
        {
            MasterController masterController = new MasterController();

            masterController.FontController.AddFontToState("Arial", "Regular");
            masterController.PersistenceController.SaveState(TestResources.TEST_SAVE_FILE);

            masterController.PersistenceController.LoadState(TestResources.TEST_SAVE_FILE);

            byte[] arial = masterController.FontController.GetFont("Arial", "Regular");

            PdfDocumentBuilder builder = new PdfDocumentBuilder();
            PdfDocumentBuilder.AddedFont font = builder.AddTrueTypeFont(arial);

            Assert.IsTrue(true); // At this point the font loaded correctly.
        }

        [TestMethod()]
        public void AddNonEmbedFontToState()
        {
            MasterController masterController = new MasterController();
            masterController.FontController.SetEmbedFonts(false);

            masterController.FontController.AddFontToState("Arial", "Regular");
            masterController.PersistenceController.SaveState(TestResources.TEST_SAVE_FILE);

            masterController.PersistenceController.LoadState(TestResources.TEST_SAVE_FILE);

            FontInfo arial = masterController.FontController.GetFontInfo("Arial", "Regular");

            Assert.IsTrue(arial.Bytes == null);
        }

        [TestMethod()]
        public void RemoveFontFromState()
        {
            MasterController masterController = new MasterController();

            masterController.FontController.SetEmbedFonts(true);
            masterController.FontController.AddFontToState("Arial", "Regular");
            masterController.PersistenceController.SaveState(TestResources.TEST_SAVE_FILE);

            masterController.PersistenceController.LoadState(TestResources.TEST_SAVE_FILE);

            Assert.IsTrue(masterController.PersistenceController.State.EmbeddedFonts.Count == 1);

            masterController.FontController.RemoveFontFromState("Arial", "Regular");
            masterController.PersistenceController.SaveState(TestResources.TEST_SAVE_FILE);

            Assert.IsTrue(masterController.PersistenceController.State.EmbeddedFonts.Count == 0);
        }

        [TestMethod()]
        public void AddImageToState()
        {
            MasterController masterController = new MasterController();

            masterController.ImageController.AddImageToState(TestResources.InsertFiles.TEST_JPEG);
            masterController.PersistenceController.SaveState(TestResources.TEST_SAVE_FILE);

            masterController.PersistenceController.LoadState(TestResources.TEST_SAVE_FILE);

            byte[] testImage = masterController.ImageController.GetImage(new FileInfo(TestResources.InsertFiles.TEST_JPEG).Name);

            PdfDocumentBuilder builder = new PdfDocumentBuilder();
            PdfPageBuilder page = builder.AddPage(1, 1);

            page.AddJpeg(testImage, new UglyToad.PdfPig.Core.PdfRectangle(1, 1, 1, 1));
            Assert.IsTrue(true); // At this point the image loaded correctly.
        }


        [TestMethod()]
        public void RemoveImageFromState()
        {
            MasterController masterController = new MasterController();

            masterController.ImageController.AddImageToState(TestResources.InsertFiles.TEST_JPEG);
            masterController.PersistenceController.SaveState(TestResources.TEST_SAVE_FILE);

            masterController.PersistenceController.LoadState(TestResources.TEST_SAVE_FILE);

            Assert.IsTrue(masterController.PersistenceController.State.EmbeddedImages.Count == 1);

            masterController.ImageController.RemoveImageFromState(new FileInfo(TestResources.InsertFiles.TEST_JPEG).Name);
            masterController.PersistenceController.SaveState(TestResources.TEST_SAVE_FILE);

            Assert.IsTrue(masterController.PersistenceController.State.EmbeddedImages.Count == 0);
        }
    }
}