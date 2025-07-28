using Asp.Versioning;
using JuniorOnly.Application.DTO.Application;
using JuniorOnly.Application.DTO.CandidateProfile;
using JuniorOnly.Application.Interfaces;
using JuniorOnly.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace JuniorOnly.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CandidateProfilesController : CustomControllerBase
    {
        private readonly ICandidateProfileService _profileService;
        private readonly IApplicationService _applicationService;
        private readonly ILogger<CandidateProfilesController> _logger;

        public CandidateProfilesController(ICandidateProfileService profileService, IApplicationService applicationService, ILogger<CandidateProfilesController> logger)
        {
            _profileService = profileService;
            _applicationService = applicationService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<CandidateProfileDto>>> GetAll()
        {
            var profiles = await _profileService.GetAllProfilesAsync();
            return Ok(profiles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CandidateProfileDto>> GetById(Guid id)
        {
            var profiles = await _profileService.GetProfileByIdAsync(id);
            return Ok(profiles);
        }

        [HttpGet("{candidateId}/applications")]
        public async Task<ActionResult<ApplicationDto>> GetApplications(Guid candidateId)
        {
            var applications = await _applicationService.GetApplicationsByCandidateAsync(candidateId);
            return Ok(applications);
        }

        [HttpPost]
        public async Task<ActionResult<CandidateProfileDto>> Create([FromBody] CandidateProfileCreateDto createDto)
        {
            var newProfile = await _profileService.CreateProfileAsync(createDto);
            _logger.LogInformation("Candidate profile created with ID {candidateID}", newProfile.Id);
            return CreatedAtAction(nameof(GetById), new { id = newProfile.Id }, newProfile);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CandidateProfileDto>> Update(Guid id, [FromBody] CandidateProfileUpdateDto updateDto)
        {
            var updatedProfile = await _profileService.UpdateProfileAsync(id, updateDto);
            _logger.LogInformation("Candidate profile updated with ID {candidateID}", id);
            return Ok(updatedProfile);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _profileService.DeleteProfileAsync(id);
            _logger.LogInformation("Candidate profile deleted with ID {candidateID}", id);
            return NoContent();
        }
    }
}
