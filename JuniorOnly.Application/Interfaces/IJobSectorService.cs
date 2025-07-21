using JuniorOnly.Application.DTO.JobSector;
using JuniorOnly.Application.DTO.Tag;

namespace JuniorOnly.Application.Interfaces
{
    public interface IJobSectorService
    {
        /// <summary>
        /// Gets all sectors.
        /// </summary>
        /// <returns>List of SectorDto representing all sectors.</returns>
        public Task<List<JobSectorDto>> GetAllSectorsAsync();

        /// <summary>
        /// Gets a sector by its unique identifier.
        /// </summary>
        /// <param name="id">Guid of the sector.</param>
        /// <returns>SectorDto if found, otherwise throw a not found exception.</returns>
        public Task<JobSectorDto?> GetSectorByIdAsync(Guid sectorId);

        /// <summary>
        /// Creates a new sector.
        /// </summary>
        /// <param name="name">Name of the sector.</param>
        /// <returns>The created SectorDto.</returns>
        public Task<JobSectorDto> CreateSectorAsync(JobSectorCreateDto createDto);

        /// <summary>
        /// Deletes a sector.
        /// </summary>
        /// <param name="sectorId">Guid of the sector to delete.</param>
        public Task DeleteSectorAsync(Guid sectorId);

        /// <summary>
        /// Creates a new sector.
        /// </summary>
        /// <param name="sectorId">The guid of the sector.</param>
        /// <param name="updateDto">The JobSectorUpdateDto containing the data.</param>
        /// <returns>The updated SectorDto.</returns>
        public Task<JobSectorDto> UpdateSectorAsync(Guid sectorId, JobSectorUpdateDto updateDto);
    }
}