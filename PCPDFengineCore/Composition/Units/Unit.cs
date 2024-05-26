using PCPDFengineCore.Extensions;

namespace PCPDFengineCore.Composition.Units
{
    public class Unit
    {
        public UnitTypes Type { get => type; set => type = value; }
        public double Value { get => value; set => this.value = value; }

        private UnitTypes type;
        private double value;

        public Unit() { }
        public Unit(double value, UnitTypes type)
        {
            this.type = type;
            this.value = value;
        }

        public Unit(double value)
        {
            this.type = UnitTypes.Millimeter;
            this.value = value;
        }

        public double ValueAs(UnitTypes desiredType)
        {
            if (Type == desiredType)
                return Value;

            double newValue = Value;

            // Normalise to default millimeters
            switch (Type)
            {
                case UnitTypes.Centimeter:
                    newValue = Value * 10;
                    break;

                case UnitTypes.Inch:
                    newValue = Value * 25.4;
                    break;

                case UnitTypes.Point:
                    newValue = (Value / 72) * 25.4;
                    break;
            }

            // Values are now in millimeters.
            switch (desiredType)
            {
                case UnitTypes.Centimeter:
                    newValue = newValue / 10;
                    break;

                case UnitTypes.Inch:
                    newValue = newValue / 25.4;
                    break;

                case UnitTypes.Point:
                    newValue = (newValue / 25.4) * 72;
                    break;
            }

            return newValue;
        }

        public override string? ToString()
        {
            return this.DumpObject();
        }
    }
}