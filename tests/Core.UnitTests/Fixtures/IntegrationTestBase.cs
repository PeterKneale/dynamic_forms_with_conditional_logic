namespace Core.UnitTests;

[Collection(nameof(IntegrationTestCollection))]
public class IntegrationTestBase
{
    protected IntegrationTestFixture _fixture;

    public IntegrationTestBase(IntegrationTestFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _fixture.OutputHelper = output;
    }
}