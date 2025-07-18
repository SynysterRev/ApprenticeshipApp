using System;
using JuniorOnly.Domain.Enums;

namespace JuniorOnly.Application.DTO.CandidateProfile
{
    public class CandidateProfileCreateDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string[] DesiredJobTitles { get; set; } = Array.Empty<string>();
        public ExperienceLevel ExperienceLevel { get; set; }
        public string? ResumeUrl { get; set; }
        public string? Bio { get; set; }
    }
}
