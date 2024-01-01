using Microsoft.EntityFrameworkCore;
using Products.Data;
using Products.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductDataContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ProductsContext") ?? throw new InvalidOperationException("Connection string 'ProductsContext' not found.")));

// Add observability code here
builder.Services.AddObservability("Products", builder.Configuration, ["eShopLite.Products"]);
builder.Services.AddSingleton<ProductsMetrics>();
// Add services to the container.
var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapProductEndpoints();

app.UseStaticFiles();

app.CreateDbIfNotExists();

app.MapObservability();

app.Run();
