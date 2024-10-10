using StoreCashFlow.Domain;

namespace StoreCashFlow.Api.Service;

public class CustomerService()
{
    private List<Customer> _customers = [];
    private int _customerId = 1;
    public Customer AddCustomer(Customer newCustomer)
    {
        newCustomer.CustomerId = _customerId++;
        _customers.Add(newCustomer);
        return newCustomer;
    }

    public List<Customer> GetCustomers()
    {
        return _customers;
    }
    public Customer? GetCustomerById(int id)
    {
        return _customers.First(c => c.CustomerId == id);
    }

    public bool DeleteCustomer(int id)
    {
        var customer = GetCustomerById(id);
        if (customer == null)
        {
            return false;
        }
        _customers.Remove(customer);
        return true;
    }

    public bool UpdateCustomer(Customer updateCustomer)
    {
        var customer = GetCustomerById(updateCustomer.CustomerId);
        if (customer == null)
        {
            return false;
        }
        customer.FirstName = updateCustomer.FirstName;
        customer.LastName = updateCustomer.LastName;
        customer.Potronimic = updateCustomer.Potronimic;
        customer.CardNumber = updateCustomer.CardNumber;
        return true;
    }
}
