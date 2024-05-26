namespace PCPDFengineCore.Composition
{
    public class Document
    {
        private List<Page> pages;
        private string name;

        public Document(string name)
        {
            this.name = name;
            this.pages = new List<Page>();
        }

        internal List<Page> Pages { get => pages; }
        public string Name { get => name; set => name = value; }

        public void Add(Page page)
        {
            pages.Add(page);
        }

        public void Remove(string pageName)
        {
            pages.RemoveAll(page => page.Name == pageName);
        }

        public void Remove(Page page)
        {
            pages.Remove(page);
        }
    }
}
