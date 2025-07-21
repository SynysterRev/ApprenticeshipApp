using JuniorOnly.Domain.Enums;

namespace JuniorOnly.Application.DTO.Offer
{
    public class OfferDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public ContractType ContractType { get; set; }
        public int ExperienceRequired { get; set; }
        public int SalaryMin { get; set; }
        public int SalaryMax { get; set; }
        public SalaryPeriod SalaryPeriod { get; set; } = SalaryPeriod.Year;
        public RemoteType RemoteType { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid CompanyId { get; set; }
        public List<Guid> TagIds { get; set; } = new();

        public string SalaryRangeDisplay
        {
            get
            {
                string period = SalaryPeriod switch
                {
                    SalaryPeriod.Year => "/year",
                    SalaryPeriod.Month => "/month",
                    SalaryPeriod.Day => "/day",
                    _ => string.Empty
                };
                return $"{SalaryMin:N0} - {SalaryMax:N0} {period}";
            }
        }
    }
}
