using FluentValidation;
using Hahn.ApplicationProcess.December2020.Web.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Web.Validators
{
    public class ApplicantRequestValidator : AbstractValidator<ApplicantRequest>
    {
		public ApplicantRequestValidator()
		{
			RuleFor(x => x.Name).MinimumLength(5);
			RuleFor(x => x.FamilyName).MinimumLength(5);
			RuleFor(x => x.EmailAddress).EmailAddress().Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
			RuleFor(x => x.Address).MinimumLength(10);
			RuleFor(x => x.Age).InclusiveBetween(20, 60);
			RuleFor(x => x.CountryOfOrigin).NotEmpty().CountryName();
		}
	}
}
