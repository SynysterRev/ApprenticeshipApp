using JuniorOnly.Domain.Enums;

namespace JuniorOnly.Application.DTO.CandidateProfile
{
    public class CandidateProfileDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string[] DesiredJobTitles { get; set; } = Array.Empty<string>();
        public ExperienceLevel ExperienceLevel { get; set; }
        public string? ResumeUrl { get; set; }
        public string? Bio { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
