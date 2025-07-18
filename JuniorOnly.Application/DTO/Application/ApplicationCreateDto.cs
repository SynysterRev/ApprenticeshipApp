using System;
using JuniorOnly.Domain.Enums;
using JuniorOnly.Domain.Entities;

namespace JuniorOnly.Application.DTO.Application
{
    public class ApplicationCreateDto
    {
        public Guid ApplicantId { get; set; }
        public Guid OfferId { get; set; }
        public string? Message { get; set; }
    }
}
