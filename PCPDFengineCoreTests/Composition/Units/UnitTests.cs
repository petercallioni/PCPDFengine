﻿namespace PCPDFengineCore.Composition.Units.Tests
{
    [TestClass()]
    public class UnitTests
    {
        [TestMethod()]
        public void ConvertToCentimetersFromMillimeters()
        {
            Unit unit = new Unit(10, UnitTypes.Millimeter);
            Assert.AreEqual(1, unit.ValueAs(UnitTypes.Centimeter));
        }

        [TestMethod()]
        public void ConvertToPointsFromCentimeters()
        {
            Unit unit = new Unit(21, UnitTypes.Centimeter);
            Assert.AreEqual(595.2755905511812, unit.ValueAs(UnitTypes.Point));
        }

        [TestMethod()]
        public void ConvertToInchesFromMillimeters()
        {
            Unit unit = new Unit(10, UnitTypes.Millimeter);
            Assert.AreEqual(0.3937007874015748, unit.ValueAs(UnitTypes.Inch));
        }

        [TestMethod()]
        public void ConvertToPointsFromMillimeters()
        {
            Unit unit = new Unit(100, UnitTypes.Millimeter);
            Assert.AreEqual(283.46456692913387, unit.ValueAs(UnitTypes.Point));
        }

        [TestMethod()]
        public void ConvertToMillimetersFromPoints()
        {
            Unit unit = new Unit(100, UnitTypes.Point);
            Assert.AreEqual(35.27777777777777, unit.ValueAs(UnitTypes.Millimeter));
        }

        [TestMethod()]
        public void ConvertToMillimetersFromInches()
        {
            Unit unit = new Unit(100, UnitTypes.Inch);
            Assert.AreEqual(2540, unit.ValueAs(UnitTypes.Millimeter));
        }

        [TestMethod()]
        public void ConvertToMillimetersFromCentimeters()
        {
            Unit unit = new Unit(100, UnitTypes.Centimeter);
            Assert.AreEqual(1000, unit.ValueAs(UnitTypes.Millimeter));
        }

        [TestMethod()]
        public void ConvertToMillimetersFromMillimeters()
        {
            Unit unit = new Unit(100, UnitTypes.Millimeter);
            Assert.AreEqual(100, unit.ValueAs(UnitTypes.Millimeter));
        }

        [TestMethod()]
        public void UnitOperatorJustPlusTest()
        {
            Unit a = new Unit(10, UnitTypes.Millimeter);
            Assert.AreEqual(10, +a.ValueAs(UnitTypes.Millimeter));
        }

        [TestMethod()]
        public void UnitOperatorJustNegativeTest()
        {
            Unit a = new Unit(10, UnitTypes.Millimeter);
            Assert.AreEqual(-10, -a.ValueAs(UnitTypes.Millimeter));
        }

        [TestMethod()]
        public void UnitOperatorPlusTest()
        {
            Unit a = new Unit(10, UnitTypes.Millimeter);
            Unit b = new Unit(20, UnitTypes.Millimeter);
            Assert.AreEqual(30, (a + b).ValueAs(UnitTypes.Millimeter));
        }

        [TestMethod()]
        public void UnitOperatorMinusTest()
        {
            Unit a = new Unit(50, UnitTypes.Millimeter);
            Unit b = new Unit(20, UnitTypes.Millimeter);
            Assert.AreEqual(30, (a - b).ValueAs(UnitTypes.Millimeter));
        }

        [TestMethod()]
        public void UnitOperatorTimesTest()
        {
            Unit a = new Unit(50, UnitTypes.Millimeter);
            Unit b = new Unit(2, UnitTypes.Millimeter);
            Assert.AreEqual(100, (a * b).ValueAs(UnitTypes.Millimeter));
        }

        [TestMethod()]
        public void UnitOperatorDivideTest()
        {
            Unit a = new Unit(100, UnitTypes.Millimeter);
            Unit b = new Unit(2, UnitTypes.Millimeter);
            Assert.AreEqual(50, (a / b).ValueAs(UnitTypes.Millimeter));
        }
    }
}