using Moq;
using System;
using WebApiNetCore.Entities;
using WebApiNetCore.Helpers;
using WebApiNetCore.Repositories;
using Xunit;

namespace Tests.Helpers
{
    public class ExceptionHelpersTests : IDisposable
    {
        private MockRepository mockRepository;



        public ExceptionHelpersTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        public void Dispose()
        {
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void TestItThrowNotFoundIfNull()
        {
         
            Invoice invoice = null;

            Assert.Throws<NotFoundException>(() => ExceptionHelpers.ThrowNotFoundIfNull(invoice, "invoice", 1 ));
            ExceptionHelpers.ThrowNotFoundIfNull(invoice = new Invoice(), "invoice", 1);



        }

        
    }
}
