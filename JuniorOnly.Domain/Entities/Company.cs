using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JuniorOnly.Domain.IdentityEntities;

namespace JuniorOnly.Domain.Entities
{
    public class Company
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(300)]
        public string? LogoUrl { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [StringLength(200)]
        public string? Website { get; set; }

        public bool IsReconversionFriendly { get; set; }

        [Required]
        [ForeignKey("CreatedByUser")]
        public Guid CreatedByUserId { get; set; }
        public virtual ApplicationUser CreatedByUser { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Offer> JobOffers { get; set; } = new List<Offer>();
    }
}