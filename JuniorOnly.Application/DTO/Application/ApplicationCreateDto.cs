using System;

namespace JuniorOnly.Application.DTO.Application
{
    public class ApplicationCreateDto
    {
        public Guid ApplicantId { get; set; }
        public Guid OfferId { get; set; }
        public string? Message { get; set; }
        public Guid? CandidateProfileId { get; set; }
    }
}
