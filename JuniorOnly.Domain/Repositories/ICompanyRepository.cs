using JuniorOnly.Domain.Entities;

namespace JuniorOnly.Domain.Repositories
{
    public interface ICompanyRepository
    {
        /// <summary>
        /// Get all companies.
        /// </summary>
        /// <returns>A list of all companies.</returns>
        public Task<List<Company>> GetAllCompaniesAsync();

        /// <summary>
        /// Get a specific company by its ID.
        /// </summary>
        /// <param name="companyId">The ID of the company.</param>
        /// <returns>The company if found; otherwise, null.</returns>
        public Task<Company?> GetCompanyByIdAsync(Guid companyId);

        /// <summary>
        /// Add a new company.
        /// </summary>
        /// <param name="company">The company to add.</param>
        /// <returns>The created company.</returns>
        public Task<Company> AddCompanyAsync(Company company);

        /// <summary>
        /// Update an existing company.
        /// </summary>
        /// <param name="updatedCompany">The updated company data.</param>
        /// <returns>The updated company if successful; otherwise, null.</returns>
        public Task<Company?> UpdateCompanyAsync(Company updatedCompany);

        /// <summary>
        /// Delete a company by its ID.
        /// </summary>
        /// <param name="companyId">The ID of the company to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        public Task<bool> DeleteCompanyAsync(Guid companyId);

        /// <summary>
        /// Get all job offers for a specific company.
        /// </summary>
        /// <param name="companyId">The ID of the company.</param>
        /// <returns>A list of all job offers for the company.</returns>
        public Task<List<Offer>> GetJobOffersByCompanyAsync(Guid companyId);

        /// <summary>
        /// Get all reconversion friendly companies.
        /// </summary>
        /// <returns>A list of all reconversion friendly companies.</returns>
        public Task<List<Company>> GetReconversionFriendlyCompaniesAsync();
    }
}