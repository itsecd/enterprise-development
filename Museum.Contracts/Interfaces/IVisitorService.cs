using Museum.Contracts.Dto;

namespace Museum.Contracts.Interfaces;

/// <summary>
/// Описывает работу с посетителями музея
/// </summary>
public interface IVisitorService
    : IEntityService<VisitorDto, VisitorEditDto>;
