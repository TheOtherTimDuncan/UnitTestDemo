using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Web.Sections.Home;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.Test.Fluent.ValidationTests
{
    [TestClass]
    public class BlogModelValidatorTests
    {
        public BlogModelValidator GetValidator()
        {
            return new BlogModelValidator();
        }

        [TestClass]
        public class NamePropertyTests : BlogModelValidatorTests
        {
            [TestMethod]
            public void ShouldHaveErrorWhenNameIsNull()
            {
                GetValidator().ShouldHaveValidationErrorFor(x => x.Name, (string)null);
            }

            [TestMethod]
            public void ShouldHaveErrorWhenNameIsEmpty()
            {
                GetValidator().ShouldHaveValidationErrorFor(x => x.Name, "");
            }

            [TestMethod]
            public void ShouldHaveErrorWhenNameIsWhitespace()
            {
                GetValidator().ShouldHaveValidationErrorFor(x => x.Name, "  ");
            }

            [TestMethod]
            public void ShouldHaveErrorWhenNameIsLessThan2Characters()
            {
                GetValidator().ShouldHaveValidationErrorFor(x => x.Name, "x");
            }

            [TestMethod]
            public void ShouldHaveErrorWhenNameIsMoreThan50Characters()
            {
                GetValidator().ShouldHaveValidationErrorFor(x => x.Name, new string('*', 51));
            }

            [TestMethod]
            public void ShouldNotHaveErrorWhenNameHasValue()
            {
                GetValidator().ShouldNotHaveValidationErrorFor(x => x.Name, "xx");
            }
        }
    }
}
