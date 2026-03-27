using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Api.Models;
using Dima.Core;
using Dima.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Common.Api;

public static class BuilderExtension
{
    public static void AddConfiguration(this WebApplicationBuilder builder)
    {
        Configuration.ConnectionString = 
            builder
            .Configuration
            .GetConnectionString("DefaultConnection") 
            ?? string.Empty;
    }

    public static void AddDocumentation(this WebApplicationBuilder builder)
    {
        // Swagger
        // OpenAPI nativo .NET 10
        builder.Services.AddEndpointsApiExplorer(); // Necessário para o Swagger
        builder.Services.AddOpenApi();
        
        //Document(document =>
        //{
        //    document.DocumentName = "v1";
        //    document.Title = "Dima API";
        //    document.Version = "v1.0.0";
        //    document.Description = "API Dima - .NET 10";
        //});
        /*
        builder.Services.AddEndpointsApiExplorer(); // Necessário para o Swagger
        builder.Services.AddSwaggerGen( // Necessário para o Swagger
            x =>
            {
                x.CustomSchemaIds(type => type.FullName ?? type.Name); // Necessário para o Swagger colocar o Full Qualified Name
            });
        */
    }

    public static void AddSecurity(this WebApplicationBuilder builder)
    {
        // A ordem das duas chamadas abaixo deve ser mantida
        builder.Services                                                // Quem você é?
            .AddAuthentication(IdentityConstants.ApplicationScheme)
            .AddIdentityCookies();
        builder.Services.AddAuthorization();        // O que você pode fazer?
    }

    public static void AddDataContexts(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddIdentityCore<User>() // Classe User customizada - O padrão é IdentityUser
            .AddRoles<IdentityRole<long>>() // IdentityRole padrão usando long
            .AddEntityFrameworkStores<AppDbContext>() // Armazenamento dos dados de identity
            .AddApiEndpoints(); // Cadastro padrão de endpoints da API

        builder.Services.AddDbContext<AppDbContext>(x =>
        {
            x.UseSqlServer(Configuration.ConnectionString);
        });
    }

    public static void AddCrossOrigin(this WebApplicationBuilder builder)
    {
        
    }

    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

        builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
    }
}