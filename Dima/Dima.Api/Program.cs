using Dima.Api.Data;
using Dima.Core.Enums;
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

//app.MapGet("/v1/transactions", () => "API is Running");

app.MapPost("/v1/transactions", (Request request, Handler handler) => handler.Handle(request))
    .WithName("Transactions: Create")           // Ajuda na documentação do Swagger
    .WithSummary("Cria uma nova transação")     // Ajuda na documentação do Swagger
    .Produces<Response>();                      // Garante que a resposta ser� do tipo Response
/*
app.MapPost("/v1/categories",
                    (CreateGategoryRequest request,
                     ICategoryHandler handler) 
                => handler.CreateAsync(request))
    .WithName("Categories: Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response<Category>>();
*/

//app.MapPut("/v1/transactions", () => "API is Running");
//app.MapDelete("/v1/transactions", () => "API is Running");


app.Run();

// Request
public class Request
{
    public string Title { get; set; } = string.Empty;
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
public class Handler
{
    public Response Handle(Request request)
    {
        // Aqui você pode implementar a lógica para criar uma nova transação
        // Por exemplo, salvar no banco de dados e retornar a resposta
        return new Response
        {
            Id = 1, // Simulando um ID gerado pelo banco de dados
            Title = request.Title,
            CreatedAt = request.CreatedAt,
            Type = request.Type,
            Amount = request.Amount,
            CategoryId = request.CategoryId,
            UserId = request.UserId
        };
    }
}
