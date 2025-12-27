using FluentAssertions;
using NGErp.General.Service.DTOs.PythonApi;
using Xunit;

namespace NGErp.PythonIntegration.Tests.ContractTests
{
    /// <summary>
    /// Contract tests to ensure Python API response structure matches our DTOs
    /// These tests verify that fields haven't changed on the Python side
    /// </summary>
    public class CompanyDtoContractTests
    {
        [Fact]
        public void CompanyDto_Should_Have_Required_Properties()
        {
            // Arrange & Act
            var properties = typeof(CompanyDto).GetProperties();
            var propertyNames = properties.Select(p => p.Name).ToList();

            // Assert - Verify all expected properties exist
            propertyNames.Should().Contain("Id", "Id property should exist");
            propertyNames.Should().Contain("Name", "Name property should exist");
            propertyNames.Should().Contain("Address", "Address property should exist");
            propertyNames.Should().Contain("Phone", "Phone property should exist");
            propertyNames.Should().Contain("Email", "Email property should exist");
            propertyNames.Should().Contain("CreatedAt", "CreatedAt property should exist");
            propertyNames.Should().Contain("UpdatedAt", "UpdatedAt property should exist");
        }

        [Fact]
        public void CompanyDto_Id_Should_Be_Int()
        {
            // Arrange
            var idProperty = typeof(CompanyDto).GetProperty("Id");

            // Assert
            idProperty.Should().NotBeNull("Id property should exist");
            idProperty!.PropertyType.Should().Be(typeof(int), "Id should be of type int");
        }

        [Fact]
        public void CompanyDto_Name_Should_Be_String()
        {
            // Arrange
            var nameProperty = typeof(CompanyDto).GetProperty("Name");

            // Assert
            nameProperty.Should().NotBeNull("Name property should exist");
            nameProperty!.PropertyType.Should().Be(typeof(string), "Name should be of type string");
        }

        [Fact]
        public void CompanyDto_Address_Should_Be_NullableString()
        {
            // Arrange
            var addressProperty = typeof(CompanyDto).GetProperty("Address");

            // Assert
            addressProperty.Should().NotBeNull("Address property should exist");
            addressProperty!.PropertyType.Should().Be(typeof(string), "Address should be of type string?");
        }

        [Fact]
        public void CompanyDto_Phone_Should_Be_NullableString()
        {
            // Arrange
            var phoneProperty = typeof(CompanyDto).GetProperty("Phone");

            // Assert
            phoneProperty.Should().NotBeNull("Phone property should exist");
            phoneProperty!.PropertyType.Should().Be(typeof(string), "Phone should be of type string?");
        }

        [Fact]
        public void CompanyDto_Email_Should_Be_NullableString()
        {
            // Arrange
            var emailProperty = typeof(CompanyDto).GetProperty("Email");

            // Assert
            emailProperty.Should().NotBeNull("Email property should exist");
            emailProperty!.PropertyType.Should().Be(typeof(string), "Email should be of type string?");
        }

        [Fact]
        public void CompanyDto_CreatedAt_Should_Be_DateTime()
        {
            // Arrange
            var createdAtProperty = typeof(CompanyDto).GetProperty("CreatedAt");

            // Assert
            createdAtProperty.Should().NotBeNull("CreatedAt property should exist");
            createdAtProperty!.PropertyType.Should().Be(typeof(DateTime), "CreatedAt should be of type DateTime");
        }

        [Fact]
        public void CompanyDto_UpdatedAt_Should_Be_NullableDateTime()
        {
            // Arrange
            var updatedAtProperty = typeof(CompanyDto).GetProperty("UpdatedAt");

            // Assert
            updatedAtProperty.Should().NotBeNull("UpdatedAt property should exist");
            updatedAtProperty!.PropertyType.Should().Be(typeof(DateTime?), "UpdatedAt should be of type DateTime?");
        }

        [Fact]
        public void CreateCompanyDto_Should_Have_Required_Properties()
        {
            // Arrange & Act
            var properties = typeof(CreateCompanyDto).GetProperties();
            var propertyNames = properties.Select(p => p.Name).ToList();

            // Assert
            propertyNames.Should().Contain("Name", "Name property should exist");
            propertyNames.Should().Contain("Address", "Address property should exist");
            propertyNames.Should().Contain("Phone", "Phone property should exist");
            propertyNames.Should().Contain("Email", "Email property should exist");
        }

        [Fact]
        public void UpdateCompanyDto_Should_Have_Required_Properties()
        {
            // Arrange & Act
            var properties = typeof(UpdateCompanyDto).GetProperties();
            var propertyNames = properties.Select(p => p.Name).ToList();

            // Assert
            propertyNames.Should().Contain("Name", "Name property should exist");
            propertyNames.Should().Contain("Address", "Address property should exist");
            propertyNames.Should().Contain("Phone", "Phone property should exist");
            propertyNames.Should().Contain("Email", "Email property should exist");
        }

        [Fact]
        public void CompanyDto_Should_Not_Have_Extra_Properties()
        {
            // Arrange
            var properties = typeof(CompanyDto).GetProperties();
            var expectedPropertyCount = 7; // Id, Name, Address, Phone, Email, CreatedAt, UpdatedAt

            // Assert
            properties.Length.Should().Be(expectedPropertyCount, 
                $"CompanyDto should have exactly {expectedPropertyCount} properties. " +
                "If Python API added new fields, update the DTO and this test.");
        }

        [Fact]
        public void CompanyDto_Should_Be_Serializable()
        {
            // Arrange
            var dto = new CompanyDto
            {
                Id = 1,
                Name = "Test Company",
                Address = "123 Test St",
                Phone = "+1234567890",
                Email = "test@company.com",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            // Act
            var json = System.Text.Json.JsonSerializer.Serialize(dto);
            var deserialized = System.Text.Json.JsonSerializer.Deserialize<CompanyDto>(json);

            // Assert
            deserialized.Should().NotBeNull();
            deserialized!.Id.Should().Be(dto.Id);
            deserialized.Name.Should().Be(dto.Name);
            deserialized.Address.Should().Be(dto.Address);
            deserialized.Phone.Should().Be(dto.Phone);
            deserialized.Email.Should().Be(dto.Email);
        }
    }
}
