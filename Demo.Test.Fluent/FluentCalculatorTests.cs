using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.Test.Fluent
{
    [TestClass]
    public class FluentCalculatorTests
    {
        [TestClass]
        public class AddMethodTests : FluentCalculatorTests
        {
            [TestMethod]
            public void ReturnsSumOfGivenValues()
            {
                new Calculator()
                    .Add(1, 2, 3)
                    .Should()
                    .Be(6, "that's how math works");
            }

            [TestMethod]
            public void ThrowsExceptionIfNoValuesGiven()
            {
                Action action = () =>
                {
                    int result = new Calculator().Add();
                };

                action
                    .ShouldThrow<ArgumentOutOfRangeException>("at least two parameters are required")
                    .And
                    .ParamName
                    .Should()
                    .Be("values");
            }

            [TestMethod]
            public void ThrowsExceptionIfOnlyOneValueGiven()
            {
                Action action = () =>
                {
                    int result = new Calculator().Add(1);
                };

                action
                    .ShouldThrow<ArgumentOutOfRangeException>("at least two parameters are required")
                    .And
                    .ParamName
                    .Should()
                    .Be("values");
            }
        }

        [TestClass]
        public class SubtractMethodTests : FluentCalculatorTests
        {
            [TestMethod]
            public void ReturnsResultOfValuesSubtractedFromEachOther()
            {
                new Calculator()
                    .Subtract(3, 2, 1)
                    .Should()
                    .Be(0, "that's how math works");
            }

            [TestMethod]
            public void ThrowsExceptionIfNoValuesGiven()
            {
                Action action = () =>
                {
                    int result = new Calculator().Subtract();
                };

                action
                    .ShouldThrow<ArgumentOutOfRangeException>("at least two parameters are required")
                    .And
                    .ParamName
                    .Should()
                    .Be("values");
            }

            [TestMethod]
            public void ThrowsExceptionIfOnlyOneValueGiven()
            {
                Action action = () =>
                {
                    int result = new Calculator().Subtract(1);
                };

                action
                    .ShouldThrow<ArgumentOutOfRangeException>("at least two parameters are required")
                    .And
                    .ParamName
                    .Should()
                    .Be("values");
            }
        }

        [TestClass]
        public class MultiplyMethodTests : FluentCalculatorTests
        {
            [TestMethod]
            public void ReturnsResultOfValuesMultipliedTogether()
            {
                new Calculator()
                    .Multiply(2, 3, 4)
                    .Should()
                    .Be(24, "that's how math works");

                //new Calculator()
                //  .Multiply(2, 3, 4)
                //  .Should()
                //  .Be(12, "that's how math works");
            }

            [TestMethod]
            public void ThrowsExceptionIfNoValuesGiven()
            {
                Action action = () =>
                {
                    int result = new Calculator().Multiply();
                };

                action
                    .ShouldThrow<ArgumentOutOfRangeException>("at least two parameters are required")
                    .And
                    .ParamName
                    .Should()
                    .Be("values");
            }

            [TestMethod]
            public void ThrowsExceptionIfOnlyOneValueGiven()
            {
                Action action = () =>
                {
                    int result = new Calculator().Multiply(1);
                };

                action
                    .ShouldThrow<ArgumentOutOfRangeException>("at least two parameters are required")
                    .And
                    .ParamName
                    .Should()
                    .Be("values");
            }
        }

        [TestClass]
        public class DivideMethodTests : FluentCalculatorTests
        {
            [TestMethod]
            public void ReturnsResultOfValuesDividedAgainstEach()
            {
                new Calculator()
                    .Divide(24, 4, 2)
                    .Should()
                    .Be(3, "that's how math works");
            }

            [TestMethod]
            public void ThrowsExceptionIfNoValuesGiven()
            {
                Action action = () =>
                {
                    decimal result = new Calculator().Divide();
                };

                action
                    .ShouldThrow<ArgumentOutOfRangeException>("at least two parameters are required")
                    .And
                    .ParamName
                    .Should()
                    .Be("values");
            }

            [TestMethod]
            public void ThrowsExceptionIfOnlyOneValueGiven()
            {
                Action action = () =>
                {
                    decimal result = new Calculator().Divide(1);
                };

                action
                    .ShouldThrow<ArgumentOutOfRangeException>("at least two parameters are required")
                    .And
                    .ParamName
                    .Should()
                    .Be("values");
            }
        }
    }
}
