using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using WebApiNetCore;
using Xunit;

namespace Tests.Filters
{
    public class ValidateModelStateAttributeTests : IDisposable
    {
        private MockRepository mockRepository;



        public ValidateModelStateAttributeTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        public void Dispose()
        {
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public void InvalidModelStateShouldReturnBadRequestObjectResult()
        {
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
            context.ModelState.AddModelError("id", "missing");
                  
            
            // Act
            ValidateModelStateAttribute validateModelStateAttribute = this.CreateValidateModelStateAttribute();
            validateModelStateAttribute.OnActionExecuting(context);
            Assert.NotNull(context);
            Assert.IsType<BadRequestObjectResult>(context.Result);


            // Assert

        }

        private ValidateModelStateAttribute CreateValidateModelStateAttribute()
        {
            return new ValidateModelStateAttribute();
        }
    }
}
