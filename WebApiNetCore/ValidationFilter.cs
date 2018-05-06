using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;


namespace WebApiNetCore
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var descriptor = actionContext.ActionDescriptor as ControllerActionDescriptor;

            if (descriptor != null)
            {
                foreach (var parameter in descriptor.MethodInfo.GetParameters())
                {
                    var argument = actionContext.ActionArguments[parameter.Name];

                    EvaluateValidationAttributes(parameter, argument, actionContext.ModelState);
                }
            }
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Result = new BadRequestObjectResult(actionContext.ModelState);
            }
        }

        private void EvaluateValidationAttributes(ParameterInfo parameter, object argument, ModelStateDictionary modelState)
        {
            foreach (var attributeData in parameter.CustomAttributes)
            {
                var attributeInstance = CustomAttributeExtensions.GetCustomAttribute(parameter, attributeData.AttributeType);

                var validationAttribute = attributeInstance as ValidationAttribute;

                if (validationAttribute != null)
                {
                    var isValid = validationAttribute.IsValid(argument);
                    if (!isValid)
                    {
                        modelState.AddModelError(parameter.Name, validationAttribute.FormatErrorMessage(parameter.Name));
                    }
                }
            }
        }
    }

    public class AuthenticationFilter : ActionFilterAttribute,IAuthorizationFilter
    {
        private string secrets = "Secret007";

        
        public void OnAuthorization(AuthorizationFilterContext context) {
            context.HttpContext.Request.Headers.TryGetValue("X-API-KEY", out Microsoft.Extensions.Primitives.StringValues value);
            if (!(value.Count>0 && value.SingleOrDefault().Equals(secrets)))
            {
               context.Result = new UnauthorizedResult();
            }


        }
    }

    public class AuthenticationFailureResult : IActionResult
    {
        public AuthenticationFailureResult()
        {
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return Task.FromResult(new UnauthorizedResult());
        }
    }
}