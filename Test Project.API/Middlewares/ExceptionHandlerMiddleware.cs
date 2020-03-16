using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Project.BLL.Managers.Interfaces;
using Test_Project.Shared.Models;

namespace Test_Project.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate request;

        public ExceptionHandlerMiddleware( RequestDelegate request)
        {
            this.request = request;
        }
        public async Task InvokeAsync(HttpContext context, IExceptionLogManager exceptionLogManager)
        {
            await exceptionLogManager.LogException(context);

            await request(context);
        }
    }
}
