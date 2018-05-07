using Microsoft.Extensions.Primitives;
using System.Linq;
using WebApiNetCore.Entities;

namespace WebApiNetCore.Repositories
{
    public class SecretRepository : ISecretRepository
    {
        private readonly IInvoiceContext context;

        public SecretRepository(IInvoiceContext context)
        {
            this.context = context;
        }

        public bool CheckValidUserSecret(StringValues stringValues)
        {
            return context.Secrets.Any(x => x.Key == stringValues);
        }
    }
}