using System;
using System.Collections.Generic;
using System.Linq;
using Demo.Web.Sections.Common.Validation;
using FluentValidation.Mvc;

namespace Demo.Web
{
    public static class ValidationConfig
    {
        public static void ConfigureValidation(IServiceProvider serviceProvider)
        {
            FluentValidationModelValidatorProvider.Configure(provider =>
            {
                provider.ValidatorFactory = new ValidationFactory(serviceProvider);
            });
        }
    }
}
