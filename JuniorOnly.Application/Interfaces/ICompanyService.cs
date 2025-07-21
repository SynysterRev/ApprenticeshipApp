using JuniorOnly.Application.DTO.Company;

namespace JuniorOnly.Application.Interfaces
{
    public interface ICompanyService
    {
        /// <summary>
        /// Gets all companies.
        /// </summary>
        /// <returns>List of CompanyDto representing all companies.</returns>
        public Task<List<CompanyDto>> GetAllCompaniesAsync();

        /// <summary>
        /// Gets a company by its unique identifier.
        /// </summary>
        /// <param name="id">Guid of the company.</param>
        /// <returns>CompanyDto if found, otherwise null.</returns>
        public Task<CompanyDto?> GetCompanyByIdAsync(Guid companyId);

        /// <summary>
        /// Creates a new company.
        /// </summary>
        /// <param name="companyDto">CompanyCreateDto containing company data.</param>
        /// <returns>The created CompanyDto.</returns>
        public Task<CompanyDto> CreateCompanyAsync(CompanyCreateDto companyDto);

        /// <summary>
        /// Updates an existing company.
        /// </summary>
        /// <param name="id">Guid of the company to update.</param>
        /// <param name="companyDto">CompanyUpdateDto with updated data.</param>
        /// <returns>The updated CompanyDto if found, otherwise null.</returns>
        public Task<CompanyDto?> UpdateCompanyAsync(Guid companyId, CompanyUpdateDto companyDto);

        /// <summary>
        /// Deletes a company.
        /// </summary>
        /// <param name="id">Guid of the company to delete.</param>
        public Task DeleteCompanyAsync(Guid companyId);

        /// <summary>
        /// Gets all companies that are reconversion friendly.
        /// </summary>
        /// <returns>List of CompanyDto that are reconversion friendly.</returns>
        public Task<List<CompanyDto>> GetReconversionFriendlyCompaniesAsync();
    }
}