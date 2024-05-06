using PCPDFengineCore.Extensions;
using PCPDFengineCore.Models.RecordReaderOptions;
using PCPDFengineCore.RecordReader;
using PCPDFengineCore.RecordReader.RecordReaderOptions;
using PCPDFengineCoreTests;
using System.Diagnostics;

namespace PCPDFengineCore.Persistence.Tests
{
    [TestClass()]
    public class PersistenceControllerTests
    {
        private PersistenceController _controller;
        public PersistenceControllerTests()
        {
            _controller = new PersistenceController();
        }

        [TestMethod()]
        public void TestSaveFile()
        {
            PersistanceState state = new PersistanceState();
            _controller.SaveState(state, TestResources.TEST_SAVE_FILE);

            Assert.IsTrue(new FileInfo(TestResources.TEST_SAVE_FILE).Exists);
        }

        [TestMethod()]
        public void CanLoadFileInformation()
        {
            PersistanceState state = new PersistanceState();
            FileInformation fileInformation = new FileInformation();

            state.FileInformation = fileInformation;

            _controller.SaveState(state, TestResources.TEST_SAVE_FILE);

            PersistanceState loadedState = _controller.LoadState(TestResources.TEST_SAVE_FILE);

            Trace.WriteLine(loadedState.DumpObject());
            Assert.AreEqual(loadedState.FileInformation.DatabaseVersion, DatabaseInformation.Version);
        }

        [TestMethod()]
        public void CanLoadDataReaderFixedWdith()
        {
            PersistanceState state = new PersistanceState();

            TextFixedRecordReaderOptions _options = new TextFixedRecordReaderOptions(1, true, new Field(PCPDFengineCore.Models.Enums.FieldType.STRING, "Header 0", "HEAD"),
                new TextFixedWidthDataField[]
                {
                    new TextFixedWidthDataField("Header 0", 5, FixedWidthAligment.LEFT, PCPDFengineCore.Models.Enums.FieldType.STRING),
                }.ToList());

            TextFixedRecordReader reader = new TextFixedRecordReader(_options);
            state.RecordReader = reader;

            _controller.SaveState(state, TestResources.TEST_SAVE_FILE);

            PersistanceState loadedState = _controller.LoadState(TestResources.TEST_SAVE_FILE);

            Assert.AreEqual(((TextFixedRecordReader)loadedState.RecordReader).Options.Fields.First().Name, "Header 0");
        }

        [TestMethod()]
        public void CanLoadDataReaderDelimited()
        {
            PersistanceState state = new PersistanceState();

            TextDelimitedRecordReaderOptions options = new TextDelimitedRecordReaderOptions(1, new Field(PCPDFengineCore.Models.Enums.FieldType.STRING, "Header 0", "HEAD"),
        new TextDelimitedDataField[]
        {
                new TextDelimitedDataField("Header 0", PCPDFengineCore.Models.Enums.FieldType.STRING),
        }.ToList());

            TextDelimitedRecordReader reader = new TextDelimitedRecordReader(options);
            FileInformation fileInformation = new FileInformation();

            state.FileInformation = fileInformation;
            state.RecordReader = reader;

            _controller.SaveState(state, TestResources.TEST_SAVE_FILE);

            PersistanceState loadedState = _controller.LoadState(TestResources.TEST_SAVE_FILE);

            Assert.AreEqual(((TextDelimitedRecordReader)loadedState.RecordReader).Options.Fields.First().Name, "Header 0");
        }
    }
}