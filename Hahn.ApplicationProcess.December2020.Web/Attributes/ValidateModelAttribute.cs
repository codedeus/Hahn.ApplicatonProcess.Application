using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Hahn.ApplicationProcess.December2020.Web.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }

        public class ValidationError
        {
            /// <summary>
            /// The field that failed model validation based on defined rule
            /// </summary>
            /// <example>FamilyName</example>
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string Field { get; }

            /// <summary>
            /// The item describing what is wrong with the stated field
            /// </summary>
            /// <example>The length of 'Family Name' must be at least 5 characters. You entered 4 characters.</example>
            public string Message { get; }

            public ValidationError(string field, string message)
            {
                Field = field != string.Empty ? field : null;
                Message = message;
            }
        }

        public class ValidationResultModel
        {
            /// <summary>
            /// Object describing what went wrong with the object validation
            /// </summary>
            /// <example>Validation Failed</example>
            public string Message { get; }

            public List<ValidationError> Errors { get; }

            public ValidationResultModel(ModelStateDictionary modelState)
            {
                Message = "Validation Failed";
                Errors = modelState.Keys
                        .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(key, x.ErrorMessage)))
                        .ToList();
            }
        }

        public class ValidationFailedResult : ObjectResult
        {
            public ValidationFailedResult(ModelStateDictionary modelState)
                : base(new ValidationResultModel(modelState))
            {
                StatusCode = StatusCodes.Status400BadRequest;
            }
        }
    }
}
