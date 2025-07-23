using Microsoft.AspNetCore.Mvc;
using JuniorOnly.Controllers;
using Asp.Versioning;
using JuniorOnly.Application.Interfaces;
using JuniorOnly.Application.DTO.Company;

namespace JuniorOnly.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CompaniesController : CustomControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ILogger<CompaniesController> _logger;

        public CompaniesController(ICompanyService companyService, ILogger<CompaniesController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        // GET: api/v1/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
        {
            var companies = await _companyService.GetAllCompaniesAsync();
            return Ok(companies);
        }

        // GET: api/v1/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(Guid id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);
            return Ok(company);
        }

        // PUT: api/v1/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<CompanyDto>> PutCompany(Guid id, CompanyUpdateDto updateDto)
        {
            var company = await _companyService.UpdateCompanyAsync(id, updateDto);
            return Ok(company);
        }

        // POST: api/v1/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> PostCompany(CompanyCreateDto createDto)
        {
            var newCompany = await _companyService.CreateCompanyAsync(createDto);
            _logger.LogInformation("Company created with ID {CompanyId}", newCompany.Id);
            return CreatedAtAction("GetCompany", new { id = newCompany.Id }, newCompany);
        }

        // DELETE: api/v1/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            await _companyService.DeleteCompanyAsync(id);

            return NoContent();
        }
    }
}
