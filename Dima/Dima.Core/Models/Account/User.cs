namespace Dima.Core.Models.Account;

public class User
{
    // Dados da Url padrão do Identity v1/identity/manage/info
    public string Email { get; set; } = string.Empty;
    public Dictionary<string, string> Claims { get; set; } = [];
}