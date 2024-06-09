namespace PCPDFengineCoreTests
{
    internal static class TestResources
    {
        internal static string RESOURCES_DIRECTORY = Path.GetFullPath("./TestResources/");
        internal static string TEST_SAVE_FILE = Path.Combine(RESOURCES_DIRECTORY, "TEST.pcpdf");

        internal static class TestPDFs
        {
            internal static string DIRECTORY = Path.Combine(RESOURCES_DIRECTORY, "TestPDFs");
            internal static string TEST_BASE_PDF = Path.Combine(DIRECTORY, "TEST.PDF");
            internal static string TEST_LINE_PDF = Path.Combine(DIRECTORY, "TEST_LINE.PDF");
            internal static string TEST_SQUARE_PDF = Path.Combine(DIRECTORY, "TEST_SQUARE.PDF");
            internal static string TEST_TRIANGLE_PDF = Path.Combine(DIRECTORY, "TEST_TRIANGLE.PDF");
        }

        internal static class DataFiles
        {
            internal static string DIRECTORY = Path.Combine(RESOURCES_DIRECTORY, "TestDataFiles");
            internal static string FIXED_WIDTH = Path.Combine(DIRECTORY, "FixedWidth.txt");
            internal static string DELIMITED_CSV = Path.Combine(DIRECTORY, "Delimited.csv");
        }

        internal static class InsertFiles
        {
            internal static string DIRECTORY = Path.Combine(RESOURCES_DIRECTORY, "TestInsertFiles");
            internal static string TEST_PDF = Path.Combine(DIRECTORY, "TestPDF.pdf");
            internal static string TEST_PNG = Path.Combine(DIRECTORY, "TestPNG.png");
            internal static string TEST_JPEG = Path.Combine(DIRECTORY, "TestJPEG.jpg");
            internal static string TEST_BMP = Path.Combine(DIRECTORY, "TestBMP.bmp");
            internal static string TEST_GIF = Path.Combine(DIRECTORY, "TestGIF.gif");
            internal static string TEST_TIFF = Path.Combine(DIRECTORY, "TestTIFF.tif");
        }
    }
}
