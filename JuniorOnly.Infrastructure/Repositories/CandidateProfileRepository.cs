using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Repositories;
using JuniorOnly.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace JuniorOnly.Infrastructure.Repositories
{
    public class CandidateProfileRepository : ICandidateProfileRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CandidateProfileRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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

        public async Task<CandidateProfile?> UpdateProfileAsync(CandidateProfile updatedProfile)
        {
            var foundProfile = await _dbContext.CandidateProfiles.FindAsync(updatedProfile.Id);
            if (foundProfile == null)
            {
                return null;
            }
            _dbContext.Entry(foundProfile).CurrentValues.SetValues(updatedProfile);
            await _dbContext.SaveChangesAsync();
            return foundProfile;
        }

        public async Task<bool> DeleteProfileAsync(Guid profileId)
        {
            var foundProfile = await _dbContext.CandidateProfiles.FindAsync(profileId);
            if (foundProfile == null)
            {
                return false;
            }
            _dbContext.CandidateProfiles.Remove(foundProfile);
            await _dbContext.SaveChangesAsync();
            return true;
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