namespace PCPDFengineCore.Composition
{
    public class DocumentCollection
    {
        private List<Document> documents;

        public DocumentCollection()
        {
            this.documents = new List<Document>();
        }

        internal List<Document> Documents { get => documents; }

        public void Add(Document document)
        {
            documents.Add(document);
        }

        public void Remove(string documentName)
        {
            documents.RemoveAll(document => document.Name == documentName);
        }

        public void Remove(Document document)
        {
            documents.Remove(document);
        }
    }
}
