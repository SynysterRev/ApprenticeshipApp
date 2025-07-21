using JuniorOnly.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace JuniorOnly.Application.DTO.Offer
{
    public class OfferUpdateDto
    {
        [StringLength(150)]
        public string? Title { get; set; }

        [StringLength(3000)]
        public string? Description { get; set; }

        [StringLength(200)]
        public string? Location { get; set; }
        public ContractType? ContractType { get; set; }

        [Range(0, 2)]
        public int? ExperienceRequired { get; set; }
        public int? SalaryMin { get; set; }
        public int? SalaryMax { get; set; }
        public SalaryPeriod? SalaryPeriod { get; set; }
        public RemoteType? RemoteType { get; set; }
        public Guid? CompanyId { get; set; }
        public List<Guid>? TagIds { get; set; }
    }


}
