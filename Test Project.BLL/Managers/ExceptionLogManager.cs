using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test_Project.BLL.Managers.Interfaces;
using Test_Project.Shared.Models;
using Test_Project.Shared.Resources;

namespace Test_Project.BLL.Managers
{
    public class ExceptionLogManager : IExceptionLogManager
    {
        private readonly ILogger<ExceptionLogManager> logger;
        public ExceptionLogManager(ILogger<ExceptionLogManager> logger)
        {
            this.logger = logger;
        }
        public async Task<OperationOutcome> LogException(HttpContext httpContext)
        {
            var exceptionHandlerPathFeature = httpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature != null)
            {
                var errorMessage = exceptionHandlerPathFeature.Error.Message;
                var stackTrace = exceptionHandlerPathFeature.Error.StackTrace;

                logger.LogError($"ErrorMessage:{ errorMessage}, StackTrace:{ stackTrace}");

            }
            return await Task.FromResult(new OperationOutcome(false, GlobalResource.Error));
        }
    }
}
