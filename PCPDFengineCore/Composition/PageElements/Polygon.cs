using PCPDFengineCore.Composition.Interfaces;
using PCPDFengineCore.Composition.Units;
using System.Drawing;

namespace PCPDFengineCore.Composition.PageElements
{
    public enum BasePolygonTypes
    {
        RECTANGLE
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
            Line currentLine = AddLine(InitialX, InitialY, Width, new Unit(0, height.Type));
            currentLine.CurrentX = currentLine.CurrentX + Width;
            currentLine.BorderColor = new Colour(Color.Blue);

            // Right
            currentLine = AddLine(currentLine.CurrentX, currentLine.CurrentY, new Unit(0, width.Type), Height);
            currentLine.CurrentY = currentLine.CurrentY + Height;
            currentLine.BorderColor = new Colour(Color.Red);

            // Bottom
            currentLine = AddLine(currentLine.CurrentX, currentLine.CurrentY, -Width, new Unit());
            currentLine.CurrentX = currentLine.CurrentX - Width;
            currentLine.BorderColor = new Colour(Color.Green);

            //// Left
            currentLine = AddLine(currentLine.CurrentX, currentLine.CurrentY, new Unit(), -Height);
            currentLine.BorderColor = new Colour(Color.Yellow);

            CurrentY = InitialY + Height;
            CurrentX = InitialX + Width;
        }
    }
}
