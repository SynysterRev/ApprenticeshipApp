using JuniorOnly.Domain.Entities;

namespace JuniorOnly.Domain.Repositories
{
    public interface IJobSectorRepository : IBaseRepository
    {
        /// <summary>
        /// Get all sectors in the database.
        /// </summary>
        /// <returns>A list of all sectors.</returns>
        public Task<List<JobSector>> GetAllJobSectorsAsync();

        /// <summary>
        /// Get the sector matching the given ID if any.
        /// </summary>
        /// <param name="sectorId">The sector ID.</param>
        /// <returns>The sector if found, or null.</returns>
        public Task<JobSector?> GetJobSectorByIdAsync(Guid sectorId);

        /// <summary>
        /// Get all sectors matching the given list of IDs.
        /// </summary>
        /// <param name="sectorIds">The list of sectors IDs.</param>
        /// <returns>A list of found sectors.</returns>
        public Task<List<JobSector>> GetJobSectorsByIdsAsync(IEnumerable<Guid> sectorIds);

        /// <summary>
        /// Get the sector matching the name if any.
        /// </summary>
        /// <param name="normalizedName">The sector name.</param>
        /// <returns>The sector if found, or null.</returns>
        public Task<JobSector?> GetJobSectorByNameAsync(string normalizedName);

        /// <summary>
        /// Add a new sector in the database.
        /// </summary>
        /// <param name="newSector">The sector to add.</param>
        /// <returns>The new sector added.</returns>
        public Task<JobSector> AddJobSectorAsync(JobSector newSector);

        /// <summary>
        /// Delete the sector with the matching ID.
        /// </summary>
        /// <param name="sector">The sector object to delete.</param>
        public Task DeleteJobSectorAsync(JobSector sector);
    }
}
