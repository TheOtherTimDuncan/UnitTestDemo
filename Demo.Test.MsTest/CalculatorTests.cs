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
                Calculator calculator = new Calculator();
                int result = calculator.Add(1, 2, 3);
                Assert.AreEqual(6, result, "that's how math works");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void ThrowsExceptionIfNoValuesGiven()
            {
                Calculator calculator = new Calculator();
                int result = calculator.Add();
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void ThrowsExceptionIfOnlyOneValueGiven()
            {
                Calculator calculator = new Calculator();
                int result = calculator.Add(1);
            }
        }

        [TestClass]
        public class SubtractMethodTests : CalculatorTests
        {
            [TestMethod]
            public void ReturnsResultOfValuesSubtractedFromEachOther()
            {
                Calculator calculator = new Calculator();
                int result = calculator.Subtract(3, 2, 1);
                Assert.AreEqual(0, result, "that's how math works");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void ThrowsExceptionIfNoValuesGiven()
            {
                Calculator calculator = new Calculator();
                int result = calculator.Subtract();
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void ThrowsExceptionIfOnlyOneValueGiven()
            {
                Calculator calculator = new Calculator();
                int result = calculator.Subtract(1);
            }
        }

        [TestClass]
        public class MultiplyMethodTests : CalculatorTests
        {
            [TestMethod]
            public void ReturnsResultOfValuesMultipliedTogether()
            {
                Calculator calculator = new Calculator();
                int result = calculator.Multiply(2, 3, 4);
                //Assert.AreEqual(12, result, "that's how math works");
                Assert.AreEqual(24, result, "that's how math works");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void ThrowsExceptionIfNoValuesGiven()
            {
                Calculator calculator = new Calculator();
                int result = calculator.Multiply();
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void ThrowsExceptionIfOnlyOneValueGiven()
            {
                Calculator calculator = new Calculator();
                int result = calculator.Multiply(1);
            }
        }

        [TestClass]
        public class DivideMethodTests : CalculatorTests
        {
            [TestMethod]
            public void ReturnsResultOfValuesDividedAgainstEach()
            {
                Calculator calculator = new Calculator();
                decimal result = calculator.Divide(24, 4, 2);
                Assert.AreEqual(3, result, "that's how math works");
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void ThrowsExceptionIfNoValuesGiven()
            {
                Calculator calculator = new Calculator();
                decimal result = calculator.Divide();
            }

            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void ThrowsExceptionIfOnlyOneValueGiven()
            {
                Calculator calculator = new Calculator();
                decimal result = calculator.Divide(1);
            }
        }
    }
}
