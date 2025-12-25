using System.Reflection;
using System.Text.Json;

namespace NGErp.PythonIntegration.Tests.Helpers
{
    /// <summary>
    /// Helper class for validating API contracts
    /// </summary>
    public static class ContractValidator
    {
        /// <summary>
        /// Validates that a type has all expected properties
        /// </summary>
        public static void ValidateProperties(Type dtoType, params string[] expectedProperties)
        {
            var actualProperties = dtoType.GetProperties()
                .Select(p => p.Name)
                .ToHashSet();

            foreach (var expected in expectedProperties)
            {
                if (!actualProperties.Contains(expected))
                {
                    throw new Exception(
                        $"Property '{expected}' not found in {dtoType.Name}. " +
                        $"Python API may have changed. Available properties: {string.Join(", ", actualProperties)}");
                }
            }
        }

        /// <summary>
        /// Validates that a JSON response has all expected fields
        /// </summary>
        public static void ValidateJsonFields(JsonElement json, params string[] expectedFields)
        {
            var missingFields = new List<string>();

            foreach (var field in expectedFields)
            {
                if (!json.TryGetProperty(field, out _))
                {
                    missingFields.Add(field);
                }
            }

            if (missingFields.Any())
            {
                throw new Exception(
                    $"Missing required fields in JSON response: {string.Join(", ", missingFields)}. " +
                    "Python API may have removed these fields.");
            }
        }

        /// <summary>
        /// Gets all unexpected fields in a JSON response
        /// </summary>
        public static List<string> GetUnexpectedFields(JsonElement json, params string[] expectedFields)
        {
            var expectedSet = new HashSet<string>(expectedFields);
            var unexpectedFields = new List<string>();

            foreach (var property in json.EnumerateObject())
            {
                if (!expectedSet.Contains(property.Name))
                {
                    unexpectedFields.Add(property.Name);
                }
            }

            return unexpectedFields;
        }

        /// <summary>
        /// Validates that a property has the expected type
        /// </summary>
        public static void ValidatePropertyType(Type dtoType, string propertyName, Type expectedType)
        {
            var property = dtoType.GetProperty(propertyName);
            
            if (property == null)
            {
                throw new Exception($"Property '{propertyName}' not found in {dtoType.Name}");
            }

            if (property.PropertyType != expectedType)
            {
                throw new Exception(
                    $"Property '{propertyName}' in {dtoType.Name} has type {property.PropertyType.Name}, " +
                    $"but expected {expectedType.Name}. Python API may have changed the field type.");
            }
        }

        /// <summary>
        /// Compares two DTOs field by field
        /// </summary>
        public static Dictionary<string, (object? Expected, object? Actual)> CompareObjects<T>(T expected, T actual)
        {
            var differences = new Dictionary<string, (object?, object?)>();
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var expectedValue = property.GetValue(expected);
                var actualValue = property.GetValue(actual);

                if (!Equals(expectedValue, actualValue))
                {
                    differences[property.Name] = (expectedValue, actualValue);
                }
            }

            return differences;
        }
    }
}
