using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuniorOnly.Domain.Entities
{
    public class Favorite
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("CandidateProfile")]
        public Guid CandidateProfileId { get; set; }
        public virtual CandidateProfile CandidateProfile { get; set; } = null!;

        [Required]
        [ForeignKey("JobOffer")]
        public Guid JobOfferId { get; set; }
        public virtual Offer JobOffer { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}