using PCPDFengineCore.Composition.PageElements;
using PCPDFengineCore.Composition.Units;

namespace PCPDFengineCore.Composition.Interfaces
{
    public interface IHasBorder
    {
        public Unit Thickness { get; set; }
        public Colour BorderColor { get; set; }
    }
}
