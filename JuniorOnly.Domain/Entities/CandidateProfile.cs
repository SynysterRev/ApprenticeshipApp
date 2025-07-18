using JuniorOnly.Domain.Enums;
using JuniorOnly.Domain.IdentityEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuniorOnly.Domain.Entities
{
    public class CandidateProfile
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public string[] DesiredJobTitles { get; set; } = Array.Empty<string>();

        [Required]
        public ExperienceLevel ExperienceLevel { get; set; }

        [StringLength(300)]
        public string? ResumeUrl { get; set; }

        [StringLength(1000)]
        public string? Bio { get; set; }

        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}