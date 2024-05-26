using UglyToad.PdfPig.Writer;

namespace PCPDFengineCore.Composition
{
    internal class PdfComposer
    {
        internal PdfDocumentBuilder ComposePage(PdfDocumentBuilder builder, Page page)
        {
            builder.AddPage(
                page.Width.ValueAs(Units.UnitTypes.Point),
                page.Height.ValueAs(Units.UnitTypes.Point)
            );

            return builder;
        }
    }
}
