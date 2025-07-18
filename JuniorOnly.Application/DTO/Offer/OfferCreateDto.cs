using System;
using System.Collections.Generic;
using JuniorOnly.Domain.Enums;

namespace JuniorOnly.Application.DTO.Offer
{
    public class OfferCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public ContractType ContractType { get; set; }
        public int ExperienceRequired { get; set; }
        public RemoteType RemoteType { get; set; }
        public Guid CompanyId { get; set; }
        public List<Guid> TagIds { get; set; } = new();
    }
}
