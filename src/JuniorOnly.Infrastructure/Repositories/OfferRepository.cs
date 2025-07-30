using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Repositories;
using JuniorOnly.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace JuniorOnly.Infrastructure.Repositories
{
    public class OfferRepository : BaseRepository, IOfferRepository
    {
        public OfferRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<List<Offer>> GetAllOffersAsync()
        {
            return await _dbContext.Offers.Include(o => o.Company).ToListAsync();
        }

        public async Task<List<Offer>> GetOffersByCompanyAsync(Guid companyId)
        {
            return await _dbContext.Offers.Where(o => o.CompanyId == companyId).ToListAsync();
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

        public async Task<List<Offer>> SearchOffersAsync(string searchTerm, int? experienceMax = null)
        {
            var query = _dbContext.Offers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = searchTerm.Trim().ToLower();
                query = query.Where(o => o.Title.ToLower().Contains(searchTerm) || o.Description.ToLower().Contains(searchTerm));
            }

            if (experienceMax.HasValue && experienceMax >= 0)
            {
                query = query.Where(o => o.ExperienceRequired <= experienceMax.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Offer>> GetFavoriteOffersByCandidateAsync(Guid candidateId)
        {
            return await _dbContext.Favorites
                .Where(f => f.CandidateProfileId == candidateId)
                .Select(f => f.JobOffer)
                .Include(o => o.Company)
                .ToListAsync();
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