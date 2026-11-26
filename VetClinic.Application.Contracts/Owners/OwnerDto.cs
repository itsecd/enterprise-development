namespace VetClinic.Application.Contracts.Owners;
/// <summary>
/// DTO исходящих данных для отображения информации о владельцах
/// </summary>
/// <param name="Id"></param>
/// <param name="FullName"></param>
/// <param name="Phone"></param>
/// <param name="Adress"></param>
public record OwnerDto(int Id, string FullName, string Phone, string Adress);

