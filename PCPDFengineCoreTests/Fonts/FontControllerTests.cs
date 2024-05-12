namespace PCPDFengineCore.Fonts.Tests
{
    [TestClass()]
    public class FontControllerTests
    {
        [TestMethod()]
        public void CanGetFont()
        {
            FontController fontController = new FontController();

            Dictionary<string, List<FontInfo>> fonts = fontController.GetInstalledTtfFonts();

            Assert.IsTrue(fonts.TryGetValue("Arial", out List<FontInfo>? familly));
        }
    }
}