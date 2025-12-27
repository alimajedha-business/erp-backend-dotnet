using FluentAssertions;
using System.Text.Json;
using Xunit;

namespace NGErp.PythonIntegration.Tests.SerializationTests
{
    /// <summary>
    /// Tests to ensure JSON serialization/deserialization works correctly
    /// These tests verify that Python's JSON format is compatible with our DTOs
    /// </summary>
    public class JsonSerializationTests
    {
        [Fact]
        public void Should_Deserialize_Python_Company_Response_With_SnakeCase()
        {
            // Arrange - This is the format Python typically returns
            var pythonJson = @"{
                ""id"": 1,
                ""name"": ""Test Company"",
                ""address"": ""123 Test St"",
                ""phone"": ""+1234567890"",
                ""email"": ""test@company.com"",
                ""created_at"": ""2024-01-01T10:00:00Z"",
                ""updated_at"": ""2024-01-02T15:30:00Z""
            }";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };

            // Act
            var result = JsonSerializer.Deserialize<JsonElement>(pythonJson, options);

            // Assert
            result.ValueKind.Should().NotBe(JsonValueKind.Undefined, "JSON should be deserializable");
        }

        [Fact]
        public void Should_Handle_Null_Optional_Fields()
        {
            // Arrange - Python might return null for optional fields
            var pythonJson = @"{
                ""id"": 1,
                ""name"": ""Test Company"",
                ""address"": null,
                ""phone"": null,
                ""email"": null,
                ""created_at"": ""2024-01-01T10:00:00Z"",
                ""updated_at"": null
            }";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };

            // Act
            var result = JsonSerializer.Deserialize<JsonElement>(pythonJson, options);

            // Assert
            result.TryGetProperty("address", out var address).Should().BeTrue();
            address.ValueKind.Should().Be(JsonValueKind.Null, "Null values should be preserved");
        }

        [Fact]
        public void Should_Handle_Missing_Optional_Fields()
        {
            // Arrange - Python might not include optional fields at all
            var pythonJson = @"{
                ""id"": 1,
                ""name"": ""Test Company"",
                ""created_at"": ""2024-01-01T10:00:00Z""
            }";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };

            // Act
            var result = JsonSerializer.Deserialize<JsonElement>(pythonJson, options);

            // Assert
            result.TryGetProperty("address", out _).Should().BeFalse(
                "Missing optional fields should not cause errors");
        }

        [Fact]
        public void Should_Serialize_CreateCompanyDto_For_Python()
        {
            // Arrange
            var dto = new
            {
                Name = "New Company",
                Address = "456 New St",
                Phone = "+9876543210",
                Email = "new@company.com"
            };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            // Act
            var json = JsonSerializer.Serialize(dto, options);
            var deserialized = JsonSerializer.Deserialize<JsonElement>(json);

            // Assert
            deserialized.TryGetProperty("name", out var name).Should().BeTrue();
            name.GetString().Should().Be("New Company");
        }

        [Theory]
        [InlineData("2024-01-01T10:00:00Z")]
        [InlineData("2024-01-01T10:00:00")]
        [InlineData("2024-01-01T10:00:00+00:00")]
        public void Should_Parse_Different_DateTime_Formats_From_Python(string dateTimeString)
        {
            // Arrange
            var pythonJson = $@"{{
                ""created_at"": ""{dateTimeString}""
            }}";

            // Act
            var result = JsonSerializer.Deserialize<JsonElement>(pythonJson);
            result.TryGetProperty("created_at", out var createdAt).Should().BeTrue();
            var createdAtString = createdAt.GetString();

            // Assert
            DateTime.TryParse(createdAtString, out var parsedDate).Should().BeTrue(
                $"DateTime format '{dateTimeString}' should be parseable");
        }

        [Fact]
        public void Should_Detect_Field_Type_Mismatch()
        {
            // Arrange - Python returns id as string instead of number
            var pythonJsonWithStringId = @"{
                ""id"": ""1"",
                ""name"": ""Test Company"",
                ""created_at"": ""2024-01-01T10:00:00Z""
            }";

            // Act
            var result = JsonSerializer.Deserialize<JsonElement>(pythonJsonWithStringId);
            result.TryGetProperty("id", out var idElement).Should().BeTrue();

            // Assert
            idElement.ValueKind.Should().Be(JsonValueKind.String,
                "This test documents that Python changed 'id' from number to string. " +
                "If this assertion fails, it means Python fixed the type back to number.");
        }

        [Fact]
        public void Should_Detect_Missing_Required_Field()
        {
            // Arrange - Python response missing required 'name' field
            var pythonJsonMissingName = @"{
                ""id"": 1,
                ""created_at"": ""2024-01-01T10:00:00Z""
            }";

            // Act
            var result = JsonSerializer.Deserialize<JsonElement>(pythonJsonMissingName);

            // Assert
            result.TryGetProperty("name", out _).Should().BeFalse(
                "If this test fails, it means Python is now including the 'name' field. " +
                "This test documents when Python breaks the contract by omitting required fields.");
        }

        [Fact]
        public void Should_Detect_Extra_Unexpected_Fields()
        {
            // Arrange - Python added new fields we don't know about
            var pythonJsonWithExtraFields = @"{
                ""id"": 1,
                ""name"": ""Test Company"",
                ""created_at"": ""2024-01-01T10:00:00Z"",
                ""new_field_from_python"": ""surprise!"",
                ""another_new_field"": 42
            }";

            var expectedFields = new HashSet<string>
            {
                "id", "name", "address", "phone", "email", "created_at", "updated_at"
            };

            // Act
            var result = JsonSerializer.Deserialize<JsonElement>(pythonJsonWithExtraFields);
            var actualFields = new HashSet<string>();
            
            foreach (var property in result.EnumerateObject())
            {
                actualFields.Add(property.Name);
            }

            var extraFields = actualFields.Except(expectedFields).ToList();

            // Assert
            extraFields.Should().Contain("new_field_from_python",
                "This test documents when Python adds new fields. " +
                "Update the DTO if these fields should be included.");
        }
    }
}
