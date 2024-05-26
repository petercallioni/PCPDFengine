using PCPDFengineCore.Composition.Units;

namespace PCPDFengineCore.Composition.Interfaces
{
    public interface IRect : IPositionable
    {
        public Unit Width { get; set; }
        public Unit Height { get; set; }
    }
}
