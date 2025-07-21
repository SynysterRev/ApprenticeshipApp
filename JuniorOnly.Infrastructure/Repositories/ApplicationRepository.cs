using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Repositories;
using JuniorOnly.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace JuniorOnly.Infrastructure.Repositories
{
    public class ApplicationRepository : BaseRepository, IApplicationRepository
    {
        public ApplicationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Application>> GetAllApplicationsAsync()
        {
            return await _dbContext.Applications.Include(a => a.Applicant).Include(a => a.Offer).ToListAsync();
        }

        public async Task<List<Application>> GetApplicationsByCandidateAsync(Guid candidateId)
        {
            return await _dbContext.Applications.Where(a => a.ApplicantId == candidateId).Include(a => a.Offer).ToListAsync();
        }

        public async Task<List<Application>> GetApplicationsByOfferAsync(Guid offerId)
        {
            return await _dbContext.Applications.Where(a => a.OfferId == offerId).Include(a => a.Applicant).ToListAsync();
        }

        public async Task<Application?> GetApplicationByIdAsync(Guid applicationId)
        {
            return await _dbContext.Applications.Include(a => a.Applicant).Include(a => a.Offer).FirstOrDefaultAsync(a => a.Id == applicationId);
        }

        public async Task<Application> AddApplicationAsync(Application application)
        {
            _dbContext.Applications.Add(application);
            await _dbContext.SaveChangesAsync();
            return application;
        }

        public async Task DeleteApplicationAsync(Application application)
        {
            _dbContext.Applications.Remove(application);
            await _dbContext.SaveChangesAsync();
        }
    }
}