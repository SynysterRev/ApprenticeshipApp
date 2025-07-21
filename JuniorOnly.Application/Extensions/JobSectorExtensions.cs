using JuniorOnly.Application.DTO.JobSector;
using JuniorOnly.Application.DTO.Tag;
using JuniorOnly.Domain.Entities;

namespace JuniorOnly.Application.Extensions
{
    public static class JobSectorExtensions
    {
        public static JobSectorDto ToDto(this JobSector jobSector)
        {
            return new JobSectorDto
            {
                Id = jobSector.Id,
                IsActive = jobSector.IsActive,
                Name = jobSector.Name,
                CreatedAt = jobSector.CreatedAt,
                UpdatedAt = jobSector.UpdatedAt,
            };
        }

        public static JobSector ToEntity(this JobSectorCreateDto createDto)
        {
            return new JobSector
            {
                Name = createDto.Name,
                IsActive = createDto.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
        }

        public static void UpdateFrom(this JobSector jobSector, JobSectorUpdateDto updateDto)
        {
            if (!string.IsNullOrEmpty(updateDto.Name))
            {
                jobSector.Name = updateDto.Name;
            }

            if (updateDto.IsActive.HasValue)
            {
                jobSector.IsActive = updateDto.IsActive.Value;
            }

            jobSector.UpdatedAt = DateTime.UtcNow;
        }
    }
}
