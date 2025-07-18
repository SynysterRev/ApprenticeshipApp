using System;
using System.Collections.Generic;
using JuniorOnly.Domain.Enums;

namespace JuniorOnly.Application.DTO.Offer
{
    public class OfferUpdateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public ContractType? ContractType { get; set; }
        public int? ExperienceRequired { get; set; }
        public RemoteType? RemoteType { get; set; }
        public Guid? CompanyId { get; set; }
        public List<Guid>? TagIds { get; set; }
    }
}
