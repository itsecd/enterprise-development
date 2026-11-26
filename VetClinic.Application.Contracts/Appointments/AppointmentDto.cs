namespace VetClinic.Application.Contracts.Appointments;
/// <summary>
/// DTO исходящих данных для оформления или редактирования записи на прием
/// </summary>
/// <param name="DateTime">Дата и время проведения приема</param>
/// <param name="Diagnosis">Предварительный или итоговый диагноз</param>
/// <param name="PetName">Кличка питомца</param>
/// <param name="VetName">ФИО врача</param>
/// <param name="ServiceTitle">Название оказываемой медицинской услуги</param>
/// <param name="RoomNumber">Номер смотрового кабинета</param>
/// <param name="VisitType">Признак первичного или повторного обращения(Да/Нет)</param>
public record AppointmentDto(int Id, DateTime DateTime, string? Diagnosis, string PetName, string VetName, string ServiceTitle, decimal Price, int RoomNumber, string? VisitType);


