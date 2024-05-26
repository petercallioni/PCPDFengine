using PCPDFengineCore.Extensions;

namespace PCPDFengineCore.Composition.Units
{
    public class Unit
    {
        public UnitTypes Type;
        public double Value;

        public Unit(double value, UnitTypes type)
        {
            Type = type;
            Value = value;
        }

        public Unit(double value)
        {
            Type = UnitTypes.Millimeter;
            Value = value;
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