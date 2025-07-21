using System.ComponentModel.DataAnnotations;

namespace JuniorOnly.Application.DTO.Company
{
    public class CompanyCreateDto
    {
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
        public Guid CreatedByUserId { get; set; }
    }
}
