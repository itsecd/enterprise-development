using StoreCashFlow.Domain;
using StoreCashFlow.Memory;

namespace StoreCashFlow.Api.Service;

public class CustomerService(CustomerCollection customerCollection)
{
    public Customer AddCustomer(Customer newCustomer)
    {
        return customerCollection.AddCustomer(newCustomer);
    }

    public List<Customer> GetCustomers()
    {
        return customerCollection.GetCustomers();
    }
    public Customer? GetCustomerById(int id)
    {
        return customerCollection.GetCustomerById(id);
    }

    public bool DeleteCustomer(int id)
    {
        return customerCollection.DeleteCustomer(id);
    }

    public bool UpdateCustomer(Customer updateCustomer)
    {
        return customerCollection.UpdateCustomer(updateCustomer);
    }
}
