using System.Security.Claims;
using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;
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
        
        app.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapIdentityApi<User>();

        app.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapPost("/logout", async (
                SignInManager<User> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return Results.Ok();
            })
            .RequireAuthorization();

        app.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapGet("/roles", (
                ClaimsPrincipal user) =>
            {
                // O ClaimsPrincipal pega dados do usuário do cookie (logado), evitando ir ao banco de dados,
                // porém, enquanto o cookie for válido, os dados do usuário não serão atualizados
        
                if (user.Identity is null || !user.Identity.IsAuthenticated)
                    return Results.Unauthorized();
        
                // Códigos compatíveis (mantida a forma com Pattern Matching)
                // var identity = user.Identity as ClaimsIdentity;
                // if (identity is null)   
                if (user.Identity is not ClaimsIdentity identity)
                    return Results.Empty;
        
                var roles = identity
                    .FindAll(identity.RoleClaimType)
                    .Select(c => new
                    {
                        // Padrão de dados de role para o frontend
                        c.Issuer,
                        c.OriginalIssuer,
                        c.Type,
                        c.Value,
                        c.ValueType
                    });
        
                return TypedResults.Json(roles);
            })
            .RequireAuthorization();
        
    }
}