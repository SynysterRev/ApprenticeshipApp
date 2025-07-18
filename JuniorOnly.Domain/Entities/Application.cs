using JuniorOnly.Domain.IdentityEntities;
using JuniorOnly.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuniorOnly.Domain.Entities
{
    public class Application
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("Applicant")]
        public Guid ApplicantId { get; set; }

        [Required]
        [ForeignKey("Offer")]
        public Guid OfferId { get; set; }

        [Required]
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;

        [StringLength(1000)]
        public string? Message { get; set; }

        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ApplicationUser Applicant { get; set; } = null!;
        public virtual Offer Offer { get; set; } = null!;
    }
}