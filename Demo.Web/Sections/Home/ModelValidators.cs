using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Web.Sections.Home.Models;
using FluentValidation;

namespace Demo.Web.Sections.Home
{
    public class BlogModelValidator : AbstractValidator<BlogModel>
    {
        public BlogModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Please enter a name")
                .Length(2, 50).WithMessage("Please enter a name with at least 2 characters and no more than 50");
        }
    }
}
