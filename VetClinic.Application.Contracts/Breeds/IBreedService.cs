namespace VetClinic.Application.Contracts.Breeds;

/// <summary>
/// Интерфейс прикладного сервиса для управления справочником пород животных.
/// </summary>
public interface IBreedAppService : IApplicationService<BreedDto, BreedCreateUpdateDto, int>;
