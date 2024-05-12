using PCPDFengineCore.Extensions;
using PCPDFengineCore.Fonts;
using PCPDFengineCore.Models.RecordReaderOptions;
using PCPDFengineCore.RecordReader;
using PCPDFengineCore.RecordReader.RecordReaderOptions;
using PCPDFengineCoreTests;
using System.Diagnostics;
using System.Drawing.Text;
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
            PersistanceState state = new PersistanceState();
            controller.SaveState(state, TestResources.TEST_SAVE_FILE);

            Assert.IsTrue(new FileInfo(TestResources.TEST_SAVE_FILE).Exists);
        }

        [TestMethod()]
        public void CanLoadFileInformation()
        {
            PersistenceController controller = new PersistenceController();
            PersistanceState state = new PersistanceState();
            FileInformation fileInformation = new FileInformation();

            state.FileInformation = fileInformation;

            controller.SaveState(state, TestResources.TEST_SAVE_FILE);

            PersistanceState loadedState = controller.LoadState(TestResources.TEST_SAVE_FILE);

            Trace.WriteLine(loadedState.DumpObject());
            Assert.AreEqual(loadedState.FileInformation.DatabaseVersion, DatabaseInformation.Version);
        }

        [TestMethod()]
        public void CanLoadDataReaderFixedWdith()
        {
            PersistenceController controller = new PersistenceController();
            PersistanceState state = new PersistanceState();

            TextFixedRecordReaderOptions _options = new TextFixedRecordReaderOptions(1, true, new Field(PCPDFengineCore.Models.Enums.FieldType.STRING, "Header 0", "HEAD"),
                new TextFixedWidthDataField[]
                {
                    new TextFixedWidthDataField("Header 0", 5, FixedWidthAligment.LEFT, PCPDFengineCore.Models.Enums.FieldType.STRING),
                }.ToList());

            TextFixedRecordReader reader = new TextFixedRecordReader(_options);
            state.RecordReader = reader;

            controller.SaveState(state, TestResources.TEST_SAVE_FILE);

            PersistanceState loadedState = controller.LoadState(TestResources.TEST_SAVE_FILE);

            Assert.AreEqual(((TextFixedRecordReader)loadedState.RecordReader).Options.Fields.First().Name, "Header 0");
        }

        [TestMethod()]
        public void CanLoadDataReaderDelimited()
        {
            PersistenceController controller = new PersistenceController();
            PersistanceState state = new PersistanceState();

            TextDelimitedRecordReaderOptions options = new TextDelimitedRecordReaderOptions(1, new Field(PCPDFengineCore.Models.Enums.FieldType.STRING, "Header 0", "HEAD"),
                new TextDelimitedDataField[]
                {
                    new TextDelimitedDataField("Header 0", PCPDFengineCore.Models.Enums.FieldType.STRING),
                }.ToList());

            TextDelimitedRecordReader reader = new TextDelimitedRecordReader(options);
            state.RecordReader = reader;

            controller.SaveState(state, TestResources.TEST_SAVE_FILE);

            PersistanceState loadedState = controller.LoadState(TestResources.TEST_SAVE_FILE);

            Assert.AreEqual(((TextDelimitedRecordReader)loadedState.RecordReader).Options.Fields.First().Name, "Header 0");
        }

        [TestMethod()]
        public void CreatePersistentStateIndent()
        {
            PersistenceController controller = new PersistenceController(true);
            PersistanceState state = new PersistanceState();
            controller.SaveState(state, TestResources.TEST_SAVE_FILE);
            string json = controller.GetRawPersistantStateJson(TestResources.TEST_SAVE_FILE);

            Assert.IsTrue(Regex.Match(json, "^\\{[\\s]").Success);
        }

        [TestMethod()]
        public void CreatePersistentStateNoIndent()
        {
            PersistenceController controller = new PersistenceController();
            PersistanceState state = new PersistanceState();
            controller.SaveState(state, TestResources.TEST_SAVE_FILE);

            string json = controller.GetRawPersistantStateJson(TestResources.TEST_SAVE_FILE);

            Assert.IsTrue(Regex.Match(json, "^\\{[^\\s]").Success);
        }

        [TestMethod()]
        public void AddFontToState()
        {
            PersistenceController controller = new PersistenceController();
            FontController fontController = new FontController();
            PersistanceState state = new PersistanceState();

            Dictionary<string, List<FontInfo>> fonts = fontController.GetInstalledTtfFonts();

            KeyValuePair<string, List<FontInfo>> arial = fonts.GetEntry("Arial");
            state.AddedFonts.Add(arial.Key, arial.Value);
            controller.SaveState(state, TestResources.TEST_SAVE_FILE);

            PersistanceState loadedState = controller.LoadState(TestResources.TEST_SAVE_FILE);

            PrivateFontCollection privateFontCollection = new PrivateFontCollection();

            arial = loadedState.AddedFonts.GetEntry("Arial");
            byte[] fontBytes = arial.Value.Where(x => x.Style == "Regular").First().Bytes;

            PdfDocumentBuilder builder = new PdfDocumentBuilder();
            PdfDocumentBuilder.AddedFont font = builder.AddTrueTypeFont(fontBytes);

            Assert.IsTrue(true); // At this point the font loaded correctly.
        }
    }
}