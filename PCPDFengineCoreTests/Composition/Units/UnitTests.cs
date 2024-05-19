namespace PCPDFengineCore.Composition.Units.Tests
{
    [TestClass()]
    public class UnitTests
    {
        [TestMethod()]
        public void ConvertToCentimetersFromMillimeters()
        {
            Unit unit = new Unit(10, UnitTypes.Millimeter);
            Assert.AreEqual(1, unit.ConvertTo(UnitTypes.Centimeter).Value);
        }

        [TestMethod()]
        public void ConvertToInchesFromMillimeters()
        {
            Unit unit = new Unit(10, UnitTypes.Millimeter);
            Assert.AreEqual(0.3937007874015748, unit.ConvertTo(UnitTypes.Inch).Value);
        }

        [TestMethod()]
        public void ConvertToPointsFromMillimeters()
        {
            Unit unit = new Unit(100, UnitTypes.Millimeter);
            Assert.AreEqual(283.46456692913387, unit.ConvertTo(UnitTypes.Point).Value);
        }

        [TestMethod()]
        public void ConvertToMillimetersFromPoints()
        {
            Unit unit = new Unit(100, UnitTypes.Point);
            Assert.AreEqual(35.27777777777777, unit.ConvertTo(UnitTypes.Millimeter).Value);
        }

        [TestMethod()]
        public void ConvertToMillimetersFromInches()
        {
            Unit unit = new Unit(100, UnitTypes.Inch);
            Assert.AreEqual(2540, unit.ConvertTo(UnitTypes.Millimeter).Value);
        }

        [TestMethod()]
        public void ConvertToMillimetersFromCentimeters()
        {
            Unit unit = new Unit(100, UnitTypes.Centimeter);
            Assert.AreEqual(1000, unit.ConvertTo(UnitTypes.Millimeter).Value);
        }

        [TestMethod()]
        public void ConvertToMillimetersFromMillimeters()
        {
            Unit unit = new Unit(100, UnitTypes.Millimeter);
            Assert.AreEqual(100, unit.ConvertTo(UnitTypes.Millimeter).Value);
        }
    }
}