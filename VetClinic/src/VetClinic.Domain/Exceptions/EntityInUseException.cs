namespace VetClinic.Domain.Exceptions;

public sealed class EntityInUseException(Exception innerException)
    : Exception("Запись используется другими объектами.", innerException);
