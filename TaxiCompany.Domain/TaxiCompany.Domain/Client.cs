namespace TaxiCompany.Domain;
/// <summary>
/// Класс <c>Клиент</c> хранит информацию о клиенте
/// </summary>
public class Client
{
    ///<value>Идентификатор клиента</value>
    public required int Id { get; set; }
    ///<value>Имя и фамилия</value>
    public required string FullName { get; set; }
    ///<value>Номер телефона</value>
    public required string PhoneNumber { get; set; }
}
