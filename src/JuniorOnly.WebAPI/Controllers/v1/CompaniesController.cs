using Microsoft.AspNetCore.Mvc;
using JuniorOnly.Controllers;
using Asp.Versioning;
using JuniorOnly.Application.Interfaces;
using JuniorOnly.Application.DTO.Company;
using JuniorOnly.Application.DTO.Offer;

namespace JuniorOnly.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CompaniesController : CustomControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IOfferService _offerService;
        private readonly ILogger<CompaniesController> _logger;

        public CompaniesController(ICompanyService companyService, ILogger<CompaniesController> logger, IOfferService offerService)
        {
            _companyService = companyService;
            _logger = logger;
            _offerService = offerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyDto>>> GetAll()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetById(Guid id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            return Ok(company);
        }

        [HttpGet("{companyId}/offers")]
        public async Task<ActionResult<List<OfferDto>>> GetOffers(Guid companyId)
        {
            var offers = await _offerService.GetOffersByCompanyAsync(companyId, 1);
            return Ok(offers);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CompanyDto>> Update(Guid id, CompanyUpdateDto updateDto)
        {
            var company = await _companyService.UpdateCompanyAsync(id, updateDto);
            _logger.LogInformation("Company updated with ID {CompanyId}", id);
            return Ok(company);
        }

        [HttpPost]
        public async Task<ActionResult<CompanyDto>> Create(CompanyCreateDto createDto)
        {
            var newCompany = await _companyService.CreateCompanyAsync(createDto);
            _logger.LogInformation("Company created with ID {CompanyId}", newCompany.Id);
            return CreatedAtAction(nameof(GetById), new { id = newCompany.Id }, newCompany);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _companyService.DeleteCompanyAsync(id);
            _logger.LogInformation("Company delete with ID {CompanyId}", id);
            return NoContent();
        }
    }
}
