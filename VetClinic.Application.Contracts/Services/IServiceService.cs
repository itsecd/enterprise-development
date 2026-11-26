namespace VetClinic.Application.Contracts.Services;

/// <summary>
/// Интерфейс прикладного сервиса для управления перечнем медицинских услуг и тарифов клиники.
/// </summary>
public interface IServiceAppService : IApplicationService<ServiceDto, ServiceCreateUpdateDto, int>;
