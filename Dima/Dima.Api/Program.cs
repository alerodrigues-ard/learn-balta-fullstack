using Dima.Api.Data;
using Dima.Core.Enums;
using Dima.Core.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connString = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(connString);
});

// Swagger
builder.Services.AddEndpointsApiExplorer(); // Necessário para o Swagger
builder.Services.AddSwaggerGen( // Necessário para o Swagger
    x =>
    {
        x.CustomSchemaIds(type => type.FullName ?? type.Name); // Necessário para o Swagger colocar o Full Qualified Name
    });

builder.Services.AddTransient<Handler>(); // Registra o Handler como um serviço transitório
// builder.Services.AddTransient<ICategoryHandler, CategoryHandler>(); // Registra o Handler como um serviço transitório

var app = builder.Build();

app.UseSwagger(); // Necessário para o Swagger
app.UseSwaggerUI();

/*
app.MapPost("/v1/transactions", (Request request, Handler handler) => handler.Handle(request))
    .WithName("Transactions: Create")           // Ajuda na documentação do Swagger
    .WithSummary("Cria uma nova transação")     // Ajuda na documentação do Swagger
    .Produces<Response>();                      // Garante que a resposta ser� do tipo Response
*/

app.MapPost("/v1/categories",
                    (Request request,
                     Handler handler) 
                => handler.Handle(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response>();


//app.MapPut("/v1/transactions", () => "API is Running");
//app.MapDelete("/v1/transactions", () => "API is Running");


app.Run();

