using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test_Project.Shared.Models;

namespace Test_Project.BLL.Managers.Interfaces
{
    public interface IExceptionLogManager
    {
        Task<OperationOutcome> LogException(HttpContext httpContext);
    }
}
