using Hahn.ApplicationProcess.December2020.Domain.Models;
using Hahn.ApplicationProcess.December2020.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Data.Repository
{
    public class ApplicantRepository : EfRepository, IApplicantRepository
    {
        public ApplicantRepository(ApplicationDbContext context)
            : base(context)
        { }

        public Task<Applicant> GetApplicantByEmail(string emailAddress)
        {
            return ApplicationDbContext.Applicants
                .SingleOrDefaultAsync(a => a.EmailAddress == emailAddress);
        }

        private ApplicationDbContext ApplicationDbContext
        {
            get { return _dbContext; }
        }
    }
}
