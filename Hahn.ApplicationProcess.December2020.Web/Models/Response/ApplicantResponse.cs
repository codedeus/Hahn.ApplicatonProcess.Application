using Hahn.ApplicationProcess.December2020.Web.Models.Request;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Web.Models.Response
{
    public class ApplicantResponse: ApplicantRequest
    {
        /// <summary>
        /// Applicant's Unique ID
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
    }
}
