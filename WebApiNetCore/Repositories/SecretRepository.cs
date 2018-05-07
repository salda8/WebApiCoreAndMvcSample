using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
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

        public bool CheckValidUserSecret(StringValues stringValues) {

            return context.Secrets.Any(x => x.Key == stringValues);
        }


    }
}
