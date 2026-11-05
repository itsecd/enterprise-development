using Microsoft.AspNetCore.Mvc;
using VetClinic.Domain.Exceptions;

namespace VetClinic.Api.Controllers;

public abstract class ClinicControllerBase(ILogger logger) : ControllerBase
{
    protected async Task<ActionResult> Execute(string operation, Func<Task<ActionResult>> action)
    {
        try
        {
            return await action();
        }
        catch (EntityNotFoundException exception)
        {
            logger.LogWarning(exception, "{Operation} завершился ошибкой 404", operation);
            return NotFound(ProblemInfo(StatusCodes.Status404NotFound, "Не найдено", exception.Message));
        }
        catch (EntityInUseException exception)
        {
            logger.LogWarning(exception, "{Operation} завершился ошибкой 409", operation);
            return Conflict(ProblemInfo(StatusCodes.Status409Conflict, "Сущность используется", exception.Message));
        }
        catch (DomainRuleException exception)
        {
            logger.LogWarning(exception, "{Operation} завершился ошибкой 400", operation);
            return BadRequest(ProblemInfo(StatusCodes.Status400BadRequest, "Нарушено правило предметной области", exception.Message));
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "{Operation} завершился ошибкой 500", operation);
            return Problem(title: "Внутренняя ошибка сервера", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    private static ProblemDetails ProblemInfo(int status, string title, string detail) => new()
    {
        Status = status,
        Title = title,
        Detail = detail
    };
}
