using AutoMapper;
using DistrictEnterpriseStatisticalData.Api.DTO;
using DistrictEnterpriseStatisticalData.Domain.Entity;

namespace DistrictEnterpriseStatisticalData.Api;

public class Mapper: Profile
{
    public Mapper()
    {
        CreateMap<Enterprise, EnterpriseDto>();
        CreateMap<EnterpriseDto, Enterprise>();
        CreateMap<EnterpriseCreateDto, Enterprise>();
        CreateMap<Enterprise, EnterpriseCreateDto>();

        CreateMap<EnterpriseType, EnterpriseTypeDto>();
        CreateMap<EnterpriseTypeDto, EnterpriseType>();
        CreateMap<EnterpriseType, EnterpriseTypeCreateDto>();
        CreateMap<EnterpriseTypeCreateDto, EnterpriseType>();

        CreateMap<FormOfOwnership, FormOfOwnershipDto>();
        CreateMap<FormOfOwnershipDto, FormOfOwnership>();
        CreateMap<FormOfOwnershipCreateDto, FormOfOwnership>();
        CreateMap<FormOfOwnership, FormOfOwnershipCreateDto>();

        CreateMap<Supplier, SupplierDto>();
        CreateMap<SupplierDto, Supplier>();
        CreateMap<SupplierCreateDto, Supplier>();
        CreateMap<Supplier, SupplierCreateDto>();

        CreateMap<Supply, SupplyDto>();
        CreateMap<SupplyDto, Supply>();
        CreateMap<SupplyCreateDto, Supply>();
        CreateMap<Supply, SupplyCreateDto>();
    }
}