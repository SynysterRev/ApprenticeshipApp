using JuniorOnly.Application.Commons;
using JuniorOnly.Application.DTO.Favorite;
using JuniorOnly.Application.DTO.Offer;
using JuniorOnly.Application.DTO.Pagination;
using JuniorOnly.Application.Exceptions;
using JuniorOnly.Application.Extensions;
using JuniorOnly.Application.Interfaces;
using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Repositories;
using JuniorOnly.Domain.Search;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace JuniorOnly.Application.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly ICandidateProfileRepository _profileRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly PaginationOptions _paginationOptions;

        public OfferService(IOfferRepository offersRepository,
            ICompanyRepository companyRepository,
            ICandidateProfileRepository profileRepository,
            IOptions<PaginationOptions> paginationOptions)
        {
            _offerRepository = offersRepository;
            _companyRepository = companyRepository;
            _profileRepository = profileRepository;
            _paginationOptions = paginationOptions.Value;
        }

        public async Task<OfferDto> CreateOfferAsync(OfferCreateDto offerDto)
        {
            if (offerDto.SalaryMin > offerDto.SalaryMax)
            {
                throw new ValidationException("The minimal salary cannot be greater than the maximum salary.");
            }

            var company = await _companyRepository.GetCompanyByIdAsync(offerDto.CompanyId);

            if (company == null)
            {
                throw new NotFoundException($"Company with ID {offerDto.CompanyId} not found");
            }

            Offer offer = offerDto.ToEntity();
            offer.Id = Guid.NewGuid();
            offer.Company = company;
            await _offerRepository.AddOfferAsync(offer);

            return offer.ToDto();
        }

        public async Task DeleteOfferAsync(Guid offerId)
        {
            var offer = await _offerRepository.GetOfferByIdAsync(offerId);

            if (offer == null)
            {
                throw new NotFoundException($"Offer with ID {offerId} not found");
            }

            await _offerRepository.DeleteOfferAsync(offer);
        }

        public async Task<PaginatedResponse<OfferDto>> GetAllOffersAsync(int pageNumber)
        {
            var offers = await PaginatedList<Offer>.CreateAsync(
                _offerRepository.GetAllOffers(),
                pageNumber,
                _paginationOptions.DefaultPageSize);
            return offers.ToResponse(offers.Select(o => o.ToDto()));
        }

        public async Task<OfferDto> GetOfferByIdAsync(Guid offerId)
        {
            var offer = await _offerRepository.GetOfferByIdAsync(offerId);

            if (offer == null)
            {
                throw new NotFoundException($"Offer with ID {offerId} not found");
            }

            return offer.ToDto();
        }

        public async Task<PaginatedResponse<OfferDto>> GetOffersByCompanyAsync(Guid companyId, int pageNumber)
        {
            var offers = await PaginatedList<Offer>.CreateAsync(
                _offerRepository.GetOffersByCompany(companyId),
                pageNumber,
                _paginationOptions.DefaultPageSize);
            return offers.ToResponse(offers.Select(o => o.ToDto()));
        }

        public async Task<PaginatedResponse<OfferDto>> SearchOffersAsync(OfferSearchQuery query, int pageNumber)
        {
            var criteria = new OfferSearchCriteria
            {
                SearchTerm = query.SearchTerm,
                RemoteType = query.RemoteType,
                ContractType = query.ContractType,
                MaxSalary = query.MaxSalary,
                MinSalary = query.MinSalary,
            };
            var offersQuery = _offerRepository.SearchOffers(criteria);
            var offers = await PaginatedList<Offer>.CreateAsync(
                offersQuery,
                pageNumber,
                _paginationOptions.DefaultPageSize);

            return offers.ToResponse(offers.Select(o => o.ToDto()));
        }

        public async Task<OfferDto> UpdateOfferAsync(Guid offerId, OfferUpdateDto offerDto)
        {
            var offer = await _offerRepository.GetOfferByIdAsync(offerId);

            if (offer == null)
            {
                throw new NotFoundException($"Offer with ID {offerId} not found");
            }

            offer.UpdateFrom(offerDto);

            await _offerRepository.SaveChangesAsync();
            return offer.ToDto();
        }

        public async Task SoftDeleteOfferAsync(Guid offerId)
        {
            var offer = await _offerRepository.GetOfferByIdAsync(offerId);

            if (offer == null)
            {
                throw new NotFoundException($"Offer with ID {offerId} not found");
            }

            offer.DeletedAt = DateTime.UtcNow;
            offer.IsDeleted = true;

            await _offerRepository.SaveChangesAsync();
        }

        public async Task<int> GetOffersCountAsync()
        {
            return await _offerRepository.GetOffersCountAsync();
        }

        public async Task<List<OfferDto>> GetLastestOffersAsync(int count)
        {
            var offers = await _offerRepository.GetLastestOffersAsync(count);
            return offers.Select(o => o.ToDto()).ToList();
        }

        public async Task<PaginatedResponse<OfferDto>> GetFavoriteOffersAsync(Guid candidateId, int pageNumber)
        {
            var favoritesQuery = _offerRepository.GetFavoriteOffersByCandidate(candidateId);
            var favorites = await PaginatedList<Offer>.CreateAsync(
                favoritesQuery,
                pageNumber,
                _paginationOptions.DefaultPageSize);
            return favorites.ToResponse(favorites.Select(f => f.ToDto()));
        }

        public async Task<FavoriteDto?> AddToFavoritesAsync(FavoriteCreateDto createDto)
        {
            var candidate = await _profileRepository.GetProfileByIdAsync(createDto.CandidateProfileId);
            if (candidate == null)
                throw new NotFoundException($"Candidate with ID {createDto.CandidateProfileId} not found");

            var offer = await _offerRepository.GetOfferByIdAsync(createDto.JobOfferId);
            if (offer == null)
                throw new NotFoundException($"Offer with ID {createDto.JobOfferId} not found");

            // Check favorite does not already exist
            var alreadyFavorite = await _offerRepository.IsFavoriteAsync(createDto.CandidateProfileId, createDto.JobOfferId);
            if (alreadyFavorite)
                return null;

            var favorite = createDto.ToEntity();

            await _offerRepository.AddFavoriteAsync(favorite);

            return favorite.ToDto();
        }

        public async Task<bool> IsFavoriteAsync(Guid candidateId, Guid offerId)
        {
            return await _offerRepository.IsFavoriteAsync(candidateId, offerId);
        }

        public async Task RemoveFromFavoritesAsync(Guid candidateId, Guid offerId)
        {
            await _offerRepository.RemoveFavoriteAsync(candidateId, offerId);
        }
    }
}
