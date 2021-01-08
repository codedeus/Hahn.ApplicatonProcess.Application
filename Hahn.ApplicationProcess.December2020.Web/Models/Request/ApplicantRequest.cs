using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Web.Models.Request
{
    public class ApplicantRequest
    {
        /// <summary>
        /// Applicant's given name
        /// </summary>
        /// <example>David</example>
        public string Name { get; set; }
        /// <summary>
        /// Applicant's Family Name
        /// </summary>
        /// <example>Beller</example>
        public string FamilyName { get; set; }
        /// <summary>
        /// Applicant's contact address
        /// </summary>
        /// <example>1087 Aviation Way</example>
        public string Address { get; set; }
        /// <summary>
        /// Applicant's country of origin
        /// </summary>
        /// <example>Germany</example>
        public string CountryOfOrigin { get; set; }

        /// <summary>
        /// Applicant's email address
        /// </summary>
        /// <example>davidbeller@gmail.com</example>
        [EmailAddress]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Applicant's age. 
        /// Must be greater than or equal to 20 but less than or equal to 60
        /// </summary>
        /// <example>25</example>
        public int Age { get; set; }
        /// <summary>
        /// Status indicating whether applicant is hired or not
        /// </summary>
        /// <example>true</example>
        public bool Hired { get; set; } = false;
    }
}
