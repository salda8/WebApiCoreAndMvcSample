using System;

namespace WebApiNetCore.Repositories
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
