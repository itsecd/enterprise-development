using Museum.Contracts.Dto;

namespace Museum.Contracts.Interfaces;

/// <summary>
/// Описывает работу с экскурсиями музея
/// </summary>
public interface ITourService
    : IEntityService<TourDto, TourEditDto>;
