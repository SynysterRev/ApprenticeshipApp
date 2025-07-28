using JuniorOnly.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace JuniorOnly.Application.DTO.CandidateProfile
{
    public class CandidateProfileUpdateDto
    {
        [StringLength(150)]
        public string? FullName { get; set; }

        [StringLength(200)]
        public string? Location { get; set; }
        public string[]? DesiredJobTitles { get; set; }
        public ExperienceLevel? ExperienceLevel { get; set; }

        [StringLength(300)]
        public string? ResumeUrl { get; set; }

        [StringLength(1000)]
        public string? Bio { get; set; }
    }
}
