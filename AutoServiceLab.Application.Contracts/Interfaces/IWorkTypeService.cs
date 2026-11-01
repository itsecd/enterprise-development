using AutoServiceLab.Application.Contracts.DTOs.WorkTypes;

namespace AutoServiceLab.Application.Contracts.Interfaces;

/// <summary>
/// Контракт для работы с видами работ
/// </summary>
public interface IWorkTypeService : IApplicationService<WorkTypeDto, WorkTypeCreateUpdateDto, int>
{
}
