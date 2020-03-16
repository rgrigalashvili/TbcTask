using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Project.BLL.Models;

namespace Test_Project.API.ActionFilter
{
    public class ValidationActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var model in context.ActionArguments.Values)
            {
                if (model is BaseModel validationModel)
                {
                    var validation = validationModel.Validate();

                    if (!validation.IsSuccess)
                    {
                        context.Result = new OkObjectResult(validation.ErrorMessage);
                    }
                }
            }
        }
    }
}
