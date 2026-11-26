using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using VetClinic.Api.Middleware;
using VetClinic.Application.Exceptions;

namespace VetClinic.Tests;

public class ExceptionHandlerTests
{
    [Fact]
    public async Task InvalidPetBreed_ReturnsBadRequest()
    {
        var response = new CapturingProblemDetailsService();
        var handler = new InvalidPetBreedExceptionHandler(response, NullLogger<InvalidPetBreedExceptionHandler>.Instance);
        var context = new DefaultHttpContext();
        var error = new InvalidPetBreedException("Порода относится к другому виду");

        Assert.True(await handler.TryHandleAsync(context, error, TestContext.Current.CancellationToken));
        Assert.Equal(StatusCodes.Status400BadRequest, context.Response.StatusCode);
        Assert.Equal(error.Message, response.Details?.Detail);
    }

    [Fact]
    public async Task UnexpectedArgumentError_IsNotHandledAsBadRequest()
    {
        var response = new CapturingProblemDetailsService();
        var handler = new InvalidPetBreedExceptionHandler(response, NullLogger<InvalidPetBreedExceptionHandler>.Instance);
        var context = new DefaultHttpContext();

        Assert.False(await handler.TryHandleAsync(context, new ArgumentNullException("value"), TestContext.Current.CancellationToken));
        Assert.Null(response.Details);
    }

    [Fact]
    public async Task ConcurrencyError_ReturnsConflict()
    {
        var response = new CapturingProblemDetailsService();
        var handler = new DbUpdateExceptionHandler(response, NullLogger<DbUpdateExceptionHandler>.Instance);
        var context = new DefaultHttpContext();
        var error = new DbUpdateConcurrencyException("Internal error");

        Assert.True(await handler.TryHandleAsync(context, error, TestContext.Current.CancellationToken));
        Assert.Equal(StatusCodes.Status409Conflict, context.Response.StatusCode);
        Assert.Equal(StatusCodes.Status409Conflict, response.Details?.Status);
        Assert.NotEqual(error.Message, response.Details?.Detail);
    }

    [Fact]
    public async Task UnexpectedDbUpdateError_IsNotHandledAsConflict()
    {
        var response = new CapturingProblemDetailsService();
        var handler = new DbUpdateExceptionHandler(response, NullLogger<DbUpdateExceptionHandler>.Instance);
        var context = new DefaultHttpContext();
        var error = new DbUpdateException("Write failed", new InvalidOperationException("Missing column"));

        Assert.False(await handler.TryHandleAsync(context, error, TestContext.Current.CancellationToken));
        Assert.Null(response.Details);
    }

    private sealed class CapturingProblemDetailsService : IProblemDetailsService
    {
        public ProblemDetails? Details { get; private set; }

        public ValueTask WriteAsync(ProblemDetailsContext context)
        {
            Details = context.ProblemDetails;
            return ValueTask.CompletedTask;
        }

        public async ValueTask<bool> TryWriteAsync(ProblemDetailsContext context)
        {
            await WriteAsync(context);
            return true;
        }
    }
}
