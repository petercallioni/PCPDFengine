namespace PCPDFengineCoreTests
{
    internal static class TestResources
    {
        internal static string RESOURCES_DIRECTORY = Path.GetFullPath("./TestResources/");
        internal static string TEST_SAVE_FILE = Path.Combine(RESOURCES_DIRECTORY, "TEST.pcpdf");

        internal static class DataFiles
        {
            internal static string DIRECTORY = Path.Combine(RESOURCES_DIRECTORY, "TestDataFiles");
            internal static string FIXED_WIDTH = Path.Combine(DIRECTORY, "FixedWidth.txt");
            internal static string DELIMITED_CSV = Path.Combine(DIRECTORY, "Delimited.csv");
        }

        internal static class InsertFiles
        {
            internal static string DIRECTORY = Path.Combine(RESOURCES_DIRECTORY, "TestInsertFiles");
            internal static string TestPdf = Path.Combine(DIRECTORY, "TestPDF.pdf");
            internal static string TestPng = Path.Combine(DIRECTORY, "TestPNG.png");
            internal static string TestJpeg = Path.Combine(DIRECTORY, "TestJPEG.jpg");
            internal static string TestBmp = Path.Combine(DIRECTORY, "TestBMP.bmp");
            internal static string TestGif = Path.Combine(DIRECTORY, "TestGIF.gif");
            internal static string TestTiff = Path.Combine(DIRECTORY, "TestTIFF.tif");
        }
    }
}
