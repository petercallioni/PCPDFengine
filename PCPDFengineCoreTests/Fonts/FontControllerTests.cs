namespace PCPDFengineCore.Fonts.Tests
{
    [TestClass()]
    public class FontControllerTests
    {
        [TestMethod()]
        public void CanGetFont()
        {
            MasterController masterController = new MasterController();

            Dictionary<string, List<FontInfo>> fonts = masterController.FontController.InstalledFonts;

            Assert.IsTrue(fonts.TryGetValue("Arial", out List<FontInfo>? familly));
        }

        [TestMethod()]
        public void GetFontInfoNoEmbedTest()
        {
            MasterController masterController = new MasterController();

            FontInfo font = masterController.FontController.GetFontInfo("Arial", "Regular");

            Assert.IsNotNull(font);
        }
    }
}