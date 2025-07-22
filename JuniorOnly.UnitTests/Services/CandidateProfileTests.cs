using AutoFixture;
using FluentAssertions;
using JuniorOnly.Application.DTO.CandidateProfile;
using JuniorOnly.Application.Exceptions;
using JuniorOnly.Application.Extensions;
using JuniorOnly.Application.Interfaces;
using JuniorOnly.Application.Services;
using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.Enums;
using JuniorOnly.Domain.IdentityEntities;
using JuniorOnly.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.ComponentModel.DataAnnotations;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JuniorOnly.UnitTests.Services
{
    public class CandidateProfileTests
    {
        private readonly Mock<ICandidateProfileRepository> _profileRepositoryMock;
        private readonly ICandidateProfileService _profileService;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly IFixture _fixture;

        public CandidateProfileTests()
        {
            _fixture = new Fixture();

            _fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _profileRepositoryMock = new Mock<ICandidateProfileRepository>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null);

            _profileService = new CandidateProfileService(_profileRepositoryMock.Object, _userManagerMock.Object);
        }

        [Fact]
        public async Task CreateProfileAsync_ShouldThrowNotFoundException_WhenInvalidUserId()
        {
            var createDto = _fixture.Create<CandidateProfileCreateDto>();
            _userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(null as ApplicationUser);

            Func<Task> action = async () =>
            {
                await _profileService.CreateProfileAsync(createDto);
            };

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task CreateProfileAsync_ShouldThrowValidationException_WhenUserIdAlreadyExist()
        {
            var createDto = _fixture.Create<CandidateProfileCreateDto>();
            var profile = createDto.ToEntity();

            _userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser { Id = createDto.UserId });
            _profileRepositoryMock.Setup(p => p.GetProfileByUserIdAsync(createDto.UserId)).ReturnsAsync(profile);

            Func<Task> action = async () =>
            {
                await _profileService.CreateProfileAsync(createDto);
            };

            await action.Should().ThrowAsync<ValidationException>();
        }

        [Fact]
        public async Task CreateProfileAsync_ShouldBeSuccessful_WhenAllDetailsProvided()
        {
            var createDto = _fixture.Create<CandidateProfileCreateDto>();
            var profile = createDto.ToEntity();

            _userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser { Id = createDto.UserId });
            _profileRepositoryMock.Setup(p => p.GetProfileByUserIdAsync(createDto.UserId)).ReturnsAsync(null as CandidateProfile);

            var result = await _profileService.CreateProfileAsync(createDto);
            var expectedResult = profile.ToDto();
            expectedResult.Id = result.Id;

            result.Should().NotBeNull();
            result.Id.Should().NotBe(Guid.Empty);
            result.Should().BeEquivalentTo(expectedResult, options => options
            .Excluding(p => p.CreatedAt)
            .Excluding(p => p.UpdatedAt));
        }

        [Fact]
        public async Task DeleteProfilAsync_ShouldThrowNotFoundException_WhenInvalidId()
        {
            var invalidID = Guid.NewGuid();
            _profileRepositoryMock.Setup(p => p.GetProfileByIdAsync(invalidID)).ReturnsAsync(null as CandidateProfile);

            Func<Task> action = async () =>
            {
                await _profileService.DeleteProfileAsync(invalidID);
            };

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteProfilAsync_ShouldBeSuccessful_WhenValidId()
        {
            var profile = _fixture.Create<CandidateProfile>();

            _profileRepositoryMock.Setup(p => p.GetProfileByIdAsync(profile.Id)).ReturnsAsync(profile);

            await _profileService.DeleteProfileAsync(profile.Id);

            _profileRepositoryMock.Verify(p => p.DeleteProfileAsync(profile), Times.Once);
        }

        [Fact]
        public async Task GetAllProfilesAsync_ShouldBeSuccessful_EmptyList()
        {
            _profileRepositoryMock.Setup(p => p.GetAllProfilesAsync()).ReturnsAsync(new List<CandidateProfile>());

            var profiles = await _profileService.GetAllProfilesAsync();

            _profileRepositoryMock.Verify(p => p.GetAllProfilesAsync(), Times.Once);
            profiles.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllProfilesAsync_ShouldBeSuccessful_FewProfiles()
        {
            var existingProfiles = _fixture.CreateMany<CandidateProfile>(3).ToList();
            _profileRepositoryMock.Setup(p => p.GetAllProfilesAsync()).ReturnsAsync(existingProfiles);

            var profiles = await _profileService.GetAllProfilesAsync();
            var expectedProfiles = existingProfiles.Select(p => p.ToDto()).ToList();

            _profileRepositoryMock.Verify(p => p.GetAllProfilesAsync(), Times.Once);
            profiles.Should().NotBeEmpty();
            profiles.Should().BeEquivalentTo(expectedProfiles);
        }

        [Fact]
        public async Task GetProfileByIdAsync_ShouldThrowNotFoundException_WhenInvalidId()
        {
            var invalidId = Guid.NewGuid();

            _profileRepositoryMock.Setup(p => p.GetProfileByIdAsync(invalidId)).ReturnsAsync(null as CandidateProfile);

            Func<Task> action = async () => { await _profileService.GetProfileByIdAsync(invalidId); };

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetProfileByIdAsync_ShouldBeSuccessful_WhenValidId()
        {
            var profile = _fixture.Create<CandidateProfile>();

            _profileRepositoryMock.Setup(p => p.GetProfileByIdAsync(profile.Id)).ReturnsAsync(profile);

            var result = await _profileService.GetProfileByIdAsync(profile.Id);
            var expected = profile.ToDto();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected, options => options
            .Excluding(p => p.UpdatedAt)
            .Excluding(p => p.CreatedAt));
        }

        [Fact]
        public async Task GetProfileByUserIdAsync_ShouldThrowNotFoundException_WhenInvalidUserId()
        {
            var invalidUserId = Guid.NewGuid();

            _profileRepositoryMock.Setup(p => p.GetProfileByUserIdAsync(invalidUserId)).ReturnsAsync(null as CandidateProfile);

            Func<Task> action = async () => { await _profileService.GetProfileByUserIdAsync(invalidUserId); };

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetProfileByUserIdAsync_ShouldBeSuccessful_WhenValidUserId()
        {
            var profile = _fixture.Create<CandidateProfile>();

            _profileRepositoryMock.Setup(p => p.GetProfileByUserIdAsync(profile.UserId)).ReturnsAsync(profile);

            var result = await _profileService.GetProfileByUserIdAsync(profile.UserId);
            var expected = profile.ToDto();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected, options => options
            .Excluding(p => p.UpdatedAt)
            .Excluding(p => p.CreatedAt));
        }

        [Fact]
        public async Task UpdateProfileAsync_ShouldThrowNotFoundException_WhenInvalidId()
        {
            var invalidId = Guid.NewGuid();
            var updateDto = _fixture.Create<CandidateProfileUpdateDto>();

            _profileRepositoryMock.Setup(p => p.GetProfileByIdAsync(invalidId)).ReturnsAsync(null as CandidateProfile);

            Func<Task> action = async () => { await _profileService.UpdateProfileAsync(invalidId, updateDto); };

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task UpdateProfileAsync_ShouldBeSuccessful_WhenValidId()
        {
            var existingProfile = _fixture.Create<CandidateProfile>();
            var updateDto = _fixture.Create<CandidateProfileUpdateDto>();

            _profileRepositoryMock.Setup(p => p.GetProfileByIdAsync(existingProfile.Id)).ReturnsAsync(existingProfile);

            var result = await _profileService.UpdateProfileAsync(existingProfile.Id, updateDto);
            var expected = existingProfile.ToDto();

            _profileRepositoryMock.Verify(p => p.SaveChangesAsync(), Times.Once);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expected, options =>
                options.Excluding(p => p.UpdatedAt)
                       .Excluding(p => p.CreatedAt));
        }

        //[Fact]
        //public async Task SearchProfilesAsync_ShouldBeSuccessful_WithFilters()
        //{
        //    var profiles = _fixture.CreateMany<CandidateProfile>(3).ToList();
        //    _profileRepositoryMock
        //        .Setup(p => p.SearchProfilesAsync(It.IsAny<string>(), It.IsAny<ExperienceLevel?>()))
        //        .ReturnsAsync(profiles);

        //    var result = await _profileService.SearchProfilesAsync("Paris", ExperienceLevel.Senior);

        //    var expected = profiles.Select(p => p.ToDto()).ToList();

        //    result.Should().BeEquivalentTo(expected);
        //}
    }
}
