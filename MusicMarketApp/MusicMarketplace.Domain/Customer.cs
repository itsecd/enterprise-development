﻿namespace MusicMarketplace.Domain;

/// <summary>
/// Покупатель.
/// </summary>
public class Сustomer
{
    /// <summary>
    /// ID Покупателя.
    /// </summary>
    public int Id;

    /// <summary>
    /// Ф.И.О.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Страна проживания.
    /// </summary>
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// Адрес.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// История заказов. 
    /// </summary>
    public List<Purchase> Purchases = [];

    public Сustomer() { }

    public Сustomer(int id, string name, string country, string address, List<Purchase> purchases)
    {
        Id = id;
        Name = name;
        Country = country;
        Address = address;
        Purchases = purchases;
    }
}