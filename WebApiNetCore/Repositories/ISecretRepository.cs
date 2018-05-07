using Microsoft.Extensions.Primitives;

namespace WebApiNetCore.Repositories
{
    public interface ISecretRepository
    {
        bool CheckValidUserSecret(StringValues stringValues);
    }
}