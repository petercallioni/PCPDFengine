namespace PCPDFengineCore.Fonts.Tests
{
    [TestClass()]
    public class FontControllerTests
    {
        [TestMethod()]
        public void CanGetFont()
        {
            FontController fontController = new FontController();

            Dictionary<string, List<FontInfo>> fonts = fontController.InstalledFonts;

            Assert.IsTrue(fonts.TryGetValue("Arial", out List<FontInfo>? familly));
        }

        [TestMethod()]
        public void GetFontInfoNoEmbedTest()
        {
            FontController fontController = new FontController();

            FontInfo font = fontController.GetFontInfo("Arial", "Regular");

            Assert.IsNotNull(font);
        }
    }
}