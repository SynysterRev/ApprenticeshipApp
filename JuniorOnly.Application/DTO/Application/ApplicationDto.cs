using System;
using JuniorOnly.Domain.Enums;

namespace JuniorOnly.Application.DTO.Application
{
    public class ApplicationDto
    {
        public Guid Id { get; set; }
        public Guid ApplicantId { get; set; }
        public Guid OfferId { get; set; }
        public ApplicationStatus Status { get; set; }
        public string? Message { get; set; }
        public DateTime ApplieddAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
