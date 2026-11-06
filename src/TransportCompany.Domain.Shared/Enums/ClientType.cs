namespace TransportCompany.Domain.Shared.Enums;

/// <summary>
/// Тип клиента(Физическое лицо, ИП, Юридическое лицо)
/// </summary>
public enum ClientType
{
    /// <summary>
    /// Физическое лицо
    /// </summary>
    PhysicalPerson = 0,

    /// <summary>
    /// Индивидуальный предприниматель
    /// </summary>
    IndividualEntrepreneur = 1,

    /// <summary>
    /// Юридическое лицо (ООО, АО и др.)
    /// </summary>
    LegalPerson = 2

}
