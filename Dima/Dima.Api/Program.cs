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

app.MapGet("/", () => "API is Running");

app.Run();
