using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// CRUD-сервис клиентов
/// </summary>
public interface IClientService : ICrudService<ClientDto, ClientEditDto>;