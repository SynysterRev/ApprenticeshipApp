using JuniorOnly.Domain.Enums;

namespace JuniorOnly.Application.DTO.CandidateProfile
{
    public class CandidateProfileUpdateDto
    {
        public string? FullName { get; set; }
        public string? Location { get; set; }
        public string[]? DesiredJobTitles { get; set; }
        public ExperienceLevel? ExperienceLevel { get; set; }
        public string? ResumeUrl { get; set; }
        public string? Bio { get; set; }
    }
}
