using Asp.Versioning;
using JuniorOnly.Application.DTO.Application;
using JuniorOnly.Application.Interfaces;
using JuniorOnly.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JuniorOnly.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ApplicationsController : CustomControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly ILogger<ApplicationsController> _logger;

        public ApplicationsController(IApplicationService applicationService, ILogger<ApplicationsController> logger)
        {
            _applicationService = applicationService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<ApplicationDto>>> GetAll()
        {
            var applications = await _applicationService.GetAllApplicationsAsync();
            return Ok(applications);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationDto>> GetById(Guid id)
        {
            var applications = await _applicationService.GetApplicationByIdAsync(id);
            return Ok(applications);
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationDto>> Create([FromBody] ApplicationCreateDto createDto)
        {
            var newApplication = await _applicationService.CreateApplicationAsync(createDto);
            _logger.LogInformation("Application created with ID {applicationID}", newApplication.Id);
            return CreatedAtAction(nameof(GetById), new { id = newApplication.Id}, newApplication);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApplicationDto>> Update(Guid id, [FromBody] ApplicationUpdateDto updateDto)
        {
            var updatedApplication = await _applicationService.UpdateApplicationAsync(id, updateDto);
            _logger.LogInformation("Application updated with ID {applicationID}", updatedApplication.Id);
            return Ok(updatedApplication);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _applicationService.DeleteApplicationAsync(id);
            return NoContent();
        }
    }
}
