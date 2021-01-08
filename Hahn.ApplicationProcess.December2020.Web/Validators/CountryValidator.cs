using FluentValidation;
using FluentValidation.Validators;
using Hahn.ApplicationProcess.December2020.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Web.Validators
{
    public class CountryValidator : PropertyValidator
    {
        public CountryValidator()
            : base("'{PropertyValue}' is not a valid country name.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var name = (string)context.PropertyValue;

            if (string.IsNullOrEmpty(name))
            {
                return true;
            }

            return Countries.IsKnownCountry(name).Result;
        }
    }

    public static class CountryNameValidatorExtension
    {
        public static IRuleBuilderOptions<T, string> CountryName<T>(
            this IRuleBuilder<T, string> rule
        )
        {
            return rule.SetValidator(new CountryValidator());
        }
    }
}
