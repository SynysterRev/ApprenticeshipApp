using JuniorOnly.Domain.Enums;

namespace JuniorOnly.Application.DTO.Application
{
    public class ApplicationUpdateDto
    {
        public ApplicationStatus? Status { get; set; }
        public string? Message { get; set; }
    }
}
