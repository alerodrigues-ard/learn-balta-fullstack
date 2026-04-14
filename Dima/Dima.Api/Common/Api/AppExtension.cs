using Scalar.AspNetCore;

namespace Dima.Api.Common.Api;

public static class AppExtension
{
    public static void ConfigureDevEnvironment(this WebApplication app)
    {
        app.UseDeveloperExceptionPage();

        // OpenAPI JSON endpoint
        app.MapOpenApi();  // /openapi/v1.json

        // Swagger UI nativo (Scalar)
        app.MapScalarApiReference();  // /scalar/v1 (UI moderna)
        
        // app.UseSwagger();
        // app.UseSwaggerUI();
        // app.MapSwagger().RequireAuthorization();
    }

    public static void UseSecurity(this WebApplication app)
    {
        // As suas chamadas abaixo devem estar nesta ordem
        app.UseAuthentication();    // Quem eu sou
        app.UseAuthorization();     // O que eu posso fazer
    }
}