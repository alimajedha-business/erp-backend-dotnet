using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NGErp.Base.Infrastructure.Services;
using NGErp.PythonIntegration.Tests.Fixtures;
using Xunit;
using System.Net;
using System.Text.Json;

namespace NGErp.PythonIntegration.Tests.IntegrationTests
{
    /// <summary>
    /// Tests to verify Django API response structure and field consistency
    /// These tests will fail if Python API changes its response format
    /// </summary>
    [Collection("PythonApi")]
    public class DjangoApiResponseStructureTests : IClassFixture<PythonApiTestFixture>
    {
        private readonly PythonApiTestFixture _fixture;
        private readonly DjangoApiService _djangoService;

        public DjangoApiResponseStructureTests(PythonApiTestFixture fixture)
        {
            _fixture = fixture;
            _djangoService = _fixture.ServiceProvider.GetRequiredService<DjangoApiService>();
        }

        [Fact(Skip = "Requires Django API to be running")]
        public async Task DjangoApi_CompanyList_Should_Return_Json_Array()
        {
            // Act
            var result = await _djangoService.GetAsync<JsonElement>("/api/companies/");

            // Assert
            result.ValueKind.Should().Be(JsonValueKind.Array, 
                "Company list endpoint should return a JSON array");
        }

        [Fact(Skip = "Requires Django API to be running and test data")]
        public async Task DjangoApi_Company_Response_Should_Have_Expected_Fields()
        {
            // Act
            var companies = await _djangoService.GetAsync<JsonElement>("/api/companies/");

            // Assert
            if (companies.GetArrayLength() > 0)
            {
                var firstCompany = companies[0];
                
                // Verify required fields exist
                firstCompany.TryGetProperty("id", out _).Should().BeTrue("Response should have 'id' field");
                firstCompany.TryGetProperty("name", out _).Should().BeTrue("Response should have 'name' field");
                firstCompany.TryGetProperty("created_at", out _).Should().BeTrue("Response should have 'created_at' field");
                
                // Verify optional fields
                firstCompany.TryGetProperty("address", out _).Should().BeTrue("Response should have 'address' field");
                firstCompany.TryGetProperty("phone", out _).Should().BeTrue("Response should have 'phone' field");
                firstCompany.TryGetProperty("email", out _).Should().BeTrue("Response should have 'email' field");
                firstCompany.TryGetProperty("updated_at", out _).Should().BeTrue("Response should have 'updated_at' field");
            }
        }

        [Fact(Skip = "Requires Django API to be running and test data")]
        public async Task DjangoApi_Company_Id_Should_Be_Number()
        {
            // Act
            var companies = await _djangoService.GetAsync<JsonElement>("/api/companies/");

            // Assert
            if (companies.GetArrayLength() > 0)
            {
                var firstCompany = companies[0];
                firstCompany.TryGetProperty("id", out var idElement).Should().BeTrue();
                
                (idElement.ValueKind == JsonValueKind.Number).Should().BeTrue(
                    "Company 'id' field should be a number. If Python changed this to string, update the DTO.");
            }
        }

        [Fact(Skip = "Requires Django API to be running and test data")]
        public async Task DjangoApi_Company_Name_Should_Be_String()
        {
            // Act
            var companies = await _djangoService.GetAsync<JsonElement>("/api/companies/");

            // Assert
            if (companies.GetArrayLength() > 0)
            {
                var firstCompany = companies[0];
                firstCompany.TryGetProperty("name", out var nameElement).Should().BeTrue();
                
                nameElement.ValueKind.Should().Be(JsonValueKind.String,
                    "Company 'name' field should be a string");
            }
        }

        [Fact(Skip = "Requires Django API to be running and test data")]
        public async Task DjangoApi_Company_CreatedAt_Should_Be_String_DateTime()
        {
            // Act
            var companies = await _djangoService.GetAsync<JsonElement>("/api/companies/");

            // Assert
            if (companies.GetArrayLength() > 0)
            {
                var firstCompany = companies[0];
                firstCompany.TryGetProperty("created_at", out var createdAtElement).Should().BeTrue();
                
                createdAtElement.ValueKind.Should().Be(JsonValueKind.String,
                    "Company 'created_at' should be a string (ISO 8601 format)");
                
                // Try to parse as DateTime
                var createdAtString = createdAtElement.GetString();
                DateTime.TryParse(createdAtString, out _).Should().BeTrue(
                    "created_at should be parseable as DateTime. If Python changed the format, update the DTO.");
            }
        }

        [Fact(Skip = "Requires Django API to be running and test data")]
        public async Task DjangoApi_Should_Not_Have_Unexpected_Fields()
        {
            // Arrange
            var expectedFields = new HashSet<string>
            {
                "id", "name", "address", "phone", "email", "created_at", "updated_at"
            };

            // Act
            var companies = await _djangoService.GetAsync<JsonElement>("/api/companies/");

            // Assert
            if (companies.GetArrayLength() > 0)
            {
                var firstCompany = companies[0];
                var actualFields = new HashSet<string>();

                foreach (var property in firstCompany.EnumerateObject())
                {
                    actualFields.Add(property.Name);
                }

                var unexpectedFields = actualFields.Except(expectedFields).ToList();
                
                unexpectedFields.Should().BeEmpty(
                    $"Python API returned unexpected fields: {string.Join(", ", unexpectedFields)}. " +
                    "Update the DTO if these fields are intended.");
            }
        }

        [Fact(Skip = "Requires Django API to be running")]
        public async Task DjangoApi_Should_Return_404_For_NonExistent_Company()
        {
            // Arrange
            int nonExistentId = 999999;

            // Act
            Func<Task> act = async () =>
            {
                await _djangoService.GetAsync<JsonElement>($"/api/companies/{nonExistentId}/");
            };

            // Assert
            await act.Should().ThrowAsync<HttpRequestException>(
                "Django should return 404 for non-existent company");
        }

        [Fact(Skip = "Requires Django API to be running")]
        public async Task DjangoApi_CompanyDetail_Should_Return_Single_Object()
        {
            // Arrange - First get a company ID
            var companies = await _djangoService.GetAsync<JsonElement>("/api/companies/");
            
            if (companies.GetArrayLength() == 0)
            {
                // Skip test if no companies exist
                return;
            }

            var firstCompany = companies[0];
            firstCompany.TryGetProperty("id", out var idElement).Should().BeTrue();
            var companyId = idElement.GetInt32();

            // Act
            var result = await _djangoService.GetAsync<JsonElement>($"/api/companies/{companyId}/");

            // Assert
            result.ValueKind.Should().Be(JsonValueKind.Object,
                "Company detail endpoint should return a JSON object, not an array");
        }
    }
}
