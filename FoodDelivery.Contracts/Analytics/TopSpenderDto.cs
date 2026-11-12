namespace FoodDelivery.Contracts.Analytics;

/// <summary>
/// DTO клиента с общей суммой его расходов.
/// </summary>
public record TopSpenderDto(int ClientId, string FullName, decimal TotalSpent);