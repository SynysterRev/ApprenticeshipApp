using JuniorOnly.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace JuniorOnly.Application.DTO.Offer
{
    public class OfferCreateDto
    {
        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(3000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Location { get; set; } = string.Empty;
        public ContractType ContractType { get; set; }

        [Range(0, 2)]
        public int ExperienceRequired { get; set; }

        [Range(0, int.MaxValue)]
        public int SalaryMin { get; set; }

        [Range(0, int.MaxValue)]
        public int SalaryMax { get; set; }
        public SalaryPeriod SalaryPeriod { get; set; } = SalaryPeriod.Year;
        public RemoteType RemoteType { get; set; }
        public Guid CompanyId { get; set; }
        public List<Guid> TagIds { get; set; } = new();
    }
}
