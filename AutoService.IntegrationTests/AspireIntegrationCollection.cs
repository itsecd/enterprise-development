namespace AutoService.IntegrationTests;

/// <summary>
/// Коллекция интеграционных тестов: тесты выполняются последовательно
/// и совместно используют один экземпляр <see cref="AspireAppFixture"/>.
/// </summary>
[CollectionDefinition("Aspire integration")]
public class AspireIntegrationCollection : ICollectionFixture<AspireAppFixture>;
