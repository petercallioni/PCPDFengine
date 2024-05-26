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

                    if (element is IRect)
                    {
                        Line line = (Line)element;
                        PdfPoint pdfPointTo = new PdfPoint(pdfPointOrigin.X + line.Width.ValueAs(Units.UnitTypes.Point), pdfPointOrigin.Y + line.Height.ValueAs(Units.UnitTypes.Point));

                        currentPage.SetStrokeColor(line.BorderColor.R, line.BorderColor.G, line.BorderColor.B);
                        currentPage.DrawLine(
                            pdfPointOrigin,
                            pdfPointTo,
                            (decimal)line.Thickness.ValueAs(Units.UnitTypes.Point)
                        );
                    }
                }
            }

            return builder;
        }
    }
}
