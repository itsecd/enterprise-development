namespace VetClinic.Application.Contracts.Owners;
/// <summary>
/// DTO входящих данных для внесения или изменения информации о владельцах
/// </summary>
/// <param name="FullName">ФИО владельца</param>
/// <param name="Phone">Телефон владельца</param>
/// <param name="Adress">Адрес владельца</param>
public record OwnerCreateUpdateDto(string FullName, string Phone, string Adress);
/// <summary>
/// DTO исходящих данных для отображения информации о владельцах
/// </summary>
/// <param name="Id"></param>
/// <param name="FullName"></param>
/// <param name="Phone"></param>
/// <param name="Adress"></param>
public record OwnerDto(int Id, string FullName, string Phone, string Adress);

