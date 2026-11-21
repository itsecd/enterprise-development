using Microsoft.Extensions.Logging;

namespace FitnessClub.Integration.Tests;

public class IntegrationTest1
{
    private static readonly TimeSpan _defaultTimeout = TimeSpan.FromSeconds(30);

    [Fact]
    public async Task GetWebResourceRootReturnsOkStatusCode()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var appHost = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.FitnessClub_AppHost>(cancellationToken);
        appHost.Services.AddLogging(logging =>
        {
            logging.SetMinimumLevel(LogLevel.Debug);
            // Override the logging filters from the app's configuration
            logging.AddFilter(appHost.Environment.ApplicationName, LogLevel.Debug);
            logging.AddFilter("Aspire.", LogLevel.Debug);
        });
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        await using var app = await appHost.BuildAsync(cancellationToken)
            .WaitAsync(_defaultTimeout, cancellationToken);
        await app.StartAsync(cancellationToken)
            .WaitAsync(_defaultTimeout, cancellationToken);

        // Act
        using var httpClient = app.CreateHttpClient("webfrontend");
        await app.ResourceNotifications.WaitForResourceHealthyAsync(
            "webfrontend", cancellationToken)
            .WaitAsync(_defaultTimeout, cancellationToken);
        using var response = await httpClient.GetAsync("/", cancellationToken);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}