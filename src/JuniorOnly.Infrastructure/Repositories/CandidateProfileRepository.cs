using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Repositories;
using JuniorOnly.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace JuniorOnly.Infrastructure.Repositories
{
    public class CandidateProfileRepository : BaseRepository, ICandidateProfileRepository
    {
        public CandidateProfileRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<List<CandidateProfile>> GetAllProfilesAsync()
        {
            return await _dbContext.CandidateProfiles.Include(p => p.User).ToListAsync();
        }

        public async Task<CandidateProfile?> GetProfileByUserIdAsync(Guid userId)
        {
            return await _dbContext.CandidateProfiles.Include(p => p.User).FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<CandidateProfile?> GetProfileByIdAsync(Guid profileId)
        {
            return await _dbContext.CandidateProfiles.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == profileId);
        }

        public async Task<List<Favorite>> GetFavoritesByProfileAsync(Guid profileId)
        {
            return await _dbContext.Favorites.Where(f => f.CandidateProfileId == profileId).Include(f => f.JobOffer).ToListAsync();
        }

        public async Task<CandidateProfile> AddProfileAsync(CandidateProfile profile)
        {
            _dbContext.CandidateProfiles.Add(profile);
            await _dbContext.SaveChangesAsync();
            return profile;
        }

        public async Task DeleteProfileAsync(CandidateProfile profile)
        {
            _dbContext.CandidateProfiles.Remove(profile);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<CandidateProfile>> SearchProfilesAsync(string searchTerm)
        {
            return await _dbContext.CandidateProfiles
                .Where(p => p.FullName.Contains(searchTerm) || p.DesiredJobTitles.Any(t => t.Contains(searchTerm)))
                .Include(p => p.User)
                .ToListAsync();
        }
    }
}