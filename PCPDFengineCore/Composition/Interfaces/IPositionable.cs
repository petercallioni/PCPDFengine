using PCPDFengineCore.Composition.Units;

namespace PCPDFengineCore.Composition.Interfaces
{
    public interface IPositionable
    {

        public Unit InitialX { get; set; }
        public Unit InitialY { get; set; }
        public Unit CurrentX { get; set; }
        public Unit CurrentY { get; set; }
    }
}
