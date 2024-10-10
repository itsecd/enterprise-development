namespace StoreCashFlow.Api.DTO;

public class ProductPriceInfoDto
{
    public int StoreId { get; set; }
    public required string ProductGroupCode { get; set; }
    public double AvgPrice { get; set; }
}
