using JuniorOnly.Application.DTO.Company;
using JuniorOnly.Domain.Entities;

namespace JuniorOnly.Application.Extensions
{
    public static class CompanyExtensions
    {
        public static CompanyDto ToDto(this Company company)
        {
            return new CompanyDto()
            {
                Id = company.Id,
                Name = company.Name,
                LogoUrl = company.LogoUrl,
                Description = company.Description,
                Website = company.Website,
                IsReconversionFriendly = company.IsReconversionFriendly,
                CreatedByUserId = company.CreatedByUserId,
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt
            };
        }

        public static Company ToEntity(this CompanyCreateDto createDto)
        {
            return new Company()
            {
                Name = createDto.Name,
                LogoUrl = createDto.LogoUrl,
                Description = createDto.Description,
                Website = createDto.Website,
                IsReconversionFriendly = createDto.IsReconversionFriendly,
                CreatedByUserId = createDto.CreatedByUserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        public static void UpdateFrom(this Company company, CompanyUpdateDto updateDto)
        {
            if (!string.IsNullOrEmpty(updateDto.Name))
            {
                company.Name = updateDto.Name;
            }
            if (updateDto.LogoUrl != null)
            {
                company.LogoUrl = updateDto.LogoUrl;
            }
            if (updateDto.Description != null)
            {
                company.Description = updateDto.Description;
            }
            if (updateDto.Website != null)
            {
                company.Website = updateDto.Website;
            }
            if (updateDto.IsReconversionFriendly.HasValue)
            {
                company.IsReconversionFriendly = updateDto.IsReconversionFriendly.Value;
            }
            company.UpdatedAt = DateTime.UtcNow;
        }
    }
}
