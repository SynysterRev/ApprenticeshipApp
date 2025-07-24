using JuniorOnly.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace JuniorOnly.Application.DTO.CandidateProfile
{
    public class CandidateProfileCreateDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string[] DesiredJobTitles { get; set; } = Array.Empty<string>();

        [Required]
        public ExperienceLevel ExperienceLevel { get; set; }

        [StringLength(300)]
        public string? ResumeUrl { get; set; }

        [StringLength(1000)]
        public string? Bio { get; set; }
    }
}
