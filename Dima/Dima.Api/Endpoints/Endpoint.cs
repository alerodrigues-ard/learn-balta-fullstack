using Dima.Api.Common.Api;
using Dima.Api.Endpoints.Categories;
using Dima.Api.Endpoints.Transactions;
using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Dima.Api.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => new { message = "Dima API is running" });

        var endpoints = app
            .MapGroup("");

        endpoints.MapGroup("v1/categories")
            .WithTags("Categories")
            //.RequireAuthorization()
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>()
            .MapEndpoint<GetCategoryByIdEndpoint>()
            .MapEndpoint<GetAllCategoriesEndpoint>();

        endpoints.MapGroup("v1/transactions")
            .WithTags("Transactions")
            //.RequireAuthorization()
            .MapEndpoint<CreateTransactionEndpoint>()
            .MapEndpoint<UpdateTransactionEndpoint>()
            .MapEndpoint<DeleteTransactionEndpoint>()
            .MapEndpoint<GetTransactionByIdEndpoint>()
            .MapEndpoint<GetTransactionsByPeriodEndpoint>();

        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapIdentityApi<User>();

        endpoints.MapGroup("v1/identity")
            .WithTags("Identity")
            .MapPost("/logout", async (
                SignInManager<User> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return Results.Ok();
            })
            .RequireAuthorization();

        endpoints.MapGroup("v1/identity")
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

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
    
}