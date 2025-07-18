using System.ComponentModel.DataAnnotations;

namespace JuniorOnly.Domain.Entities
{
    public class Tag
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
    }
}