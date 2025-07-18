using JuniorOnly.Domain.IdentityEntities;
using JuniorOnly.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuniorOnly.Domain.Entities
{
    public class Offer
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(3000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public ContractType ContractType { get; set; }

        [Required]
        [Range(0, 2, ErrorMessage = "Experience required must be 2 years or less")]
        public int ExperienceRequired { get; set; }

        [Required]
        public RemoteType RemoteType { get; set; }

        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
