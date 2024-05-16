using PCPDFengineCore.Persistence;
using System.IO.Compression;

namespace PCPDFengineCore.Tests
{
    [TestClass()]
    public class MasterControllerTests
    {
        [TestMethod()]
        public void MasterControllerTest()
        {
            string filePath = "C:\\Users\\Peter\\Documents\\TEMP\\manual_test.zip";
            using (FileStream zipToOpen = new FileStream(filePath, FileMode.Open))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read))
                {
                    foreach (ZipArchiveEntry entry in archive.Entries.Where(x => x.Name == SaveFileLayout.STATE_JSON))
                    {
                        using (Stream entryStream = entry.Open())
                        {

                        }
                    }
                }
            }
        }
    }
}