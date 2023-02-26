// Licensed under MIT No Attribution, see LICENSE file at the root.
// Copyright 2013 Andreas Gullberg Larsen (andreas.larsen84@gmail.com). Maintained at https://github.com/angularsen/UnitsNet.

using System.Globalization;
using UnitsNet.Units;
using Xunit;

namespace UnitsNet.Tests
{
    public partial class QuantityTests
    {
        [Fact]
        public void GetHashCodeForDifferentQuantitiesWithSameValuesAreNotEqual()
        {
            var length = new Length(1.0, (LengthUnit)2);
            var area = new Area(1.0, (AreaUnit)2);

            Assert.NotEqual(length.GetHashCode(), area.GetHashCode());
        }

        [Theory]
        [InlineData("10 m", "9.89 m" , ComparisonType.Absolute, 0.1, false)] // +/- 0.1m absolute tolerance and some additional margin tolerate rounding errors in test case.
        [InlineData("10 m", "9.91 m" , ComparisonType.Absolute, 0.1, true)]
        [InlineData("10 m", "10.09 m", ComparisonType.Absolute, 0.1, true)]
        [InlineData("10 m", "1009 cm", ComparisonType.Absolute, 0.1, true)]  // Different unit, still equal.
        [InlineData("10 m", "10.11 m", ComparisonType.Absolute, 0.1, false)]
        [InlineData("10 m", "8.9 m"  , ComparisonType.Absolute, 0.1, false)] // +/- 1m relative tolerance (10%) and some additional margin tolerate rounding errors in test case.
        [InlineData("10 m", "9.1 m"  , ComparisonType.Relative, 0.1, true)]
        [InlineData("10 m", "10.9 m" , ComparisonType.Relative, 0.1, true)]
        [InlineData("10 m", "11.1 m" , ComparisonType.Relative, 0.1, false)]
        public void Equals_IEquatableQuantity(string q1String, string q2String, ComparisonType comparisonType, double tolerance, bool expectedEqual)
        {
            IQuantity<Length, LengthUnit, double> q1 = Length.Parse(q1String, CultureInfo.InvariantCulture);
            IQuantity<Length, LengthUnit, double> q2 = Length.Parse(q2String, CultureInfo.InvariantCulture);

            Assert.Equal(expectedEqual, q1.Equals(q2, tolerance, comparisonType));
        }

        [Theory]
        [InlineData("10 m", "9.89 m" , ComparisonType.Absolute, 0.1, false)] // +/- 0.1m absolute tolerance and some additional margin tolerate rounding errors in test case.
        [InlineData("10 m", "9.91 m" , ComparisonType.Absolute, 0.1, true)]
        [InlineData("10 m", "10.09 m", ComparisonType.Absolute, 0.1, true)]
        [InlineData("10 m", "1009 cm", ComparisonType.Absolute, 0.1, true)]  // Different unit, still equal.
        [InlineData("10 m", "10.11 m", ComparisonType.Absolute, 0.1, false)]
        [InlineData("10 m", "8.9 m"  , ComparisonType.Absolute, 0.1, false)] // +/- 1m relative tolerance (10%) and some additional margin tolerate rounding errors in test case.
        [InlineData("10 m", "9.1 m"  , ComparisonType.Relative, 0.1, true)]
        [InlineData("10 m", "10.9 m" , ComparisonType.Relative, 0.1, true)]
        [InlineData("10 m", "11.1 m" , ComparisonType.Relative, 0.1, false)]
        public void Equals_IQuantity(string q1String, string q2String, ComparisonType comparisonType, double tolerance, bool expectedEqual)
        {
            IQuantity q1 = Quantity.Parse(CultureInfo.InvariantCulture, typeof(Length), q1String);
            IQuantity q2 = Quantity.Parse(CultureInfo.InvariantCulture, typeof(Length), q2String);

            Assert.NotEqual(q1, q2); // Strict equality should not be equal.
            Assert.Equal(expectedEqual, q1.Equals(q2, tolerance, comparisonType));
        }
    }
}
