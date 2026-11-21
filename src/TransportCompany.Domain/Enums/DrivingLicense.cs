namespace TransportCompany.Domain.Enums;

/// <summary>
/// Категория водительского удостоверения
/// </summary>
public enum DrivingLicense
{
    /// <summary>
    /// Мопеды
    /// </summary>
    M = 0,

    /// <summary>
    /// Мотоциклы
    /// </summary>
    A = 1,

    /// <summary>
    /// Лёгкие мотоциклы
    /// </summary>
    A1 = 2,

    /// <summary>
    /// Легковые автомобили
    /// </summary>
    B = 3,

    /// <summary>
    /// Трициклы и квадрициклы
    /// </summary>
    B1 = 4,

    /// <summary>
    /// Легковые автомобили с прицепом
    /// </summary>
    BE = 5,

    /// <summary>
    /// Грузовые автомобили
    /// </summary>
    C = 6,

    /// <summary>
    /// Лёгкие грузовые автомобили
    /// </summary>
    C1 = 7,

    /// <summary>
    /// Грузовые автомобили с прицепом
    /// </summary>
    CE = 8,

    /// <summary>
    /// Лёгкие грузовые автомобили с прицепом
    /// </summary>
    C1E = 9,

    /// <summary>
    /// Автобусы
    /// </summary>
    D = 10,

    /// <summary>
    /// Малые автобусы
    /// </summary>
    D1 = 11,

    /// <summary>
    /// Автобусы с прицепом
    /// </summary>
    DE = 12,

    /// <summary>
    /// Малые автобусы с прицепом
    /// </summary>
    D1E = 13,

    /// <summary>
    /// Трамваи
    /// </summary>
    Tm = 14,

    /// <summary>
    /// Троллейбусы
    /// </summary>
    Tb = 15
}