using PCPDFengineCore.Composition.Interfaces;
using PCPDFengineCore.Composition.Units;
using System.Drawing;
using System.Text.Json.Serialization;

namespace PCPDFengineCore.Composition.PageElements
{
    public class Line : PageElement, IHas2Dimensions, IHasLines
    {
        private Unit thickness;
        private Unit width;
        private Unit height;
        private Colour borderColor;
        private List<Line> lines;

        public Line() : base()
        {
            width = new Unit(0, UnitTypes.Centimeter);
            height = new Unit(0, UnitTypes.Centimeter);
            thickness = new Unit(0, UnitTypes.Centimeter);
            borderColor = new Colour(Color.Black);
            lines = new List<Line>();
            lines.Add(this);
        }

        public Unit Thickness { get => thickness; set => thickness = value; }
        public Unit Width { get => width; set => width = value; }
        public Colour BorderColor { get => borderColor; set => borderColor = value; }
        public Unit Height { get => height; set => height = value; }

        [JsonIgnore]
        public List<Line> Lines { get => lines; }
    }
}
