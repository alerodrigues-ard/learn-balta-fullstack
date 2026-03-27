using System.Security.Claims;
using Dima.Api.Common.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Api.Models;
using Dima.Core.Handlers;
using Dima.Api.Endpoints;
using Dima.Core;
using Microsoft.OpenApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();

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

// A ordem das duas chamadas abaixo deve ser mantida
builder.Services                                                // Quem você é?
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();
builder.Services.AddAuthorization();        // O que você pode fazer?

builder.Services
    .AddIdentityCore<User>() // Classe User customizada - O padrão é IdentityUser
    .AddRoles<IdentityRole<long>>() // IdentityRole padrão usando long
    .AddEntityFrameworkStores<AppDbContext>() // Armazenamento dos dados de identity
    .AddApiEndpoints(); // Cadastro padrão de endpoints da API

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(Configuration.ConnectionString);
});



builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // OpenAPI JSON endpoint
    app.MapOpenApi();  // /openapi/v1.json

    // Swagger UI nativo (Scalar)
    app.MapScalarApiReference();  // /scalar/v1 (UI moderna)
}


// As suas chamadas abaixo devem estar nesta ordem
app.UseAuthentication();    // Quem eu sou
app.UseAuthorization();     // O que eu posso fazer

//app.UseSwagger();
//app.UseSwaggerUI();

app.MapEndpoints();

app.Run();

