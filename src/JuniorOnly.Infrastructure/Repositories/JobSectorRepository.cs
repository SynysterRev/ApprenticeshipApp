using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Repositories;
using JuniorOnly.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace JuniorOnly.Infrastructure.Repositories
{
    public class JobSectorRepository : BaseRepository, IJobSectorRepository
    {
        public JobSectorRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<JobSector> AddJobSectorAsync(JobSector newJobSector)
        {
            _dbContext.JobSectors.Add(newJobSector);
            await _dbContext.SaveChangesAsync();
            return newJobSector;
        }

        public async Task DeleteJobSectorAsync(JobSector JobSector)
        {
            _dbContext.JobSectors.Remove(JobSector);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<JobSector>> GetAllJobSectorsAsync()
        {
            return await _dbContext.JobSectors.ToListAsync();
        }

        public async Task<JobSector?> GetJobSectorByIdAsync(Guid JobSectorId)
        {
            return await _dbContext.JobSectors.Where(j => j.Id == JobSectorId).FirstOrDefaultAsync();
        }

        public async Task<JobSector?> GetJobSectorByNameAsync(string normalizedName)
        {
            return await _dbContext.JobSectors.Where(j => j.Name == normalizedName).FirstOrDefaultAsync();
        }

        public async Task<List<JobSector>> GetJobSectorsByIdsAsync(IEnumerable<Guid> JobSectorIds)
        {
            if (JobSectorIds == null || !JobSectorIds.Any())
            {
                return new List<JobSector>();
            }

            return await _dbContext.JobSectors
                .Where(j => JobSectorIds.Contains(j.Id))
                .ToListAsync();
        }
    }
}
