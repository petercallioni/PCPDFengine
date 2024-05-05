using PCPDFengineCoreTests;
using System.Security.Cryptography;

namespace PCPDFengineCore.Persistence.Tests
{
    [TestClass()]
    public class PersistenceControllerTests
    {
        [TestMethod()]
        public void LoadDataBaseTest()
        {
            PersistenceController controller = new PersistenceController();
            controller.LoadDatabase(TestResources.TEST_DATABASE, true);

            Assert.IsTrue(File.Exists(TestResources.TEST_DATABASE));
        }

        public void InsertDataTest()
        {

        }

        [TestMethod()]
        public void SaveDataBaseTest()
        {
            PersistenceController controller = new PersistenceController();
            controller.LoadDatabase(TestResources.TEST_DATABASE, true);
            controller.CloseDatabase();

            byte[] startHash = SHA1.HashData(File.ReadAllBytes(TestResources.TEST_DATABASE));

            controller.LoadDatabase(TestResources.TEST_DATABASE, false);
            FileInfo fileInfo = new FileInfo(TestResources.TEST_DATABASE);
            controller.SaveDatabase();
            controller.CloseDatabase();

            byte[] endHash = SHA1.HashData(File.ReadAllBytes(TestResources.TEST_DATABASE));

            Assert.AreNotEqual(startHash, endHash);
        }
    }
}