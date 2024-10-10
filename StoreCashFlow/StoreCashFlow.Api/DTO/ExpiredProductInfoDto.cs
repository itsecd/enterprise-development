using StoreCashFlow.Domain;

namespace StoreCashFlow.Api.DTO;

public class ExpiredProductInfoDto
{
    public required Product Product { get; set; }
    public required Store Store { get; set; }
}
