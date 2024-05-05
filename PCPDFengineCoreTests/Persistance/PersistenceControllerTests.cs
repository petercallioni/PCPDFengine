using PCPDFengineCore.Extensions;
using PCPDFengineCore.Persistence.Records;
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
        public void InsertAndReadDataTest()
        {
            _controller.LoadDatabase(TestResources.TEST_DATABASE, true);
            FileInformation fileInformation = new FileInformation(DatabaseInformation.Version, DateTime.Now, DateTime.Now);

            _controller.UpdateFileInformation(fileInformation);
            _controller.SaveDatabase();
            _controller.CloseDatabase();

            _controller.LoadDatabase(TestResources.TEST_DATABASE, false);
            FileInformation? readInfo = _controller.GetFileInformation();
            _controller.CloseDatabase();

            Trace.WriteLine(readInfo?.DumpObject() ?? "null content");
            Assert.IsNotNull(readInfo);
        }

        [TestMethod()]
        public void CanCreateDataBaseTest()
        {
            _controller.LoadDatabase(TestResources.TEST_DATABASE, true);
            Trace.WriteLine(_controller.AccessMode);

            _controller.CloseDatabase();
            Assert.IsTrue(new FileInfo(TestResources.TEST_DATABASE).Exists);
        }

        [TestMethod()]
        public void WriteAccessDataBaseTest()
        {
            _controller.LoadDatabase(TestResources.TEST_DATABASE, true);
            Assert.AreEqual(AccessMode.WRITE, _controller.AccessMode);
            _controller.CloseDatabase();
        }


        [TestMethod()]
        public void ReadAccessDataBaseTest()
        {
            // Simulate another process having a process lock
            FileStream fileStream = new FileStream(TestResources.TEST_DATABASE, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            _controller.LoadDatabase(TestResources.TEST_DATABASE, true);

            Assert.AreEqual(AccessMode.READ, _controller.AccessMode);

            fileStream.Close();
            _controller.CloseDatabase();
        }
    }
}