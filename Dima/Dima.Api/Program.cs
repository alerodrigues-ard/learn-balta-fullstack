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

// Request
public class Request
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
//    public DateTime? PaidOrReceivedAt { get; set; }
    public int /*ETransactionType*/ Type { get; set; } // = ETransactionType.Withdrawal;
    public decimal Amount { get; set; }
    public long CategoryId { get; set; }
    public string UserId { get; set; } = string.Empty;
}

// Response
public class Response
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? PaidOrReceivedAt { get; set; }
    public int /*ETransactionType*/ Type { get; set; } // = ETransactionType.Withdrawal;
    public decimal Amount { get; set; }
    public long CategoryId { get; set; }
    public string UserId { get; set; } = string.Empty;
}


// Handler
public class Handler(AppDbContext context)
{
    public Response Handle(Request request)
    {
        var category = new Category
        {
            Title = request.Title,
            Description = request.Description
        };
        
        context.Categories.Add(category);
        context.SaveChanges();
        
        // Aqui você pode implementar a lógica para criar uma nova transação
        // Por exemplo, salvar no banco de dados e retornar a resposta
        return new Response
        {
            Id = category.Id, // Simulando um ID gerado pelo banco de dados
            Title = category.Title,
            // CreatedAt = category.CreatedAt,
            // Type = category.Type,
            // Amount = category.Amount,
            // CategoryId = category.CategoryId,
            // UserId = category.UserId
        };
    }
}
