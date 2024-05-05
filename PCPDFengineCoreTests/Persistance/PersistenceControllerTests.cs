using PCPDFengineCore.Extensions;
using PCPDFengineCore.Persistence.Records;
using PCPDFengineCoreTests;
using System.Diagnostics;

namespace PCPDFengineCore.Persistence.Tests
{
    [TestClass()]
    public class PersistenceControllerTests
    {
        PersistenceController controller;
        public PersistenceControllerTests()
        {
            controller = new PersistenceController();
        }

        [TestMethod()]
        public void InsertAndReadDataTest()
        {
            controller.LoadDatabase(TestResources.TEST_DATABASE, true);
            FileInformation fileInformation = new FileInformation();
            fileInformation.DatabaseVersion = DatabaseInformation.Version;
            fileInformation.TimeCreated = DateTime.Now;
            fileInformation.TimeUpdated = DateTime.Now;

            controller.UpdateFileInformation(fileInformation);
            controller.SaveDatabase();
            controller.CloseDatabase();

            controller.LoadDatabase(TestResources.TEST_DATABASE, false);
            FileInformation? readInfo = controller.GetFileInformation();
            controller.CloseDatabase();

            Trace.WriteLine(readInfo.DumpObject());
            Assert.IsNotNull(readInfo);
        }

        [TestMethod()]
        public void CanCreateDataBaseTest()
        {
            controller.LoadDatabase(TestResources.TEST_DATABASE, true);
            controller.CloseDatabase();

            Assert.IsTrue(new FileInfo(TestResources.TEST_DATABASE).Exists);
        }
    }
}