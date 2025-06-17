using Dima.Core.Enums;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Request
// GET, POST, PUT, DELETE
// Obter, Criar, Atualizar, Excluir - CRUD
// GET (NÃO DEVE TER CORPO NA REQUISIÇÃO)
// Requisição -> Cabeçalho (Headers) + Corpo (Body)
// Cabeçalho: http://localhost:5000/v1/categories/1
// POST, PUT, DELETE (DEVE TER CORPO NA REQUISIÇÃO - DELETE É OPCIONAL)
// JSON - JavaScript Object Notation
// Exemplo de JSON: { "name": "Alexandre" }
//
// Binding -> Vínculo, Ligação, Elo
// Transformar o objeto JSON em um objeto C#
//
// Response
// Tem cabeçalho e corpo
// Status Code: 200, 201, 204, 400, 404, 500, etc.

//app.MapGet("/v1/transactions", () => "API is Running");
app.MapPost("/v1/transactions", () => new { message = "API is Running" });
//app.MapPut("/v1/transactions", () => "API is Running");
//app.MapDelete("/v1/transactions", () => "API is Running");


app.Run();

// Request
public class Request
{
    public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
//    public DateTime? PaidOrReceivedAt { get; set; }
    public int /*ETransactionType*/ Type { get; set; } //= ETransactionType.Withdrawal;
    public decimal Amount { get; set; }
    public long CategoryId { get; set; }
    public string UserId { get; set; } = string.Empty;
}


// Response

// Handler

