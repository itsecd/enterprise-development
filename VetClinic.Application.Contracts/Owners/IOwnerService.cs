namespace VetClinic.Application.Contracts.Owners;

/// <summary>
/// Интерфейс прикладного сервиса для управления профилями владельцев животных.
/// </summary>
public interface IOwnerAppService : IApplicationService<OwnerDto, OwnerCreateUpdateDto, int>;
