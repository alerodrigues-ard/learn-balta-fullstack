using Dima.Api.Common.Api;
using Dima.Api.Endpoints;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContexts();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

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

