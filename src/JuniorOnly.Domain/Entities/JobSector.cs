using System.ComponentModel.DataAnnotations;

namespace JuniorOnly.Domain.Entities
{
    public class JobSector
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
    }
}