using PCPDFengineCore.Composition.Interfaces;
using PCPDFengineCore.Composition.PageElements;
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
                            foreach (PageElements.Line line in ((IHasLines)element).Lines)
                            {
                                pdfPointOrigin = new PdfPoint(
                                    line.InitialX.ValueAs(Units.UnitTypes.Point),
                                    page.Height.ValueAs(Units.UnitTypes.Point) - line.InitialY.ValueAs(Units.UnitTypes.Point)); // PDF expects from bottom left; values in from top left

                                DrawLine(currentPage, line, pdfPointOrigin);
                            }
                        }
                    }
                }
            }

            return builder;
        }

        internal void DrawLine(PdfPageBuilder page, Line line, PdfPoint origin)
        {
            PdfPoint pdfPointTo = new PdfPoint(origin.X + line.Width.ValueAs(Units.UnitTypes.Point), origin.Y + -line.Height.ValueAs(Units.UnitTypes.Point));

            page.SetStrokeColor(line.BorderColor.R, line.BorderColor.G, line.BorderColor.B);
            page.DrawLine(
                origin,
                pdfPointTo,
                (decimal)line.Thickness.ValueAs(Units.UnitTypes.Point)
            );
        }
    }
}
