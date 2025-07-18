using JuniorOnly.Application.DTO.CandidateProfile;
using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Enums;

namespace JuniorOnly.Application.Extensions
{
    public static class CandidateProfileExtensions
    {
        public static CandidateProfileDto FromEntity(this CandidateProfile profile)
        {
            return new CandidateProfileDto
            {
                Id = profile.Id,
                UserId = profile.UserId,
                FullName = profile.FullName,
                Location = profile.Location,
                DesiredJobTitles = profile.DesiredJobTitles,
                ExperienceLevel = profile.ExperienceLevel,
                ResumeUrl = profile.ResumeUrl,
                Bio = profile.Bio
            };
        }

        public static CandidateProfile ToEntity(this CandidateProfileCreateDto createDto)
        {
            return new CandidateProfile
            {
                FullName = createDto.FullName,
                Location = createDto.Location,
                DesiredJobTitles = createDto.DesiredJobTitles,
                ExperienceLevel = createDto.ExperienceLevel,
                ResumeUrl = createDto.ResumeUrl,
                Bio = createDto.Bio
            };
        }

        public static void UpdateFrom(this CandidateProfile profile, CandidateProfileUpdateDto updateDto)
        {
            if (!string.IsNullOrEmpty(updateDto.FullName))
            {
                profile.FullName = updateDto.FullName;
            }
            if (!string.IsNullOrEmpty(updateDto.Location))
            {
                profile.Location = updateDto.Location;
            }
            if (updateDto.DesiredJobTitles != null)
            {
                profile.DesiredJobTitles = updateDto.DesiredJobTitles;
            }
            if (updateDto.ExperienceLevel.HasValue)
            {
                profile.ExperienceLevel = updateDto.ExperienceLevel.Value;
            }
            if (updateDto.ResumeUrl != null)
            {
                profile.ResumeUrl = updateDto.ResumeUrl;
            }
            if (updateDto.Bio != null)
            {
                profile.Bio = updateDto.Bio;
            }
        }
    }
}
