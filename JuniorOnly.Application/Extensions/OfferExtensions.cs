using JuniorOnly.Application.DTO.Offer;
using JuniorOnly.Domain.Entities;

namespace JuniorOnly.Application.Extensions
{
    public static class OfferExtensions
    {
        public static OfferDto ToDto(this Offer offer)
        {
            return new OfferDto
            {
                Id = offer.Id,
                Title = offer.Title,
                Description = offer.Description,
                SalaryMin = offer.SalaryMin,
                SalaryMax = offer.SalaryMax,
                CompanyId = offer.CompanyId,
                ContractType = offer.ContractType,
                ExperienceRequired = offer.ExperienceRequired,
                Location = offer.Location,
                PublishedAt = offer.PublishedAt,
                RemoteType = offer.RemoteType,
                SalaryPeriod = offer.SalaryPeriod,
                UpdatedAt = offer.UpdatedAt,
                JobSectorId = offer.JobSectorId
            };
        }

        public static Offer ToEntity(this OfferCreateDto createDto)
        {
            return new Offer
            {
                Title = createDto.Title,
                Description = createDto.Description,
                Location = createDto.Location,
                ContractType = createDto.ContractType,
                ExperienceRequired = createDto.ExperienceRequired,
                SalaryMin = createDto.SalaryMin,
                SalaryMax = createDto.SalaryMax,
                SalaryPeriod = createDto.SalaryPeriod,
                RemoteType = createDto.RemoteType,
                CompanyId = createDto.CompanyId,
                JobSectorId = createDto.JobSectorId,
                PublishedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public static void UpdateFrom(this Offer offer, OfferUpdateDto updateDto)
        {
            if (!string.IsNullOrEmpty(updateDto.Title))
            {
                offer.Title = updateDto.Title;
            }

            if (!string.IsNullOrEmpty(updateDto.Description))
            {
                offer.Description = updateDto.Description;
            }

            if (!string.IsNullOrEmpty(updateDto.Location))
            {
                offer.Location = updateDto.Location;
            }

            if (updateDto.ContractType.HasValue)
            {
                offer.ContractType = updateDto.ContractType.Value;
            }

            if (updateDto.ExperienceRequired.HasValue)
            {
                offer.ExperienceRequired = updateDto.ExperienceRequired.Value;
            }

            if (updateDto.SalaryMin.HasValue)
            {
                offer.SalaryMin = updateDto.SalaryMin.Value;
            }

            if (updateDto.SalaryMax.HasValue)
            {
                offer.SalaryMax = updateDto.SalaryMax.Value;
            }

            if (updateDto.SalaryPeriod.HasValue)
            {
                offer.SalaryPeriod = updateDto.SalaryPeriod.Value;
            }

            if (updateDto.RemoteType.HasValue)
            {
                offer.RemoteType = updateDto.RemoteType.Value;
            }

            if (updateDto.CompanyId.HasValue)
            {
                offer.CompanyId = updateDto.CompanyId.Value;
            }

            if (updateDto.JobSectorId.HasValue)
            {
                offer.JobSectorId = updateDto.JobSectorId.Value;
            }

            offer.UpdatedAt = DateTime.UtcNow;
        }
    }
}
