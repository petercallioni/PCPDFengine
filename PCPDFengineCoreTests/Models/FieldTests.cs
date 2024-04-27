using PCPDFengineCoreTests;

namespace PCPDFengineCore.Models.Tests
{
    [TestClass()]
    public class FieldTests
    {
        [TestMethod()]
        public void FieldTestString()
        {
            string testString = "THIS IS A TEST";
            Field field = new Field(Enums.FieldType.STRING, "N/A", testString);
            Assert.AreEqual(testString, field.ConvertToActualType<string>());
        }

        [TestMethod()]
        public void FieldTestStringInt()
        {
            Field field = new Field(Enums.FieldType.INT, "N/A", "0005");
            Assert.AreEqual(5, field.ConvertToActualType<int>());
        }

        [TestMethod()]
        public void FieldTestStringBoolTrue()
        {
            Field field = new Field(Enums.FieldType.BOOLEAN, "N/A", "true");
            Assert.AreEqual(true, field.ConvertToActualType<bool>());
        }
        [TestMethod()]
        public void FieldTestStringBoolYes()
        {
            Field field = new Field(Enums.FieldType.BOOLEAN, "N/A", "yes");
            Assert.AreEqual(true, field.ConvertToActualType<bool>());
        }
        [TestMethod()]
        public void FieldTestStringBool1()
        {
            Field field = new Field(Enums.FieldType.BOOLEAN, "N/A", "1");
            Assert.AreEqual(true, field.ConvertToActualType<bool>());
        }
        [TestMethod()]
        public void FieldTestStringBoolFalse()
        {
            Field field = new Field(Enums.FieldType.BOOLEAN, "N/A", "false");
            Assert.AreEqual(false, field.ConvertToActualType<bool>());
        }

        [TestMethod()]
        public void FieldTestStringBoolNo()
        {
            Field field = new Field(Enums.FieldType.BOOLEAN, "N/A", "no");
            Assert.AreEqual(false, field.ConvertToActualType<bool>());
        }
        [TestMethod()]
        public void FieldTestStringBool0()
        {
            Field field = new Field(Enums.FieldType.BOOLEAN, "N/A", "0");
            Assert.AreEqual(false, field.ConvertToActualType<bool>());
        }
        [TestMethod()]
        public void FieldTestBigInt()
        {
            Field field = new Field(Enums.FieldType.BIG_INT, "N/A", "05550000000");
            Assert.AreEqual(5550000000, field.ConvertToActualType<long>());
        }

        [TestMethod()]
        public void FieldTestDouble()
        {
            Field field = new Field(Enums.FieldType.DOUBLE, "N/A", "5.56");
            Assert.AreEqual(5.56, field.ConvertToActualType<double>());
        }

        [TestMethod()]
        public void FieldTestInsertPdf()
        {
            Field field = new Field(Enums.FieldType.INSERT_PDF, "N/A", TestResources.InsertFiles.TestPdf);
            Assert.AreEqual(true, field.ConvertToActualType<FileInfo>().Exists);
        }

        [TestMethod()]
        public void FieldTestInsertImageBmp()
        {
            Field field = new Field(Enums.FieldType.INSERT_IMAGE, "N/A", TestResources.InsertFiles.TestBmp);
            Assert.AreEqual(true, field.ConvertToActualType<FileInfo>().Exists);
        }
        [TestMethod()]
        public void FieldTestInsertImageGif()
        {
            Field field = new Field(Enums.FieldType.INSERT_IMAGE, "N/A", TestResources.InsertFiles.TestGif);
            Assert.AreEqual(true, field.ConvertToActualType<FileInfo>().Exists);
        }
        [TestMethod()]
        public void FieldTestInsertImageJpeg()
        {
            Field field = new Field(Enums.FieldType.INSERT_IMAGE, "N/A", TestResources.InsertFiles.TestJpeg);
            Assert.AreEqual(true, field.ConvertToActualType<FileInfo>().Exists);
        }
        [TestMethod()]
        public void FieldTestInsertImagePng()
        {
            Field field = new Field(Enums.FieldType.INSERT_IMAGE, "N/A", TestResources.InsertFiles.TestPng);
            Assert.AreEqual(true, field.ConvertToActualType<FileInfo>().Exists);
        }
        [TestMethod()]
        public void FieldTestInsertImageTiff()
        {
            Field field = new Field(Enums.FieldType.INSERT_IMAGE, "N/A", TestResources.InsertFiles.TestTiff);
            Assert.AreEqual(true, field.ConvertToActualType<FileInfo>().Exists);
        }
    }
}