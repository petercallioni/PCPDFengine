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

        public Unit(Unit unit)
        {
            this.type = unit.type;
            this.value = unit.value;
        }

        /// <summary>
        /// Provides a easy way to return the value in a predetermined format
        /// for comparisons.
        /// </summary>
        /// <returns></returns>
        public double ValueIndefinite()
        {
            return ValueAs(UnitTypes.Point);
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

        public Unit Negative()
        {
            Unit negative = new Unit(this);
            negative.value = -value;
            return negative;
        }

        public override string? ToString()
        {
            return this.DumpObject();
        }

        public static Unit operator +(Unit a) => a;
        public static Unit operator -(Unit a) => new Unit(a.Negative());

        public static Unit operator +(Unit a, Unit b)
        {
            UnitTypes returnType = a.Type;
            UnitTypes returnType2 = b.Type;

            if (returnType != returnType2)
            {
                throw new ArgumentException($"Cannot add {returnType} and {returnType2} directly. Use ValueAs(UnitTypes) on one of them.");
            }

            return new Unit(a.ValueAs(returnType) + b.ValueAs(returnType), returnType);
        }


        public static Unit operator -(Unit a, Unit b)
        {
            UnitTypes returnType = a.Type;
            UnitTypes returnType2 = b.Type;

            if (returnType != returnType2)
            {
                throw new ArgumentException($"Cannot subtract {returnType} and {returnType2} directly. Use ValueAs(UnitTypes) on one of them.");
            }

            return new Unit(a.ValueAs(returnType) + -b.ValueAs(returnType), returnType);
        }

        public static Unit operator *(Unit a, Unit b)
        {
            UnitTypes returnType = a.Type;
            UnitTypes returnType2 = b.Type;

            if (returnType != returnType2)
            {
                throw new ArgumentException($"Cannot times {returnType} and {returnType2} directly. Use ValueAs(UnitTypes) on one of them.");
            }

            return new Unit(a.ValueAs(returnType) * b.ValueAs(returnType), returnType);
        }

        public static Unit operator /(Unit a, Unit b)
        {
            if (b.value == 0 || a.value == 0)
            {
                throw new DivideByZeroException();
            }

            UnitTypes returnType = a.Type;
            UnitTypes returnType2 = b.Type;

            if (returnType != returnType2)
            {
                throw new ArgumentException($"Cannot divide {returnType} and {returnType2} directly. Use ValueAs(UnitTypes) on one of them.");
            }

            return new Unit(a.ValueAs(returnType) / b.ValueAs(returnType), returnType);
        }

        public static Unit operator *(Unit a, double b)
        {
            UnitTypes returnType = a.Type;

            return new Unit(a.ValueAs(returnType) * b, returnType);
        }

        public static bool operator >(Unit a, Unit b)
        {
            UnitTypes returnType = a.Type;
            UnitTypes returnType2 = b.Type;

            if (returnType != returnType2)
            {
                throw new ArgumentException($"Cannot compare > {returnType} and {returnType2} directly. Use ValueAs(UnitTypes) on one of them.");
            }

            return a.ValueAs(returnType) > b.ValueAs(returnType);
        }

        public static bool operator <(Unit a, Unit b)
        {
            UnitTypes returnType = a.Type;
            UnitTypes returnType2 = b.Type;

            if (returnType != returnType2)
            {
                throw new ArgumentException($"Cannot compare < {returnType} and {returnType2} directly. Use ValueAs(UnitTypes) on one of them.");
            }

            return a.ValueAs(returnType) < b.ValueAs(returnType);
        }

        public static Unit operator /(Unit a, double b)
        {
            if (b == 0 || a.value == 0)
            {
                throw new DivideByZeroException();
            }

            UnitTypes returnType = a.Type;

            return new Unit(a.ValueAs(returnType) / b, returnType);
        }

        public void AddUnit(Unit toAdd)
        {
            this.Value = this.Value + toAdd.ValueAs(this.Type);
        }
    }
}