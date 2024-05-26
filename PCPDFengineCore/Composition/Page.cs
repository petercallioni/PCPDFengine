using PCPDFengineCore.Composition.Units;

namespace PCPDFengineCore.Composition
{
    public class Page
    {
        private string name;
        private Unit width;
        private Unit height;

        public Page(string name)
        {
            this.name = name;
            width = new Unit(21, UnitTypes.Centimeter);        // Default to A4
            height = new Unit(29.7, UnitTypes.Centimeter);     // Default to A4
        }

        public Page(string name, Unit width, Unit height)
        {
            this.name = name;
            this.width = width;
            this.height = height;
        }

        public string Name { get => name; set => name = value; }
        public Unit Width { get => width; set => width = value; }
        public Unit Height { get => height; set => height = value; }
    }
}
