using Dima.Core.Enums;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer(); // Necessário para o Swagger
builder.Services.AddSwaggerGen( // Necessário para o Swagger
    x =>
    {
        x.CustomSchemaIds(type => type.FullName ?? type.Name); // Necessário para o Swagger colocar o Full Qualified Name
    });

var app = builder.Build();

app.UseSwagger(); // Necessário para o Swagger
app.UseSwaggerUI();

//app.MapGet("/v1/transactions", () => "API is Running");

app.MapPost("/v1/transactions", (Request request, Handler handler) => handler.Handle(request))
    .WithName("Transactions: Create")           // Ajuda na documentação do Swagger
    .WithSummary("Cria uma nova transa��o")     // Ajuda na documentação do Swagger
    .Produces<Response>();                      // Garante que a resposta ser� do tipo Response

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
        // Aqui você pode implementar a lógica para criar uma nova transa��o
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
