using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NGErp.General.Service.Services;
using NGErp.General.Service.DTOs.PythonApi;
using NGErp.PythonIntegration.Tests.Fixtures;
using Xunit;

namespace NGErp.PythonIntegration.Tests.IntegrationTests
{
    /// <summary>
    /// Integration tests that verify actual communication with Python/Django API
    /// These tests require the Django API to be running on the configured URL
    /// </summary>
    [Collection("PythonApi")]
    public class PythonIntegrationServiceTests : IClassFixture<PythonApiTestFixture>
    {
        private readonly PythonApiTestFixture _fixture;
        private readonly IPythonIntegrationService _pythonService;

        public PythonIntegrationServiceTests(PythonApiTestFixture fixture)
        {
            _fixture = fixture;
            _pythonService = _fixture.ServiceProvider.GetRequiredService<IPythonIntegrationService>();
        }

        [Fact(Skip = "Requires Django API to be running")]
        public async Task GetCompaniesAsync_Should_Return_List_Of_Companies()
        {
            // Act
            var result = await _pythonService.GetCompaniesAsync();

            // Assert
            result.Should().NotBeNull("Result should not be null");
            result.Should().BeOfType<List<CompanyDto>>("Result should be a list of CompanyDto");
        }

        [Fact(Skip = "Requires Django API to be running")]
        public async Task GetCompaniesAsync_Response_Should_Match_Contract()
        {
            // Act
            var result = await _pythonService.GetCompaniesAsync();

            // Assert
            if (result.Any())
            {
                var firstCompany = result.First();

                // Verify all required fields exist
                firstCompany.Id.Should().NotBeEmpty("Created entity should have a valid ID");
                firstCompany.Name.Should().NotBeNullOrEmpty("Name should not be null or empty");
                firstCompany.CreatedAt.Should().NotBe(default(DateTime), "CreatedAt should be set");
                
                // Verify field types implicitly through property access
                var _ = firstCompany.Address;  // Should be string or null
                var __ = firstCompany.Phone;   // Should be string or null
                var ___ = firstCompany.Email;  // Should be string or null
                var ____ = firstCompany.UpdatedAt; // Should be DateTime? or null
            }
        }

        [Fact(Skip = "Requires Django API to be running and test data")]
        public async Task GetCompanyByIdAsync_Should_Return_Company_When_Exists()
        {
            // Arrange
            Guid testCompanyId = new Guid("9AD4F9DF-A4FA-4047-A1CD-6D46ED9E16E8"); // Adjust based on your test data

            // Act
            var result = await _pythonService.GetCompanyByIdAsync(testCompanyId);

            // Assert
            result.Should().NotBeNull("Company should exist");
            result!.Id.Should().Be(testCompanyId, "Returned company should have the requested ID");
            result.Name.Should().NotBeNullOrEmpty("Company should have a name");
        }

        [Fact(Skip = "Requires Django API to be running")]
        public async Task GetCompanyByIdAsync_Should_Return_Null_When_NotExists()
        {
            // Arrange
            Guid nonExistentId = new Guid("75C9937A-A54B-41B3-88CA-5154731D2E06");

            // Act
            var result = await _pythonService.GetCompanyByIdAsync(nonExistentId);

            // Assert
            result.Should().BeNull("Non-existent company should return null");
        }

        [Fact(Skip = "Requires Django API to be running and authentication")]
        public async Task CreateCompanyAsync_Should_Create_Company_And_Return_It()
        {
            // Arrange
            var createDto = new CreateCompanyDto
            {
                Name = $"Test Company {Guid.NewGuid()}",
                Address = "123 Test Street",
                Phone = "+1-234-567-8900",
                Email = "test@company.com"
            };

            // Act
            var result = await _pythonService.CreateCompanyAsync(createDto);

            // Assert
            result.Should().NotBeNull("Created company should be returned");
            result.Id.Should().NotBeEmpty("Created entity should have a valid ID");
            result.Name.Should().Be(createDto.Name, "Name should match");
            result.Address.Should().Be(createDto.Address, "Address should match");
            result.Phone.Should().Be(createDto.Phone, "Phone should match");
            result.Email.Should().Be(createDto.Email, "Email should match");
            result.CreatedAt.Should().NotBe(default(DateTime), "CreatedAt should be set");
        }

        [Fact(Skip = "Requires Django API to be running and authentication")]
        public async Task UpdateCompanyAsync_Should_Update_Company()
        {
            // Arrange
            var createDto = new CreateCompanyDto
            {
                Name = $"Company To Update {Guid.NewGuid()}",
                Address = "Original Address"
            };
            var created = await _pythonService.CreateCompanyAsync(createDto);

            var updateDto = new UpdateCompanyDto
            {
                Name = "Updated Company Name",
                Address = "Updated Address"
            };

            // Act
            var result = await _pythonService.UpdateCompanyAsync(created.Id, updateDto);

            // Assert
            result.Should().NotBeNull("Updated company should be returned");
            result.Id.Should().Be(created.Id, "ID should remain the same");
            result.Name.Should().Be(updateDto.Name, "Name should be updated");
            result.Address.Should().Be(updateDto.Address, "Address should be updated");
        }

        [Fact(Skip = "Requires Django API to be running and authentication")]
        public async Task DeleteCompanyAsync_Should_Delete_Company()
        {
            // Arrange
            var createDto = new CreateCompanyDto
            {
                Name = $"Company To Delete {Guid.NewGuid()}",
                Address = "To be deleted"
            };
            var created = await _pythonService.CreateCompanyAsync(createDto);

            // Act
            var deleteResult = await _pythonService.DeleteCompanyAsync(created.Id);

            // Assert
            deleteResult.Should().BeTrue("Delete should succeed");

            // Verify it's actually deleted
            var getResult = await _pythonService.GetCompanyByIdAsync(created.Id);
            getResult.Should().BeNull("Deleted company should not be found");
        }
    }
}
