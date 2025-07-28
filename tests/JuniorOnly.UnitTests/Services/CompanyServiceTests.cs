using AutoFixture;
using FluentAssertions;
using JuniorOnly.Application.DTO.Company;
using JuniorOnly.Application.Exceptions;
using JuniorOnly.Application.Extensions;
using JuniorOnly.Application.Interfaces;
using JuniorOnly.Application.Services;
using JuniorOnly.Domain.Entities;
using JuniorOnly.Domain.IdentityEntities;
using JuniorOnly.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Diagnostics.Metrics;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace JuniorOnly.UnitTests.Services
{
    public class CompanyServiceTests
    {
        private readonly ICompanyService _companyService;
        private readonly Mock<ICompanyRepository> _companyRepositoryMock;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;

        private readonly IFixture _fixture;

        public CompanyServiceTests()
        {
            _fixture = new Fixture();

            _fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));

            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _companyRepositoryMock = new Mock<ICompanyRepository>();

            var store = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null);

            _companyService = new CompanyService(_companyRepositoryMock.Object, _userManagerMock.Object);
        }

        [Fact]
        public async Task CreateCompanyAsync_ShouldThrowNotFoundException_WhenUserNotFound()
        {
            var companyDto = _fixture.Create<CompanyCreateDto>();
            _userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser?)null);

            Func<Task> action = async () =>
            {
                await _companyService.CreateCompanyAsync(companyDto);
            };
            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task CreateCompanyAsync_ShouldCreateCompany_WhenUserExists()
        {
            var companyDto = _fixture.Create<CompanyCreateDto>();
            _userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser { Id = companyDto.CreatedByUserId });

            var company = companyDto.ToEntity();
            var expectedCompany = company.ToDto();

            _companyRepositoryMock.Setup(r => r.AddCompanyAsync(It.IsAny<Company>())).ReturnsAsync(company);

            var result = await _companyService.CreateCompanyAsync(companyDto);
            expectedCompany.Id = result.Id;

            result.Should().NotBeNull();
            _companyRepositoryMock.Verify(r => r.AddCompanyAsync(It.IsAny<Company>()), Times.Once);
            result.Id.Should().NotBe(Guid.Empty);
            result.Should().BeEquivalentTo(expectedCompany, options => options
            .Excluding(c => c.CreatedAt)
            .Excluding(c => c.UpdatedAt));
        }

        [Fact]
        public async Task DeleteCompanyAsync_ShouldThrowNotFoundException_WhenIdNotFound()
        {
            var invalidId = Guid.NewGuid();
            _companyRepositoryMock.Setup(r => r.GetCompanyByIdAsync(invalidId))
                         .ReturnsAsync((Company?)null);
            Func<Task> action = async () =>
            {
                await _companyService.DeleteCompanyAsync(invalidId);
            };

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteCompanyAsync_ShouldBeSuccessful_WithValidId()
        {
            var companyId = Guid.NewGuid();
            var company = new Company { Id = companyId };

            _companyRepositoryMock.Setup(r => r.GetCompanyByIdAsync(companyId))
                                  .ReturnsAsync(company);

            await _companyService.DeleteCompanyAsync(companyId);

            _companyRepositoryMock.Verify(r => r.DeleteCompanyAsync(company), Times.Once);
        }

        [Fact]
        public async Task GetCompanyByIdAsync_ShouldThrowNotFoundException_WhenInvalidId()
        {
            var invalidId = Guid.NewGuid();
            _companyRepositoryMock.Setup(r => r.GetCompanyByIdAsync(invalidId))
                         .ReturnsAsync((Company?)null);
            Func<Task> action = async () =>
            {
                await _companyService.GetCompanyByIdAsync(invalidId);
            };

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetCompanyByIdAsync_ShouldBeSuccessful_WithValidId()
        {
            var company = _fixture.Create<Company>();

            _companyRepositoryMock.Setup(r => r.GetCompanyByIdAsync(company.Id)).ReturnsAsync(company);

            var expected_company = company.ToDto();
            var companyDto = await _companyService.GetCompanyByIdAsync(company.Id);

            companyDto.Should().NotBeNull();
            companyDto.Should().BeEquivalentTo(expected_company, options => options
            .Excluding(t => t.CreatedAt)
            .Excluding(t => t.UpdatedAt));
        }

        [Fact]
        public async Task GetAllCompaniesAsync_ShouldBeEmptyList()
        {
            _companyRepositoryMock.Setup(r => r.GetAllCompaniesAsync()).ReturnsAsync(new List<Company>());
            List<CompanyDto> companies = await _companyService.GetAllCompaniesAsync();

            companies.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllCompaniesAsync_ShouldBeSuccessful()
        {
            List<Company> companies = new List<Company>()
            {
                _fixture.Build<Company>().With(temp => temp.Name, "test1").Create(),
                _fixture.Build<Company>().With(temp => temp.Name, "test2").Create(),
                _fixture.Build<Company>().With(temp => temp.Name, "test3").Create(),
            };


            List<CompanyDto> expectedCompanies = companies.Select(c => c.ToDto()).ToList();
            _companyRepositoryMock.Setup(r => r.GetAllCompaniesAsync()).ReturnsAsync(companies);
            List<CompanyDto> companiesDto = await _companyService.GetAllCompaniesAsync();

            companiesDto.Should().NotBeEmpty();
            companiesDto.Should().BeEquivalentTo(expectedCompanies);
        }

        [Fact]
        public async Task GetReconversionFriendlyCompaniesAsync_ShouldBeEmptyList()
        {
            _companyRepositoryMock.Setup(r => r.GetReconversionFriendlyCompaniesAsync()).ReturnsAsync(new List<Company>());
            List<CompanyDto> companies = await _companyService.GetReconversionFriendlyCompaniesAsync();

            companies.Should().BeEmpty();
        }

        [Fact]
        public async Task GetReconversionFriendlyCompaniesAsync_ShouldBeSuccessful()
        {
            List<Company> companies = new List<Company>()
            {
                _fixture.Build<Company>().With(temp => temp.Name, "test1")
                .With(temp => temp.IsReconversionFriendly, false).Create(),
                _fixture.Build<Company>().With(temp => temp.Name, "test2")
                .With(temp => temp.IsReconversionFriendly, true).Create(),
                _fixture.Build<Company>().With(temp => temp.Name, "test3")
                .With(temp => temp.IsReconversionFriendly, false).Create(),
            };

            List<CompanyDto> expectedCompanies = companies.Where(c => c.IsReconversionFriendly).Select(c => c.ToDto()).ToList();
            _companyRepositoryMock.Setup(r => r.GetReconversionFriendlyCompaniesAsync()).ReturnsAsync(companies.Where(c => c.IsReconversionFriendly).ToList());

            List<CompanyDto> companiesDto = await _companyService.GetReconversionFriendlyCompaniesAsync();

            companiesDto.Should().NotBeEmpty();
            companiesDto.Should().BeEquivalentTo(expectedCompanies);
        }

        [Fact]
        public async Task UpdateCompanyAsync_ShouldThrowNotFoundException_WhenInvalidId()
        {
            var invalidId = Guid.NewGuid();
            _companyRepositoryMock.Setup(r => r.GetCompanyByIdAsync(invalidId))
                         .ReturnsAsync((Company?)null);

            var updatedDto = _fixture.Create<CompanyUpdateDto>();
            Func<Task> action = async () =>
            {
                await _companyService.UpdateCompanyAsync(invalidId, updatedDto);
            };

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task UpdateCompanyAsync_ShouldBeSuccessful_WhenValidId()
        {
            var existingCompany = _fixture.Create<Company>();

            _companyRepositoryMock.Setup(r => r.GetCompanyByIdAsync(existingCompany.Id))
                         .ReturnsAsync(existingCompany);

            var updatedDto = _fixture.Build<CompanyUpdateDto>()
                .With(c => c.Name, "Updated Name")
                .With(c => c.Description, "Updated Description")
                .With(c => c.Website, "https://updated.com")
                .Create();

            var result = await _companyService.UpdateCompanyAsync(existingCompany.Id, updatedDto);

            _companyRepositoryMock.Verify(r => r.GetCompanyByIdAsync(existingCompany.Id), Times.Once);
            _companyRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);

            result.Should().NotBeNull();
            result.Name.Should().Be(updatedDto.Name);
            result.Description.Should().Be(updatedDto.Description);
            result.Website.Should().Be(updatedDto.Website);
        }
    }
}
