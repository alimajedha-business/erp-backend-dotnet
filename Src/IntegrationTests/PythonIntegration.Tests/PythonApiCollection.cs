using Xunit;

namespace NGErp.PythonIntegration.Tests
{
    [CollectionDefinition("PythonApi")]
    public class PythonApiCollection : ICollectionFixture<Fixtures.PythonApiTestFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
