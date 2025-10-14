using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
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
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>(); // Registra o Handler como um serviço transitório

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/v1/categories",
                    (CreateCategoryRequest request,
                     ICategoryHandler handler) 
                => handler.CreateAsync(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response<Category?>>();

app.MapPut("/v1/categories/{id}",
                    (long id, 
                     UpdateCategoryRequest request,
                     ICategoryHandler handler) 
                =>
                    {
                        request.Id = id;
                        handler.UpdateAsync(request);
                    })
    .WithName("Categories: Update")
    .WithSummary("Altera uma categoria existente")
    .Produces<Response<Category?>>();

app.MapDelete("/v1/categories/{id}",
                    (long id, 
                     DeleteCategoryRequest request,
                     ICategoryHandler handler) 
                =>
                    {
                        request.Id = id;
                        handler.DeleteAsync(request);
                    })
    .WithName("Categories: Delete")
    .WithSummary("Exclui uma categoria existente")
    .Produces<Response<Category?>>();


app.Run();

