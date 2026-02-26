using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Api.Models;
using Dima.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Dima.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;

// Swagger
builder.Services.AddEndpointsApiExplorer(); // Necessário para o Swagger
builder.Services.AddSwaggerGen( // Necessário para o Swagger
    x =>
    {
        x.CustomSchemaIds(type => type.FullName ?? type.Name); // Necessário para o Swagger colocar o Full Qualified Name
    });

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
    x.UseSqlServer(connString);
});



builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

var app = builder.Build();

// As suas chamadas abaixo devem estar nesta ordem
app.UseAuthentication();    // Quem eu sou
app.UseAuthorization();     // O que eu posso fazer

app.UseSwagger();
app.UseSwaggerUI();

app.MapEndpoints();

app.Run();

