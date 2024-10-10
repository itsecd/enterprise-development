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

        CreateMap<EnterpriseType, EnterpriseTypeDto>();
        CreateMap<EnterpriseTypeDto, EnterpriseType>();

        CreateMap<FormOfOwnership, FormOfOwnershipDto>();
        CreateMap<FormOfOwnershipDto, FormOfOwnership>();

        CreateMap<Supplier, SupplierDto>();
        CreateMap<SupplierDto, Supplier>();

        CreateMap<Supply, SupplyDto>();
        CreateMap<SupplyDto, Supply>();
    }
}