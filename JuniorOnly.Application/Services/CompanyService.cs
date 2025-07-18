using JuniorOnly.Application.DTO.Company;
using JuniorOnly.Application.Exceptions;
using JuniorOnly.Application.Extensions;
using JuniorOnly.Application.Interfaces;
using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.IdentityEntities;
using JuniorOnly.Domain.Repositories;
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

            return company.FromEntity();
        }

        public async Task<bool> DeleteCompanyAsync(Guid id)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(id);

            if (company == null)
            {
                throw new NotFoundException($"Company with ID {id} was not found.");
            }

            return await _companyRepository.DeleteCompanyAsync(id);
        }

        public async Task<List<CompanyDto>> GetAllCompaniesAsync()
        {
            var companies = await _companyRepository.GetAllCompaniesAsync();
            return companies.Select(company => company.FromEntity()).ToList();
        }

        public async Task<CompanyDto?> GetCompanyByIdAsync(Guid id)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(id);

            if (company == null)
            {
                throw new NotFoundException($"Company with ID {id} was not found.");
            }

            return company.FromEntity();
        }

        public async Task<List<CompanyDto>> GetReconversionFriendlyCompaniesAsync()
        {
            var friendlyCompanies = await _companyRepository.GetReconversionFriendlyCompaniesAsync();

            return friendlyCompanies.Select(c => c.FromEntity()).ToList();
        }

        public async Task<CompanyDto?> UpdateCompanyAsync(Guid id, CompanyUpdateDto companyDto)
        {
            var company = await _companyRepository.GetCompanyByIdAsync(id);

            if (company == null)
            {
                throw new NotFoundException($"Company with ID {id} was not found.");
            }

            company.UpdateFrom(companyDto);

            await _companyRepository.UpdateCompanyAsync(company);

            return company.FromEntity();
        }
    }
}
