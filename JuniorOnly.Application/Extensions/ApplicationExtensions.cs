using JuniorOnly.Application.DTO.Application;
using JuniorOnly.Domain.Enums;

namespace JuniorOnly.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static ApplicationDto ToDto(this Domain.Entities.Application application)
        {
            return new ApplicationDto
            {
                Id = application.Id,
                ApplicantId = application.ApplicantId,
                OfferId = application.OfferId,
                Status = application.Status,
                ApplieddAt = application.AppliedAt,
                Message = application.Message,
                UpdatedAt = application.UpdatedAt,
            };
        }

        public static Domain.Entities.Application ToEntity(this ApplicationCreateDto createDto)
        {
            return new Domain.Entities.Application
            {
                ApplicantId = createDto.ApplicantId,
                Message = createDto.Message,
                OfferId = createDto.OfferId,
                Status = ApplicationStatus.Pending,
                AppliedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
        }

        public static void UpdateFrom(this Domain.Entities.Application application, ApplicationUpdateDto updateDto)
        {
            if (updateDto.Status.HasValue)
            {
                application.Status = updateDto.Status.Value;
            }
            if (!string.IsNullOrEmpty(updateDto.Message))
            {
                application.Message = updateDto.Message;
            }
            application.UpdatedAt = DateTime.UtcNow;
        }
    }
}
