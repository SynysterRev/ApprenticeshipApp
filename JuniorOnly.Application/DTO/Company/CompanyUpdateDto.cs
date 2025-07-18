using System;

namespace JuniorOnly.Application.DTO.Company
{
    public class CompanyUpdateDto
    {
        public string? Name { get; set; }
        public string? LogoUrl { get; set; }
        public string? Description { get; set; }
        public string? Website { get; set; }
        public bool? IsReconversionFriendly { get; set; }
        public Guid? CreatedByUserId { get; set; }
    }
}
