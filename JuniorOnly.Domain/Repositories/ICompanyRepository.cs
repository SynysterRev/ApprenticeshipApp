using JuniorOnly.Domain.Entities;

namespace JuniorOnly.Domain.Repositories
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetAllCompaniesAsync();
        Task<Company?> GetCompanyByIdAsync(Guid companyId);
        Task<Company> AddCompanyAsync(Company company);
        Task<Company?> UpdateCompanyAsync(Company updatedCompany);
        Task<bool> DeleteCompanyAsync(Guid companyId);
        Task<List<Offer>> GetJobOffersByCompanyAsync(Guid companyId);
    }
}