using AutoFixture;
using FluentAssertions;
using JuniorOnly.Application.DTO.Favorite;
using JuniorOnly.Application.DTO.Offer;
using JuniorOnly.Application.Exceptions;
using JuniorOnly.Application.Extensions;
using JuniorOnly.Application.Interfaces;
using JuniorOnly.Application.Services;
using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JuniorOnly.UnitTests.Services
{
    public class OfferServiceTests
    {
        private readonly IOfferService _offerService;
        private readonly Mock<IOfferRepository> _offerRepositoryMock;
        private readonly Mock<ICandidateProfileRepository> _candidateProfileRepositoryMock;
        private readonly Mock<ICompanyRepository> _companyRepositoryMock;
        private readonly IFixture _fixture;

        public OfferServiceTests()
        {
            _fixture = new Fixture();

            _fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _offerRepositoryMock = new Mock<IOfferRepository>();
            _candidateProfileRepositoryMock = new Mock<ICandidateProfileRepository>();
            _companyRepositoryMock = new Mock<ICompanyRepository>();

            _offerService = new OfferService(_offerRepositoryMock.Object, _companyRepositoryMock.Object, _candidateProfileRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateOfferAsync_ShouldThrowValidationException_InvalidSalaryRange()
        {
            var offerCreate = _fixture.Build<OfferCreateDto>()
                .With(temp => temp.SalaryMin, 30)
                .With(temp => temp.SalaryMax, 15)
                .Create();

            Func<Task> action = async () =>
            {
                await _offerService.CreateOfferAsync(offerCreate);
            };

            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task CreateOfferAsync_ShouldThrowNotFoundException_CompanyInvalidId()
        {
            var offer = _fixture.Build<OfferCreateDto>()
                .With(temp => temp.SalaryMin, 15)
                .With(temp => temp.SalaryMax, 30)
                .Create();

            _companyRepositoryMock.Setup(r => r.GetCompanyByIdAsync(It.IsAny<Guid>())).ReturnsAsync(null as Company);

            Func<Task> action = async () =>
            {
                await _offerService.CreateOfferAsync(offer);
            };

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task CreateOfferAsync_ShouldBeSuccessful_AllDetailsValid()
        {
            var offerCreate = _fixture.Build<OfferCreateDto>()
                .With(temp => temp.SalaryMin, 15)
                .With(temp => temp.SalaryMax, 30)
                .Create();

            var company = _fixture.Create<Company>();
            var expectedOffer = offerCreate.ToEntity().ToDto();

            _companyRepositoryMock.Setup(r => r.GetCompanyByIdAsync(It.IsAny<Guid>())).ReturnsAsync(company);

            var offerDto = await _offerService.CreateOfferAsync(offerCreate);
            expectedOffer.Id = offerDto.Id;

            offerDto.Should().NotBeNull();
            offerDto.Id.Should().NotBe(Guid.Empty);
            offerDto.Should().BeEquivalentTo(expectedOffer, options => options
            .Excluding(o => o.PublishedAt)
            .Excluding(o => o.UpdatedAt));
        }

        [Fact]
        public async Task DeleteOfferAsync_ShouldThrowNotFoundException_OfferInvalidId()
        {
            var invalidId = Guid.NewGuid();

            _offerRepositoryMock.Setup(r => r.GetOfferByIdAsync(invalidId)).ReturnsAsync(null as Offer);

            Func<Task> action = async () =>
            {
                await _offerService.DeleteOfferAsync(invalidId);
            };

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteOfferAsync_ShouldBeSuccessful_OfferValidId()
        {
            var offer = _fixture.Create<Offer>();

            _offerRepositoryMock.Setup(r => r.GetOfferByIdAsync(offer.Id)).ReturnsAsync(offer);

            await _offerService.DeleteOfferAsync(offer.Id);

            _offerRepositoryMock.Verify(r => r.DeleteOfferAsync(offer), Times.Once);
        }

        [Fact]
        public async Task GetAllOffersAsync_ShouldBeSuccessful_EmptyList()
        {
            _offerRepositoryMock.Setup(r => r.GetAllOffersAsync()).ReturnsAsync(new List<Offer>());

            var offers = await _offerService.GetAllOffersAsync();

            _offerRepositoryMock.Verify(r => r.GetAllOffersAsync(), Times.Once);

            offers.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllOffersAsync_ShouldBeSuccessful_FewOffers()
        {
            List<Offer> offers = new List<Offer>
            {
                _fixture.Build<Offer>().With(o => o.Title, "test").Create(),
                _fixture.Build<Offer>().With(o => o.Title, "test2").Create(),
                _fixture.Build<Offer>().With(o => o.Title, "test3").Create()
            };

            _offerRepositoryMock.Setup(r => r.GetAllOffersAsync()).ReturnsAsync(offers);

            var allOffers = await _offerService.GetAllOffersAsync();

            _offerRepositoryMock.Verify(r => r.GetAllOffersAsync(), Times.Once);

            var expectedOffers = offers.Select(o => o.ToDto()).ToList();

            allOffers.Should().NotBeEmpty();
            allOffers.Should().BeEquivalentTo(expectedOffers);
        }

        [Fact]
        public async Task GetOffersByCompanyAsync_ShouldReturnOffers()
        {
            var companyId = Guid.NewGuid();
            var offers = _fixture.CreateMany<Offer>(3).ToList();
            var expectedDtos = offers.Select(o => o.ToDto()).ToList();

            _offerRepositoryMock.Setup(r => r.GetOffersByCompanyAsync(companyId))
                                .ReturnsAsync(offers);

            var result = await _offerService.GetOffersByCompanyAsync(companyId);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedDtos);
            _offerRepositoryMock.Verify(r => r.GetOffersByCompanyAsync(companyId), Times.Once);
        }

        [Fact]
        public async Task SearchOffersAsync_ShouldReturnMatchingOffers()
        {
            string searchTerm = "dev";
            int? experienceMax = 2;

            var offers = _fixture.CreateMany<Offer>(2).ToList();
            var expectedDtos = offers.Select(o => o.ToDto()).ToList();

            _offerRepositoryMock.Setup(r => r.SearchOffersAsync(searchTerm, experienceMax))
                                .ReturnsAsync(offers);

            var result = await _offerService.SearchOffersAsync(searchTerm, experienceMax);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedDtos);
            _offerRepositoryMock.Verify(r => r.SearchOffersAsync(searchTerm, experienceMax), Times.Once);
        }

        [Fact]
        public async Task UpdateOfferAsync_ShouldBeSuccessful_WhenValidId()
        {
            var offer = _fixture.Create<Offer>();
            var updateDto = _fixture.Create<OfferUpdateDto>();

            _offerRepositoryMock.Setup(r => r.GetOfferByIdAsync(offer.Id))
                                .ReturnsAsync(offer);

            var updatedDto = await _offerService.UpdateOfferAsync(offer.Id, updateDto);

            updatedDto.Should().NotBeNull();
            updatedDto.Id.Should().Be(offer.Id);
            _offerRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateOfferAsync_ShouldThrowNotFoundException_WhenInvalidId()
        {
            var invalidId = Guid.NewGuid();
            var updateDto = _fixture.Create<OfferUpdateDto>();

            _offerRepositoryMock.Setup(r => r.GetOfferByIdAsync(invalidId))
                                .ReturnsAsync(null as Offer);

            Func<Task> act = async () => await _offerService.UpdateOfferAsync(invalidId, updateDto);

            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetFavoriteOffersAsync_ShouldReturnFavorites()
        {
            var candidateId = Guid.NewGuid();
            var favorites = _fixture.CreateMany<Offer>(2).ToList();
            var expectedDtos = favorites.Select(f => f.ToDto()).ToList();

            _offerRepositoryMock.Setup(r => r.GetFavoriteOffersByCandidateAsync(candidateId))
                                .ReturnsAsync(favorites);

            var result = await _offerService.GetFavoriteOffersAsync(candidateId);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedDtos);
            _offerRepositoryMock.Verify(r => r.GetFavoriteOffersByCandidateAsync(candidateId), Times.Once);
        }

        [Fact]
        public async Task AddToFavoritesAsync_ShouldBeSuccessful_WhenNotAlreadyFavorite()
        {
            var createDto = _fixture.Create<FavoriteCreateDto>();
            var candidate = _fixture.Create<CandidateProfile>();
            var offer = _fixture.Create<Offer>();

            _candidateProfileRepositoryMock.Setup(r => r.GetProfileByIdAsync(createDto.CandidateProfileId))
                                           .ReturnsAsync(candidate);
            _offerRepositoryMock.Setup(r => r.GetOfferByIdAsync(createDto.JobOfferId))
                                .ReturnsAsync(offer);
            _offerRepositoryMock.Setup(r => r.IsFavoriteAsync(createDto.CandidateProfileId, createDto.JobOfferId))
                                .ReturnsAsync(false);

            var result = await _offerService.AddToFavoritesAsync(createDto);

            result.Should().NotBeNull();
            result!.CandidateProfileId.Should().Be(createDto.CandidateProfileId);
            result.JobOfferId.Should().Be(createDto.JobOfferId);
            _offerRepositoryMock.Verify(r => r.AddFavoriteAsync(It.IsAny<Favorite>()), Times.Once);
        }

        [Fact]
        public async Task AddToFavoritesAsync_ShouldReturnNull_WhenAlreadyFavorite()
        {
            var createDto = _fixture.Create<FavoriteCreateDto>();
            var candidate = _fixture.Create<CandidateProfile>();
            var offer = _fixture.Create<Offer>();

            _candidateProfileRepositoryMock.Setup(r => r.GetProfileByIdAsync(createDto.CandidateProfileId))
                                           .ReturnsAsync(candidate);
            _offerRepositoryMock.Setup(r => r.GetOfferByIdAsync(createDto.JobOfferId))
                                .ReturnsAsync(offer);
            _offerRepositoryMock.Setup(r => r.IsFavoriteAsync(createDto.CandidateProfileId, createDto.JobOfferId))
                                .ReturnsAsync(true);

            var result = await _offerService.AddToFavoritesAsync(createDto);

            result.Should().BeNull();
            _offerRepositoryMock.Verify(r => r.AddFavoriteAsync(It.IsAny<Favorite>()), Times.Never);
        }

        [Fact]
        public async Task AddToFavoritesAsync_ShouldThrowNotFoundException_WhenCandidateNotFound()
        {
            var createDto = _fixture.Create<FavoriteCreateDto>();

            _candidateProfileRepositoryMock.Setup(r => r.GetProfileByIdAsync(createDto.CandidateProfileId))
                                           .ReturnsAsync(null as CandidateProfile);

            Func<Task> act = async () => await _offerService.AddToFavoritesAsync(createDto);

            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task AddToFavoritesAsync_ShouldThrowNotFoundException_WhenOfferNotFound()
        {
            var createDto = _fixture.Create<FavoriteCreateDto>();
            var candidate = _fixture.Create<CandidateProfile>();

            _candidateProfileRepositoryMock.Setup(r => r.GetProfileByIdAsync(createDto.CandidateProfileId))
                                           .ReturnsAsync(candidate);
            _offerRepositoryMock.Setup(r => r.GetOfferByIdAsync(createDto.JobOfferId))
                                .ReturnsAsync(null as Offer);

            Func<Task> act = async () => await _offerService.AddToFavoritesAsync(createDto);

            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task IsFavoriteAsync_ShouldReturnTrue_WhenFavoriteExists()
        {
            var candidateId = Guid.NewGuid();
            var offerId = Guid.NewGuid();

            _offerRepositoryMock.Setup(r => r.IsFavoriteAsync(candidateId, offerId))
                                .ReturnsAsync(true);

            var result = await _offerService.IsFavoriteAsync(candidateId, offerId);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task RemoveFromFavoritesAsync_ShouldCallRepository()
        {
            var candidateId = Guid.NewGuid();
            var offerId = Guid.NewGuid();

            await _offerService.RemoveFromFavoritesAsync(candidateId, offerId);

            _offerRepositoryMock.Verify(r => r.RemoveFavoriteAsync(candidateId, offerId), Times.Once);
        }

    }
}
