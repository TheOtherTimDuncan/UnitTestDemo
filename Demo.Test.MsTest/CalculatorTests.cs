using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.Test.MsTest
{
    [TestClass]
    public class CalculatorTests
    {
        [TestClass]
        public class AddMethodTests : CalculatorTests
        {
            [TestMethod]
            public void ReturnsSumOfGivenValues()
            {
                int result = new Calculator().Add(1, 2, 3);
                Assert.AreEqual(6, result, "that's how math works");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void ThrowsExceptionIfNoValuesGiven()
            {
                int result = new Calculator().Add();
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void ThrowsExceptionIfOnlyOneValueGiven()
            {
                int result = new Calculator().Add(1);
            }
        }

        [TestClass]
        public class SubtractMethodTests : CalculatorTests
        {
            [TestMethod]
            public void ReturnsResultOfValuesSubtractedFromEachOther()
            {
                int result = new Calculator().Subtract(3, 2, 1);
                Assert.AreEqual(0, result, "that's how math works");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void ThrowsExceptionIfNoValuesGiven()
            {
                int result = new Calculator().Subtract();
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void ThrowsExceptionIfOnlyOneValueGiven()
            {
                int result = new Calculator().Subtract(1);
            }
        }

        [TestClass]
        public class MultiplyMethodTests : CalculatorTests
        {
            [TestMethod]
            public void ReturnsResultOfValuesMultipliedTogether()
            {
                int result = new Calculator().Multiply(2, 3, 4);
                //Assert.AreEqual(12, result, "that's how math works");
                Assert.AreEqual(24, result, "that's how math works");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void ThrowsExceptionIfNoValuesGiven()
            {
                int result = new Calculator().Multiply();
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void ThrowsExceptionIfOnlyOneValueGiven()
            {
                int result = new Calculator().Multiply(1);
            }
        }

        [TestClass]
        public class DivideMethodTests : CalculatorTests
        {
            [TestMethod]
            public void ReturnsResultOfValuesDividedAgainstEach()
            {
                decimal result = new Calculator().Divide(24, 4, 2);
                Assert.AreEqual(3, result, "that's how math works");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void ThrowsExceptionIfNoValuesGiven()
            {
                decimal result = new Calculator().Divide();
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void ThrowsExceptionIfOnlyOneValueGiven()
            {
                decimal result = new Calculator().Divide(1);
            }
        }
    }
}
