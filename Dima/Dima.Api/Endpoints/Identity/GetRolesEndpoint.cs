using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Models.Account;

namespace Dima.Api.Endpoints.Identity;

public class GetRolesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app
            .MapGet("/roles", Handle)
            .RequireAuthorization();

    private static Task<IResult> Handle(ClaimsPrincipal user)
    {
        // O ClaimsPrincipal pega dados do usuário do cookie (logado), evitando ir ao banco de dados,
        // porém, enquanto o cookie for válido, os dados do usuário não serão atualizados
        
        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return Task.FromResult(Results.Unauthorized());
        
        // Códigos compatíveis (mantida a forma com Pattern Matching)
        // var identity = user.Identity as ClaimsIdentity;
        // if (identity is null)   
        if (user.Identity is not ClaimsIdentity identity)
            return Task.FromResult(Results.Empty);
        
        var roles = identity
            .FindAll(identity.RoleClaimType)
            .Select(c => new RoleClaim
            {
                // Padrão de dados de role para o frontend
                Issuer = c.Issuer,
                OriginalIssuer = c.OriginalIssuer,
                Type = c.Type,
                Value = c.Value,
                ValueType = c.ValueType
            });
        
        return Task.FromResult<IResult>(TypedResults.Json(roles));
    }
}