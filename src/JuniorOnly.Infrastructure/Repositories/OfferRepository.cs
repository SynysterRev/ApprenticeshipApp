using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Repositories;
using JuniorOnly.Domain.Search;
using JuniorOnly.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace JuniorOnly.Infrastructure.Repositories
{
    public class OfferRepository : BaseRepository, IOfferRepository
    {
        public OfferRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public IQueryable<Offer> GetAllOffers()
        {
            return _dbContext.Offers.Include(o => o.Company);
        }

        public IQueryable<Offer> GetOffersByCompany(Guid companyId)
        {
            return _dbContext.Offers.Where(o => o.CompanyId == companyId);
        }

        public async Task<Offer?> GetOfferByIdAsync(Guid offerId)
        {
            return await _dbContext.Offers.Include(o => o.Company).FirstOrDefaultAsync(o => o.Id == offerId);
        }

        public async Task<Offer> AddOfferAsync(Offer newOffer)
        {
            _dbContext.Offers.Add(newOffer);
            await _dbContext.SaveChangesAsync();
            return newOffer;
        }

        public async Task DeleteOfferAsync(Offer offer)
        {
            _dbContext.Offers.Remove(offer);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> GetOffersCountAsync()
        {
            return await _dbContext.Offers.CountAsync();
        }

        public async Task<List<Offer>> GetLastestOffersAsync(int count)
        {
            var offers = await _dbContext.Offers
                .Include(o => o.Company)
                .OrderByDescending(o => o.PublishedAt)
                .Take(count)
                .ToListAsync();
            return offers;
        }

        public IQueryable<Offer> SearchOffers(OfferSearchCriteria searchCriteria)
        {
            var query = _dbContext.Offers.Include(o => o.Company).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchCriteria.SearchTerm))
            {
                searchCriteria.SearchTerm = searchCriteria.SearchTerm.Trim().ToLower();
                query = query.Where(o => o.Title.ToLower().Contains(searchCriteria.SearchTerm) || o.Description.ToLower().Contains(searchCriteria.SearchTerm));
            }

            if (searchCriteria.RemoteType.HasValue)
            {
                query = query.Where(o => o.RemoteType == searchCriteria.RemoteType.Value);
            }

            if (searchCriteria.ContractType.HasValue)
            {
                query = query.Where(o => o.ContractType == searchCriteria.ContractType.Value);
            }

            if (searchCriteria.MinSalary.HasValue)
            {
                query = query.Where(o => o.SalaryMin >= searchCriteria.MinSalary.Value);
            }

            if (searchCriteria.MaxSalary.HasValue)
            {
                query = query.Where(o => o.SalaryMax <= searchCriteria.MaxSalary.Value);
            }

            return query;
        }

        public IQueryable<Offer> GetFavoriteOffersByCandidate(Guid candidateId)
        {
            return _dbContext.Favorites
                .Where(f => f.CandidateProfileId == candidateId)
                .Select(f => f.JobOffer)
                .Include(o => o.Company);
        }

        public async Task<bool> IsFavoriteAsync(Guid candidateId, Guid offerId)
        {
            return await _dbContext.Favorites
                .AnyAsync(f => f.CandidateProfileId == candidateId && f.JobOfferId == offerId);
        }

        public async Task<Favorite> AddFavoriteAsync(Favorite favorite)
        {
            _dbContext.Favorites.Add(favorite);
            await _dbContext.SaveChangesAsync();
            return favorite;
        }

        public async Task RemoveFavoriteAsync(Guid candidateId, Guid offerId)
        {
            var favorite = await _dbContext.Favorites
                .FirstOrDefaultAsync(f => f.CandidateProfileId == candidateId && f.JobOfferId == offerId);

            if (favorite == null) return;

            _dbContext.Favorites.Remove(favorite);
            await _dbContext.SaveChangesAsync();
        }
    }
}