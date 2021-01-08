using Hahn.ApplicationProcess.December2020.Domain.Models;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Domain.Repository
{
    public interface IApplicantRepository : IRepository
    {
        Task<Applicant> GetApplicantByEmail(string emailAddress);
    }
}
