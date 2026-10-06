using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Domain;
/// <summary>
/// Класс клиента
/// </summary>
public class Client
{
    /// <summary>
    /// ID клиента
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// ФИО клиента
    /// </summary>
    public required string FullName { get; set; }
    /// <summary>
    /// Телефон клиента
    /// </summary>
    public required string Phone { get; set; }
    /// <summary>
    /// Адрес доставки
    /// </summary>
    public required string DeliveryAddress { get; set; }
}
