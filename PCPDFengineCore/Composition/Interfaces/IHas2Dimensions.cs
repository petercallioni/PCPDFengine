using PCPDFengineCore.Composition.Units;

namespace PCPDFengineCore.Composition.Interfaces
{
    public interface IHas2Dimensions : IPositionable
    {
        public Unit Width { get; set; }
        public Unit Height { get; set; }
    }
}
