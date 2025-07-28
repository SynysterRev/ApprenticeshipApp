using JuniorOnly.Application.DTO.Company;
using JuniorOnly.Application.Exceptions;
using JuniorOnly.Application.Extensions;
using JuniorOnly.Application.Interfaces;
using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.IdentityEntities;
using JuniorOnly.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace JuniorOnly.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public CompanyService(ICompanyRepository companyRepository, UserManager<ApplicationUser> userManager)
        {
            _companyRepository = companyRepository;
            _userManager = userManager;
        }

        [Authorize(Roles = "Recruiter, Admin" )]
        public async Task<CompanyDto> CreateCompanyAsync(CompanyCreateDto companyDto)
        {
            var user = await _userManager.FindByIdAsync(companyDto.CreatedByUserId.ToString());
            if (user == null)
            {
                throw new NotFoundException($"User with id {companyDto.CreatedByUserId} does not exist.");
            }
            Company company = companyDto.ToEntity();
            company.Id = Guid.NewGuid();

            await _companyRepository.AddCompanyAsync(company);

            return company.ToDto();
        }

        [Authorize(Roles = "Recruiter, Admin")]
        public async Task DeleteCompanyAsync(Guid companyId)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(companyId);

            if (company == null)
            {
                throw new NotFoundException($"Company with ID {companyId} not found.");
            }

            await _companyRepository.DeleteCompanyAsync(company);
        }

        public async Task<List<CompanyDto>> GetAllCompaniesAsync()
        {
            var companies = await _companyRepository.GetAllCompaniesAsync();
            return companies.Select(company => company.ToDto()).ToList();
        }

        public async Task<CompanyDto> GetCompanyByIdAsync(Guid companyId)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(companyId);

            if (company == null)
            {
                throw new NotFoundException($"Company with ID {companyId} was not found.");
            }

            return company.ToDto();
        }

        public async Task<List<CompanyDto>> GetReconversionFriendlyCompaniesAsync()
        {
            var friendlyCompanies = await _companyRepository.GetReconversionFriendlyCompaniesAsync();

            return friendlyCompanies.Select(c => c.ToDto()).ToList();
        }

        [Authorize(Roles = "Recruiter, Admin")]
        public async Task<CompanyDto> UpdateCompanyAsync(Guid companyId, CompanyUpdateDto companyDto)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(companyId);

            if (company == null)
            {
                throw new NotFoundException($"Company with ID {companyId} was not found.");
            }

            company.UpdateFrom(companyDto);

            await _companyRepository.SaveChangesAsync();

            return company.ToDto();
        }
    }
}
