namespace MusicMarketplace.Domain;

/// <summary>
/// Товар.
/// </summary>
public class Product
{
    /// <summary>
    /// ID Товара.
    /// </summary>
    public int Id;

    /// <summary>
    /// Тип аудионосителя.
    /// </summary>
    public CarrierType TypeOfCarrier { get; set; }

    /// <summary>
    /// Тип издания: альбом|сингл.
    /// </summary>
    public PublicationType PublicationType { get; set; }

    /// <summary>
    /// Исполнитель
    /// </summary>
    public string Creator { get; set; } = string.Empty;

    /// <summary>
    /// Название 
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Страна издания.
    /// </summary>
    public string MadeIn { get; set; } = string.Empty;

    /// <summary>
    /// Cостояние аудионосителя.
    /// </summary>
    public MediaStatus MediaStatus { get; set; }

    /// <summary>
    /// Cостояние упаковки.
    /// </summary>
    public PackagingStatus PackagingCondition { get; set; }

    /// <summary>
    /// Цена
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Cтатус: в продаже || продан. 
    /// </summary>
    public ProductStatus Status { get; set; }

    /// <summary>
    /// Продавец
    /// </summary>
    public Seller? Seller { get; set; }

    public Product() { }

    public Product(int id, CarrierType typeOfCarrier, PublicationType publicationType, string creator, string name, string madeIn,
        MediaStatus mediaStatus, PackagingStatus packagingCondition, double price, ProductStatus status, Seller seller)
    {
        Id = id;
        TypeOfCarrier = typeOfCarrier;
        PublicationType = publicationType;
        Creator = creator;
        Name = name;
        MadeIn = madeIn;
        MediaStatus = mediaStatus;
        PackagingCondition = packagingCondition;
        Price = price;
        Status = status;
        Seller = seller;
    }
}

/// <summary>
/// Тип аудионосителя.
/// </summary>
public enum CarrierType
{
    Cassette,
    Disc,
    VinylRecord
}

/// <summary>
/// Тип издания.
/// </summary>
public enum PublicationType
{
    Album,
    Single
}

/// <summary>
/// Статус товара.
/// </summary>
public enum ProductStatus
{
    Sale,
    Sold
}

/// <summary>
/// Состояние.
/// </summary>
public enum MediaStatus
{
    New,
    Excellent,
    Good,
    Satisfactory,
    Bad
}

/// <summary>
/// Состояние.
/// </summary>
public enum PackagingStatus
{
    New,
    Excellent,
    Good,
    Satisfactory,
    Bad
}

