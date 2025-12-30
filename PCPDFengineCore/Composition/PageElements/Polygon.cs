using PCPDFengineCore.Composition.Interfaces;
using PCPDFengineCore.Composition.Units;
using System.Drawing;

namespace PCPDFengineCore.Composition.PageElements
{
    public enum BasePolygonTypes
    {
        RECTANGLE,
        TRIANGLE
    }

    public class Polygon : PageElement, IHas2Dimensions, IHasBorder, IHasLines
    {
        private Unit thickness;
        private Colour borderColor;
        private Unit width;
        private Unit height;

        private List<Line> lines;

        public Polygon() : base()
        {
            width = new Unit(0, UnitTypes.Centimeter);
            height = new Unit(0, UnitTypes.Centimeter);
            thickness = new Unit(0, UnitTypes.Centimeter);
            borderColor = new Colour(Color.Black);
            lines = new List<Line>();
        }

        public Polygon(BasePolygonTypes type) : base()
        {
            width = new Unit(0, UnitTypes.Centimeter);
            height = new Unit(0, UnitTypes.Centimeter);
            thickness = new Unit(0, UnitTypes.Centimeter);
            borderColor = new Colour(Color.Black);
            lines = new List<Line>();
        }

        public Unit Thickness { get => thickness; set => thickness = value; }
        public Unit Width { get => width; set => width = value; }
        public Colour BorderColor { get => borderColor; set => borderColor = value; }
        public Unit Height { get => height; set => height = value; }

        public List<Line> Lines { get => lines; set => lines = value; }

        public Line AddLine(Unit startX, Unit startY, Unit toXAdd, Unit toYAdd)
        {
            Line line = new Line();

            line.InitialX = startX;
            line.InitialY = startY;

            line.Thickness = thickness;
            line.BorderColor = borderColor;

            line.Width = toXAdd;
            line.Height = toYAdd;

            lines.Add(line);

            return line;
        }

        public void InitialiseRectangle()
        {
            Unit thicknessModifier = Thickness.Value != 0 ? Thickness / 2 : new Unit(0, Thickness.Type);

            // Top
            Line line1 = AddLine(InitialX, InitialY, Width, new Unit(0, height.Type));
            line1.BorderColor = new Colour(Color.Blue);
            line1.Name = "TOP";

            //Bottom
            Line line2 = AddLine(InitialX, InitialY + Height, Width, new Unit(0, height.Type));
            line2.BorderColor = new Colour(Color.Red);
            line2.Name = "BOTTOM";

            //Left
            Line line3 = AddLine(InitialX, InitialY, new Unit(0, width.Type), Height);
            line3.BorderColor = new Colour(Color.Green);
            line3.Name = "LEFT";

            //Right
            Line line4 = AddLine(InitialX + Width, InitialY, new Unit(0, width.Type), Height);
            line4.BorderColor = new Colour(Color.Yellow);
            line4.Name = "RIGHT";

            // Set Position to lower right corner.
            CurrentY = InitialY + Height;
            CurrentX = InitialX + Width;
        }

        public void InitialiseEquilateralTriangle()
        {
            Unit thicknessModifier = Thickness.Value != 0 ? Thickness / 2 : new Unit(0, Thickness.Type);

            // Base
            Line line1 = AddLine(InitialX, InitialY, Width, new Unit(0, height.Type));
            line1.BorderColor = new Colour(Color.Red);
            line1.Name = "FLAT";

            //Left
            Line line2 = AddLine(InitialX, InitialY, Width / 2, Height);
            line2.BorderColor = new Colour(Color.Green);
            line2.Name = "LEFT";

            //Right
            Line line3 = AddLine(InitialX + Width, InitialY, -(Width / 2), Height);
            line3.BorderColor = new Colour(Color.Yellow);
            line3.Name = "RIGHT";

            // Set Position to lower right corner.
            CurrentY = InitialY + Height;
            CurrentX = InitialX + Width;
        }

        public double SignedArea(Polygon polygon)
        {
            double area = 0;
            for (int i = 0; i < polygon.Lines.Count; i++)
            {
                Line line = polygon.Lines[i];
                area += (line.EndX().Value - line.CurrentX.Value) * (line.EndY().Value + line.CurrentY.Value);
            }
            return area / 2;
        }
    }
}
