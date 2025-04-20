using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Core.Enums;
using Core.Models;
using Core.Models.FunctionsReturnModels;
using Core.DTOs;
using Infrastructure.Services;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        npgsqlOptions =>
        {
            npgsqlOptions.MapEnum<OrderStatus>("schema_1.order_status");
        }
    )
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging();
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    //c.SchemaFilter<HideSchemaFilter>();

    //data type examples
    c.MapType<TimeOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "time",
        Default = OpenApiAnyFactory.CreateFromJson($"\"{TimeOnly.FromDateTime(DateTime.Now):HH:mm:ss}\"")
    });
    c.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Default = OpenApiAnyFactory.CreateFromJson($"\"{DateOnly.FromDateTime(DateTime.Now):yyyy-MM-dd}\"")
    });
    c.MapType<decimal>(() => new OpenApiSchema
    {
        Type = "number",
        Format = "decimal",
        Default = OpenApiAnyFactory.CreateFromJson("100.50")
    });

    //model examples
    c.MapType<BdaySums>(() => new OpenApiSchema
    {
        Example = OpenApiAnyFactory.CreateFromJson("""
            {
                "sum": 100.50,
                "client_name": "Ivan",
                "client_lastname": "Ivanov"
            }
            """)
    });
    c.MapType<Client>(() => new OpenApiSchema
    {
        Example = OpenApiAnyFactory.CreateFromJson("""
            {
                "id": 0,
                "name": "Ivan",
                "lastname": "Ivanov",
                "birth_date": "1999-12-31"
            }
            """)
    });
    c.MapType<PostPutClientDTO>(() => new OpenApiSchema
    {
        Example = OpenApiAnyFactory.CreateFromJson("""
            {
                "name": "Ivan",
                "lastname": "Ivanov",
                "birth_date": "1999-12-31"
            }
            """)
    });
    c.MapType<AvgCostsByHour>(() => new OpenApiSchema
    {
        Example = OpenApiAnyFactory.CreateFromJson("""
            {
                "hour": 1,
                "avg_cost": 100.50
            }
            """)
    });
});

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
