using System;
using System.Linq;
using WebApiNetCore.Entities;
using WebApiNetCore.Repositories;

namespace WebApiNetCore.Helpers
{
    public static class ExceptionHelpers
    {
        public static void ThrowNotFoundIfNull(object entity,string className, int id)
        {
            if (entity == null)
            {
                throw new NotFoundException($"{className}. ID:{id} not found");
            }

        }
    }
}