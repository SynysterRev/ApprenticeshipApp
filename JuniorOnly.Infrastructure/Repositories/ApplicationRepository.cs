using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Repositories;
using JuniorOnly.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace JuniorOnly.Infrastructure.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ApplicationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public async Task<Application?> UpdateApplicationAsync(Application updatedApplication)
        {
            var foundApplication = await _dbContext.Applications.FindAsync(updatedApplication.Id);
            if (foundApplication == null)
            {
                return null;
            }
            _dbContext.Entry(foundApplication).CurrentValues.SetValues(updatedApplication);
            await _dbContext.SaveChangesAsync();
            return foundApplication;
        }

        public async Task<bool> DeleteApplicationAsync(Guid applicationId)
        {
            var foundApplication = await _dbContext.Applications.FindAsync(applicationId);
            if (foundApplication == null)
            {
                return false;
            }
            _dbContext.Applications.Remove(foundApplication);
            await _dbContext.SaveChangesAsync();           
            return true;
        }
    }
}