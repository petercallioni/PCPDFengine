using PCPDFengineCore.Composition.Interfaces;
using PCPDFengineCore.Composition.Units;
using System.Drawing;
using System.Text.Json.Serialization;

namespace PCPDFengineCore.Composition.PageElements
{
    public enum LineEnding
    {
        FLAT,
        MITRE,
        CURVE
    }

    public class Line : PageElement, IHas2Dimensions, IHasLines
    {
        private Unit thickness;
        private Unit width;
        private Unit height;
        private Colour borderColor;
        private List<Line> lines;
        private LineEnding lineEnding;

        public Line(Line line) : base()
        {
            width = new Unit(line.Width.Value, line.Width.Type);
            height = new Unit(line.Height.Value, line.Height.Type);
            thickness = new Unit(line.Thickness.Value, line.Height.Type);
            borderColor = new Colour(line.BorderColor.R, line.BorderColor.G, line.BorderColor.B);

            lines = new List<Line>();
            lines.Add(this);

            lineEnding = line.LineEnding;

            InitialX = new Unit(line.InitialX.Value, line.InitialX.Type);
            InitialY = new Unit(line.InitialY.Value, line.InitialY.Type);
        }

        public Line() : base()
        {
            width = new Unit(0, UnitTypes.Centimeter);
            height = new Unit(0, UnitTypes.Centimeter);
            thickness = new Unit(0, UnitTypes.Centimeter);
            borderColor = new Colour(Color.Black);
            lines = new List<Line>();
            lineEnding = LineEnding.FLAT;
            lines.Add(this);
        }

        public Unit Thickness { get => thickness; set => thickness = value; }
        public Unit Width { get => width; set => width = value; }
        public Colour BorderColor { get => borderColor; set => borderColor = value; }
        public Unit Height { get => height; set => height = value; }

        [JsonIgnore]
        public List<Line> Lines { get => lines; }

        public Unit EndX()
        {
            return new Unit(CurrentX + Width);
        }
        public Unit EndY()
        {
            return new Unit(CurrentY + Height);
        }

        public LineEnding LineEnding { get => lineEnding; set => lineEnding = value; }


        public LineWithEndings ApplyLineEndings(LineEnding lineEndingType, Line mainLine, Line? connectorLineOnEnd = null, Line? connectedLineOnStart = null) // connecter on start is if the polygon is closed
        {
            Line newLine1 = mainLine;
            Triangle? startEndMiter = null;
            Triangle? endEndMiter = null;
            Curve? startEndCurve = null;
            Curve? endEndCurve = null;

            switch (lineEndingType)
            {
                case LineEnding.CURVE:
                    break;
                case LineEnding.MITRE:
                    break;
            }

            return new LineWithEndings(newLine1, startEndMiter, endEndMiter, startEndCurve, endEndCurve);
        }
    }

    public struct LineWithEndings
    {
        public Line Line;
        public Triangle? StartEndMiter;
        public Triangle? EndEndMiter;
        public Curve? StartEndCurve;
        public Curve? EndEndCurve;

        public LineWithEndings(Line line, Triangle? startEndMiter, Triangle? endEndMiter, Curve? startEndCurve, Curve? endEndCurve)
        {
            Line = line;
            StartEndMiter = startEndMiter;
            EndEndMiter = endEndMiter;
            StartEndCurve = startEndCurve;
            EndEndCurve = endEndCurve;
        }
    }
    public class Triangle
    {
        public double Point1, Point2, Point3;
    }

    public class Curve
    {

    }
}
