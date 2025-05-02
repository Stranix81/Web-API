using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Core.Models;
using Core.Models.FunctionsReturnModels;
using Core.DTOs;
using System.Reflection;

namespace API
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                AddXmlComments(c);
                AddSwaggerDoc(c);
                ConfigureDataTypes(c);
                ConfigureModelExamples(c);
            });
        }

        private static void AddXmlComments(SwaggerGenOptions c)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        }

        private static void AddSwaggerDoc(SwaggerGenOptions c)
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        }

        private static void ConfigureDataTypes(SwaggerGenOptions c)
        {
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
        }

        private static void ConfigureModelExamples(SwaggerGenOptions c)
        {
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
        }
    }
}
