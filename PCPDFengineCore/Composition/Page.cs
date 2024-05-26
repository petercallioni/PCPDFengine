using PCPDFengineCore.Composition.PageElements;
using PCPDFengineCore.Composition.Units;

namespace PCPDFengineCore.Composition
{
    public class Page
    {
        private string name;
        private Unit width;
        private Unit height;

        private List<PageElement> pageElements;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Page() { }      // Required for json serialisation
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Page(string name)
        {
            this.name = name;
            pageElements = new List<PageElement>();
            width = new Unit(21, UnitTypes.Centimeter);        // Default to A4
            height = new Unit(29.7, UnitTypes.Centimeter);     // Default to A4
        }

        public Page(string name, Unit width, Unit height)
        {
            this.name = name;
            this.width = width;
            this.height = height;
            pageElements = new List<PageElement>();
        }

        public string Name { get => name; set => name = value; }
        public Unit Width { get => width; set => width = value; }
        public Unit Height { get => height; set => height = value; }
        public List<PageElement> PageElements { get => pageElements; set => pageElements = value; }
    }
}
