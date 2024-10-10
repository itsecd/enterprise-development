using Microsoft.AspNetCore.Mvc;
using StoreCashFlow.Api.Service;
using StoreCashFlow.Domain;
namespace StoreCashFlow.Api.Controller;

/// <summary>
/// Контроллер для работы с покупателями
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CustomerController(CustomerService customerService) : ControllerBase
{
    /// <summary>
    /// Получить всех покупателей
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Customer>> GetCustomers()
    {
        return Ok(customerService.GetCustomers());
    }

    /// <summary>
    /// Получить покупателя по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    public ActionResult<Customer> GetCustomer(int id)
    {
        return Ok(customerService.GetCustomerById(id));
    }

    /// <summary>
    /// Добавить покупателя
    /// </summary>
    [HttpPost]
    public ActionResult<Customer> AddCustomer(Customer newCustomer)
    {
        return Ok(customerService.AddCustomer(newCustomer));
    }

    /// <summary>
    /// Обновить покупателя
    /// </summary>
    [HttpPut]
    public IActionResult UpdateCustomer(Customer customer)
    {
        var result = customerService.UpdateCustomer(customer);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }

    /// <summary>
    /// Удалить покупателя
    /// </summary>
    [HttpDelete("{id}")]
    public IActionResult DeleteCustomer(int id)
    {
        var result = customerService.DeleteCustomer(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }
}
