using System.Net.Http.Json;
using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Core.Responses;

namespace Dima.Web.Handlers;

public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
{
    private readonly HttpClient _client =  httpClientFactory.CreateClient(Configuration.HttpClienName);
    
    public async Task<Response<string>> LoginAsync(LoginRequest request)
    {
        await _client.PostAsJsonAsync("v1/identity", request);
    }

    public Task<Response<string>> RegisterAsync(RegisterRequest request)
    {
        throw new NotImplementedException();
    }

    public Task LogoutAsync()
    {
        throw new NotImplementedException();
    }
}