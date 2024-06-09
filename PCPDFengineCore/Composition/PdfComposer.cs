using PCPDFengineCore.Composition.Interfaces;
using PCPDFengineCore.Composition.PageElements;
using PCPDFengineCore.Composition.Units;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Writer;

namespace PCPDFengineCore.Composition
{
    internal class PdfComposer
    {
        internal PdfDocumentBuilder ComposePage(PdfDocumentBuilder builder, Page page)
        {
            PdfPageBuilder currentPage = builder.AddPage(
                page.Width.ValueAs(Units.UnitTypes.Point),
                page.Height.ValueAs(Units.UnitTypes.Point)
            );

            foreach (PageElements.PageElement element in page.PageElements)
            {
                if (element is IPositionable)
                {
                    PdfPoint pdfPointOrigin = new PdfPoint(
                        element.InitialX.ValueAs(Units.UnitTypes.Point),
                        page.Height.ValueAs(Units.UnitTypes.Point) - element.InitialY.ValueAs(Units.UnitTypes.Point)); // PDF expects from bottom left; values in from top left

                    if (element is IHas2Dimensions)
                    {
                        if (element is IHasLines)
                        {
                            if (((IHasLines)element).Lines.Count > 1)
                            {
                                DrawPolygon(currentPage, page, (Polygon)element);
                            }
                            else
                            {
                                foreach (PageElements.Line line in ((IHasLines)element).Lines)
                                {
                                    LineWithEndings calculatedLine = line.ApplyLineEndings(line.LineEnding, line);

                                    if (calculatedLine.StartEndMiter != null)
                                    {
                                        DrawTriangle(currentPage, calculatedLine.StartEndMiter);
                                    }

                                    if (calculatedLine.EndEndMiter != null)
                                    {
                                        DrawTriangle(currentPage, calculatedLine.EndEndMiter);
                                    }

                                    if (calculatedLine.StartEndCurve != null)
                                    {

                                    }

                                    if (calculatedLine.EndEndCurve != null)
                                    {

                                    }
                                    DrawLine(currentPage, page, calculatedLine);
                                }
                            }
                        }
                    }
                }
            }

            return builder;
        }

        internal void DrawLine(PdfPageBuilder pageBuilder, Page page, LineWithEndings line)
        {
            //(Unit startX, Unit startY, Unit endX, Unit endY) = CaluclateLineWithThickness(page, line.Line);

            PdfPoint pdfPointOrigin = new PdfPoint(
                   line.Line.CurrentX.ValueAs(Units.UnitTypes.Point),
                   page.Height.ValueAs(Units.UnitTypes.Point) - line.Line.CurrentY.ValueAs(Units.UnitTypes.Point)); // PDF expects from bottom left; values in from top left

            PdfPoint pdfPointTo = new PdfPoint(line.Line.EndX().ValueAs(UnitTypes.Point), page.Height.ValueAs(Units.UnitTypes.Point) - line.Line.EndY().ValueAs(UnitTypes.Point));

            pageBuilder.SetStrokeColor(line.Line.BorderColor.R, line.Line.BorderColor.G, line.Line.BorderColor.B);

            pageBuilder.DrawLine(
                pdfPointOrigin,
                pdfPointTo,
                (decimal)line.Line.Thickness.ValueAs(Units.UnitTypes.Point)
            );
        }

        internal void DrawPolygon(PdfPageBuilder pageBuilder, Page page, Polygon polygon)
        {
            List<Line> calcLines = CalculatePolygonLineWithThickness(polygon);

            foreach (PageElements.Line line in calcLines)
            {
                PdfPoint pdfPointOrigin = new PdfPoint(
                       line.CurrentX.ValueAs(Units.UnitTypes.Point),
                       page.Height.ValueAs(Units.UnitTypes.Point) - line.CurrentY.ValueAs(Units.UnitTypes.Point)); // PDF expects from bottom left; values in from top left

                PdfPoint pdfPointTo = new PdfPoint(line.EndX().ValueAs(UnitTypes.Point), page.Height.ValueAs(Units.UnitTypes.Point) - line.EndY().ValueAs(UnitTypes.Point));
                pageBuilder.SetStrokeColor(line.BorderColor.R, line.BorderColor.G, line.BorderColor.B);
                pageBuilder.DrawLine(
                pdfPointOrigin,
                pdfPointTo,
                    (decimal)line.Thickness.ValueAs(Units.UnitTypes.Point)
                );
            }
        }

        internal List<Line> CalculatePolygonLineWithThickness(Polygon polygon)
        {
            List<Line> polygonLines = new List<Line>();
            foreach (Line line in polygon.Lines)
            {
                // find angle of line
                Unit dx = line.EndX() - line.CurrentX;
                Unit dy = line.EndY() - line.CurrentY;

                Unit centreX = (polygon.InitialX + (polygon.Width / 2));
                Unit centreY = (polygon.InitialY + (polygon.Height / 2));

                double angle = Math.Atan2(dy.ValueAs(dx.Type), dx.Value);

                double offsetX = Math.Sin(angle - Math.PI) * (line.Thickness.ValueAs(dx.Type) / 2);
                double offsetY = Math.Cos(angle) * (line.Thickness.ValueAs(dy.Type) / 2);

                if (line.InitialX.ValueIndefinite() < centreX.ValueIndefinite())
                {
                    offsetX = -offsetX;
                }

                if (line.InitialY.ValueIndefinite() > centreY.ValueIndefinite())
                {
                    offsetY = -offsetY;
                }

                Line polygonLine = new Line(line);
                polygonLine.InitialX.AddUnit(new Unit(offsetX, dx.Type));
                polygonLine.InitialY.AddUnit(new Unit(offsetY, dy.Type));
                //polygonLine.Width.AddUnit(new Unit(offsetX, dx.Type));
                //polygonLine.Height.AddUnit(new Unit(offsetY, dy.Type));

                polygonLines.Add(polygonLine);
            }

            // return the new start/end points
            return polygonLines;
        }

        internal void DrawTriangle(PdfPageBuilder page, Triangle triangle)
        {

        }
    }
}
