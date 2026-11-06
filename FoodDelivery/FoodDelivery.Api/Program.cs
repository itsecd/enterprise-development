using Microsoft.AspNetCore.Mvc.ApplicationModels;
using FoodDelivery.Api;
using FoodDelivery.Application.Contracts.Analytics;
using FoodDelivery.Application.Contracts.Orders;
using FoodDelivery.Application.Contracts.Dishes;
using FoodDelivery.Application.Contracts.Restaurants;
using FoodDelivery.Application.Contracts.Customers;
using FoodDelivery.Api.ErrorHandling;
using FoodDelivery.Application.Contracts.Categories;
using FoodDelivery.Application.Contracts.Common;
using FoodDelivery.Application.Services;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Repositories;
using FoodDelivery.Infrastructure.InMemory.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(
        new RouteTokenTransformerConvention(
            new LowercaseRouteTransformer()));
}).ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var problem = new Microsoft.AspNetCore.Mvc.ValidationProblemDetails(
            context.ModelState)
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Ошибка проверки входных данных",
            Instance = context.HttpContext.Request.Path.Value
        };

        var result = new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(problem);
        result.ContentTypes.Add("application/problem+json");

        return result;
    };
});
builder.Services.AddOpenApi(options =>
{
    options.AddOperationTransformer((operation, context, cancellationToken) =>
    {
        if (operation.Responses is not null)
        {
            foreach (var response in operation.Responses)
            {
                var content = response.Value.Content;
                var media = content?.Values.FirstOrDefault();

                if (media is null)
                {
                    continue;
                }

                var isError = int.TryParse(response.Key, out var status)
                    && status >= 400;

                content!.Clear();

                content[isError
                    ? "application/problem+json"
                    : "application/json"] = media;
            }
        }

        return Task.CompletedTask;
    });
});

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();

builder.Services.AddSingleton<FoodDeliveryData>();
builder.Services.AddSingleton<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<
    ICrudService<CategoryDto, CategoryRequest>,
    CategoryService>();

builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();

builder.Services.AddScoped<
    ICrudService<CustomerDto, CustomerRequest>,
    CustomerService>();

builder.Services.AddSingleton<IRestaurantRepository, RestaurantRepository>();

builder.Services.AddScoped<
    ICrudService<RestaurantDto, RestaurantRequest>,
    RestaurantService>();

builder.Services.AddSingleton<IDishRepository, DishRepository>();

builder.Services.AddScoped<IDishService, DishService>();

builder.Services.AddScoped<ICrudService<DishDto, DishRequest>>(
    services => services.GetRequiredService<IDishService>());

builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IOrderItemService, OrderItemService>();

builder.Services.AddSingleton<IAnalyticsRepository, AnalyticsRepository>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.WithTitle("API службы доставки еды");
    });
}

app.MapControllers();

app.Run();
