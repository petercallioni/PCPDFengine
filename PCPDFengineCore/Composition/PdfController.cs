using PCPDFengineCore.Persistence;
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

        private PersistenceController? persistenceController;
        private PersistenceController PersistenceController
        {
            get
            {
                if (persistenceController == null)
                {
                    throw new NullReferenceException("PersistenceController called before it is set.");
                }
                return persistenceController;
            }
        }
        public void SetPersistenceController(PersistenceController persistenceController)
        {
            this.persistenceController = persistenceController;
        }

        public void ComposePdf(List<Record> records, DocumentCollection documentCollection, string outputFilename)
        {
            PdfDocumentBuilder builder = new PdfDocumentBuilder();

            builder = CompseCycleOne(records, documentCollection, builder);
            builder = CompseCycleTwo(records, documentCollection, builder);

            byte[] documentBytes = builder.Build();

            File.WriteAllBytes(outputFilename, documentBytes);
        }

        public void SaveDocumentCollectionToState(DocumentCollection documentCollection)
        {
            PersistenceController.State.DocumentCollection = documentCollection;
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
