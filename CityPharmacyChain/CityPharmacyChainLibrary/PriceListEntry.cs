namespace CityPharmacyChain.Domain;

public class PriceListEntry(int productCode, double price, string company, string manufacturer, PaymentType paymentType, DateTime saleDate)
{
    public int ProductCode { get; set; } = productCode;
    public double Price { get; set; } = price;
    public string Company { get; set; } = company;
    public string Manufacturer { get; set; } = manufacturer;
    public PaymentType PaymentType { get; set; } = paymentType;
    public DateTime SaleDate { get; set; } = saleDate;
}
