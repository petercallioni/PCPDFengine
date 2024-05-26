using PCPDFengineCore.RecordReader;
using UglyToad.PdfPig.Writer;

namespace PCPDFengineCore.Composition
{
    public class PdfController
    {
        private PdfComposer pdfComposer;

        public PdfController()
        {
            pdfComposer = new PdfComposer();
        }


        public void ComposePdf(List<Record> records, DocumentCollection documentCollection, string outputFilename)
        {
            PdfDocumentBuilder builder = new PdfDocumentBuilder();

            builder = CompseCycleOne(records, documentCollection, builder);
            builder = CompseCycleTwo(records, documentCollection, builder);

            byte[] documentBytes = builder.Build();

            File.WriteAllBytes(outputFilename, documentBytes);
        }

        private PdfDocumentBuilder CompseCycleOne(List<Record> records, DocumentCollection documentCollection, PdfDocumentBuilder builder)
        {
            foreach (Record record in records)
            {
                foreach (Document document in documentCollection.Documents)
                {
                    foreach (Page page in document.Pages)
                    {
                        pdfComposer.ComposePage(builder, page);
                    }
                }
            }

            return builder;
        }

        private PdfDocumentBuilder CompseCycleTwo(List<Record> records, DocumentCollection documentCollection, PdfDocumentBuilder builder)
        {
            return builder;
        }
    }
}
