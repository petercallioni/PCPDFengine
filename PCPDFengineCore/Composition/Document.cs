namespace PCPDFengineCore.Composition
{
    public class Document
    {
        private List<Page> pages;
        private string name;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Document() { }  // Required for json serialisation
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Document(string name)
        {
            this.name = name;
            this.pages = new List<Page>();
        }

        public List<Page> Pages { get => pages; set => pages = value; }
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
