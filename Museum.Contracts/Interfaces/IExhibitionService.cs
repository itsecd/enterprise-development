using Museum.Contracts.Dto;

namespace Museum.Contracts.Interfaces;

/// <summary>
/// Описывает работу с выставками музея
/// </summary>
public interface IExhibitionService
    : IEntityService<ExhibitionDto, ExhibitionEditDto>;
