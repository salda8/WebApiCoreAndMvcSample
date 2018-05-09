using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Moq;
using System;
using System.Collections.Generic;
using WebApiNetCore.Middleware;
using Xunit;
namespace Tests.Middleware
{
    public class ErrorHandlingMiddlewareTests : IDisposable
    {
        private MockRepository mockRepository;

        private Mock<RequestDelegate> mockRequestDelegate;

        public ErrorHandlingMiddlewareTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockRequestDelegate = this.mockRepository.Create<RequestDelegate>();
        }

        public void Dispose()
        {
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var context = new ActionExecutingContext(
                new ActionContext
                {
                    HttpContext = httpContext,
                    RouteData = new RouteData(),
                    ActionDescriptor = new ActionDescriptor(),

                },
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                new Mock<Controller>().Object);

            // Act
            ErrorHandlingMiddleware errorHandlingMiddleware = this.CreateErrorHandlingMiddleware();
            //errorHandlingMiddleware.Invoke(context);
            

            // Assert

        }

        private ErrorHandlingMiddleware CreateErrorHandlingMiddleware()
        {
            return new ErrorHandlingMiddleware(
                this.mockRequestDelegate.Object);
        }
    }
}
