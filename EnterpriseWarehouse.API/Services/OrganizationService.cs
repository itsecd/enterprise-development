using EnterpriseWarehouse.Domain.Entities;

namespace EnterpriseWarehouse.API.Services;

public class OrganizationService
{
    private readonly List<Organization> _organizations = [];

    private int _id = 1;

    public List<Organization> GetOrganizations()
    {
        return _organizations;
    }

    public Organization? GetOrganizationById(int id)
    {
        return _organizations.Find(o => o.Id == id);
    }

    public Organization AddOrganization(Organization organization)
    {
        organization.Id = _id++;
        _organizations.Add(organization);
        return organization;
    }

    public bool DeleteOrganization(int id)
    {
        var organization = GetOrganizationById(id);
        if (organization == null)
        {
            return false;
        }
        _organizations.Remove(organization);
        return true;
    }

    public bool UpdateOrganization(Organization updatedOrganization)
    {
        var organization = GetOrganizationById(updatedOrganization.Id);
        if (organization == null)
        {
            return false;
        }
        organization.Address = updatedOrganization.Address;
        organization.Name = updatedOrganization.Name;
        return true;
    }
}
