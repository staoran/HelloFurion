using Furion.DataEncryption;

Serve.Run(services => { services.AddJwt(); });

[DynamicApiController]
public class HelloService
{
    public string Say()
    {
        var payload = new Dictionary<string, object>()
        {
            { "Id", "Id" },
            { "Account", "Account" },
            { "Name", "Name" },
            { "OrgId", "Id" },
            { "OrgName", "Name" },
            { "IsAdmin", "isAdmin" }
        };
        // "IssuerSigningKey": "NCj3K8XNavMpXqXkNxyYB9XoKL4Xpef"
        // 31 位
        return JWTEncryption.Encrypt(payload);
    }
}