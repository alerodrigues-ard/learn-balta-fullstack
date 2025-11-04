using Dima.Api.Data;
using Dima.Api.Endpoints;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(connString);
});



builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapEndpoints();

app.Run();

