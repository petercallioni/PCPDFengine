﻿using PCPDFengineCore.Composition.Interfaces;
using PCPDFengineCore.Composition.Units;

namespace PCPDFengineCore.Composition.PageElements
{
    public class PageElement : IPositionable
    {
        private Unit initialX;
        private Unit initialY;
        private Unit currentX;
        private Unit currentY;
        private string name;

        internal string classTypeString;
        public PageElement()
        {
            initialX = new Unit(0, UnitTypes.Millimeter);
            initialY = new Unit(0, UnitTypes.Millimeter);
            currentX = new Unit(0, UnitTypes.Millimeter);
            currentY = new Unit(0, UnitTypes.Millimeter);
            name = "";

            classTypeString = this.GetType().ToString();
        }

        public Unit InitialX
        {
            get => initialX;
            set
            {
                initialX = value;
                currentX = value;
            }
        }
        public Unit InitialY
        {
            get => initialY; set
            {
                initialY = value;
                currentY = value;
            }
        }

        public Unit CurrentX { get => currentX; set => currentX = value; }
        public Unit CurrentY { get => currentY; set => currentY = value; }

        public Unit CurrentXAdd(Unit toAdd)
        {
            Unit newValue = new Unit(CurrentX);

            newValue.Value = currentX.Value + toAdd.ValueAs(currentX.Type);

            currentX = newValue;

            return newValue;
        }

        public Unit CurrentYAdd(Unit toAdd)
        {
            Unit newValue = new Unit(CurrentY);

            newValue.Value = currentY.Value + toAdd.ValueAs(currentY.Type);

            currentY = newValue;

            return newValue;
        }

        public string Name { get => name; set => name = value; }
        public string ClassTypeString { get => classTypeString; }
    }
}
