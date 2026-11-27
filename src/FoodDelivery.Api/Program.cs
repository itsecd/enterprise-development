using FoodDelivery.Api;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var data = new FoodDeliveryData();
ClientEndpoints.Map(app, data);
RestaurantEndpoints.Map(app, data);
CategoryEndpoints.Map(app, data);
DishEndpoints.Map(app, data);
OrderEndpoints.Map(app, data);
OrderItemEndpoints.Map(app, data);
AnalyticsEndpoints.Map(app, data);

app.Run();
