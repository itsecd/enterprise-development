using EnterpriseWarehouse.Domain.Entities;

namespace EnterpriseWarehouse.API.Services;

public class SupplyService
{
    private readonly List<Supply> _supplies = [];

    private int _id = 1;

    public List<Supply> GetSupplies()
    {
        return _supplies;
    }

    public Supply? GetSupplyById(int id)
    {
        return _supplies.Find(o => o.Id == id);
    }

    public Supply AddSupply(Supply supply)
    {
        supply.Id = _id++;
        _supplies.Add(supply);
        return supply;
    }

    public bool DeleteSupply(int id)
    {
        var supply = GetSupplyById(id);
        if (supply == null)
        {
            return false;
        }
        _supplies.Remove(supply);
        return true;
    }

    public bool UpdateSupply(Supply updatedSupply)
    {
        var supply = GetSupplyById(updatedSupply.Id);
        if (supply == null)
        {
            return false;
        }
        supply.Product = updatedSupply.Product;
        supply.Organization = updatedSupply.Organization;
        supply.SupplyDate = updatedSupply.SupplyDate;
        supply.Quantity = updatedSupply.Quantity;
        return true;
    }
}
