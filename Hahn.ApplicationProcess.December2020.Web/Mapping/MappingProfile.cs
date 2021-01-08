using AutoMapper;
using Hahn.ApplicationProcess.December2020.Domain.Models;
using Hahn.ApplicationProcess.December2020.Web.Models.Request;
using Hahn.ApplicationProcess.December2020.Web.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            // CreateMap<Applicant, ApplicantRequest>();
            // Resource to Domain
            //CreateMap<ApplicantResponse, Applicant>();

            CreateMap<Applicant, ApplicantResponse>();
            CreateMap<ApplicantRequest, Applicant>();
        }
    }
}
