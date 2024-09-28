namespace RealEstateAgency.Domain;

/// <summary>
/// Клиент агентства недвижимости
/// </summary>
public class Client
{
    /// <summary>
    /// идентификатор клиента
    /// </summary>
    public int ClientId { get; private set; }

    /// <summary>
    /// ФИО клиента
    /// </summary>
    public string FirstAndLastName { get; private set; }

    /// <summary>
    /// паспорт
    /// </summary>
    public string Pasport { get; private set; }

    /// <summary>
    /// номер телефона
    /// </summary>
    public string NumberPhone { get; private set; }

    /// <summary>
    /// адрес прописки
    /// </summary>
    public string Address { get; private set; }

    /// <summary>
    /// почта
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Заявки клиента
    /// </summary>
    public List<Order> Orders { get; set; } = new List<Order>();

    /// <summary>
    /// конструктор для создания клиента
    /// </summary>
    /// <param name="clientId">идентификатор клиента</param>
    /// <param name="firstAndLastName">ФИО</param>
    /// <param name="pasport">паспорт</param>
    /// <param name="numberPhone">номер телефона</param>
    /// <param name="address">адрес прописки</param>
    /// <param name="email">почта</param>
    public Client(int clientId, string firstAndLastName, string pasport, string numberPhone, string address, string email)
    {
        ClientId = clientId;
        FirstAndLastName = firstAndLastName;
        Pasport = pasport;
        NumberPhone = numberPhone;
        Address = address;
        Email = email;
    }
}
