namespace VetClinic.Domain.Shared.Enums;
/// <summary>
/// Перечисление видов животных.
/// (Виды выбрал так, чтобы у них были породы.
/// Поэтому не добавил хомячков, змей и попугаев.)
/// </summary>
public enum Species
{
    /// <summary>
    /// Кошка
    /// </summary>
    Cat = 0,
    /// <summary>
    /// Собака
    /// </summary>
    Dog = 1,
    /// <summary>
    /// Голубь
    /// </summary>
    Pigeon = 2,
    /// <summary>
    /// Кролик
    /// </summary>
    Rabbit = 3,
    /// <summary>
    /// Лошадь
    /// </summary>
    Horse = 4
}