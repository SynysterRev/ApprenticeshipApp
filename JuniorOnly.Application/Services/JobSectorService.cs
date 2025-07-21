using JuniorOnly.Application.DTO.JobSector;
using JuniorOnly.Application.DTO.Tag;
using JuniorOnly.Application.Exceptions;
using JuniorOnly.Application.Extensions;
using JuniorOnly.Application.Interfaces;
using JuniorOnly.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace JuniorOnly.Application.Services
{
    public class JobSectorService : IJobSectorService
    {
        private readonly IJobSectorRepository _jobSectorRepository;

        public JobSectorService(IJobSectorRepository jobSectorRepository)
        {
            _jobSectorRepository = jobSectorRepository;
        }

        public async Task<JobSectorDto> CreateSectorAsync(JobSectorCreateDto createDto)
        {
            string normalizedName = createDto.Name.Trim().ToLower();

            var alreadyExist = await _jobSectorRepository.GetJobSectorByNameAsync(normalizedName);

            if (alreadyExist != null)
            {
                throw new ValidationException($"The job sector {normalizedName} already exists");
            }

            var jobSector = createDto.ToEntity();
            jobSector.Id = Guid.NewGuid();

            await _jobSectorRepository.AddJobSectorAsync(jobSector);
            return jobSector.ToDto();
        }

        public async Task DeleteSectorAsync(Guid sectorId)
        {
            var jobSector = await _jobSectorRepository.GetJobSectorByIdAsync(sectorId);

            if (jobSector == null)
            {
                throw new NotFoundException($"Job sector with ID {sectorId} not found");
            }

            await _jobSectorRepository.DeleteJobSectorAsync(jobSector);

        }

        public async Task<List<JobSectorDto>> GetAllSectorsAsync()
        {
            var jobSectors = await _jobSectorRepository.GetAllJobSectorsAsync();
            return jobSectors.Select(j => j.ToDto()).ToList();
        }

        public async Task<JobSectorDto?> GetSectorByIdAsync(Guid sectorId)
        {
            var jobSector = await _jobSectorRepository.GetJobSectorByIdAsync(sectorId);

            if (jobSector == null)
            {
                throw new NotFoundException($"Job sector with ID {sectorId} not found");
            }

            return jobSector.ToDto();
        }

        public async Task<JobSectorDto> UpdateSectorAsync(Guid sectorId, JobSectorUpdateDto updateDto)
        {
            var jobSector = await _jobSectorRepository.GetJobSectorByIdAsync(sectorId);

            if (jobSector == null)
            {
                throw new NotFoundException($"Job sector with ID {sectorId} not found");
            }

            jobSector.UpdateFrom(updateDto);

            await _jobSectorRepository.SaveChangesAsync();

            return jobSector.ToDto();
        }
    }
}
