using PCPDFengineCore.Extensions;
using PCPDFengineCore.Models.RecordReaderOptions;
using PCPDFengineCore.RecordReader;
using PCPDFengineCore.RecordReader.RecordReaderOptions;
using PCPDFengineCoreTests;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace PCPDFengineCore.Persistence.Tests
{
    [TestClass()]
    public class PersistenceControllerTests
    {
        [TestMethod()]
        public void TestSaveFile()
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
        public void PersistenceControllerIndentTest()
        {
            PersistenceController controller = new PersistenceController(true);
            PersistanceState state = new PersistanceState();
            controller.SaveState(state, TestResources.TEST_SAVE_FILE);

            string savedJson = File.ReadAllText(TestResources.TEST_SAVE_FILE);
            Assert.IsTrue(Regex.Match(savedJson, "^\\{[\\s]").Success);
        }

        [TestMethod()]
        public void PersistenceControllerNoIndentTest()
        {
            PersistenceController controller = new PersistenceController();
            PersistanceState state = new PersistanceState();
            controller.SaveState(state, TestResources.TEST_SAVE_FILE);

            string savedJson = File.ReadAllText(TestResources.TEST_SAVE_FILE);
            Assert.IsTrue(Regex.Match(savedJson, "^\\{[^\\s]").Success);
        }
    }
}