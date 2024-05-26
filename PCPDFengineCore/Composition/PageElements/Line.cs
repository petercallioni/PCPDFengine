using PCPDFengineCore.Composition.Interfaces;
using PCPDFengineCore.Composition.Units;
using System.Drawing;

namespace PCPDFengineCore.Composition.PageElements
{
    public class Line : PageElement, IRect, IHasBorder
    {
        private Unit thickness;
        private Unit width;
        private Unit height;
        private Colour borderColor;

        public Line() : base()
        {
            width = new Unit(0, UnitTypes.Centimeter);
            height = new Unit(0, UnitTypes.Centimeter);
            thickness = new Unit(0, UnitTypes.Centimeter);
            borderColor = new Colour(Color.Black);

            classTypeString = this.GetType().ToString();
        }

        public Unit Thickness { get => thickness; set => thickness = value; }
        public Unit Width { get => width; set => width = value; }
        public Colour BorderColor { get => borderColor; set => borderColor = value; }
        public Unit Height { get => height; set => height = value; }
    }
}
