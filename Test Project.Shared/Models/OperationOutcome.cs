using System;
using System.Collections.Generic;
using System.Text;
using Test_Project.Shared.Resources;

namespace Test_Project.Shared.Models
{
    public class OperationOutcome
    {
        public bool IsSuccess { get; protected set; }
        public string ErrorMessage { get; protected set; }
        public OperationOutcome(bool IsSuccess, string ErrorMessage)
        {
            this.IsSuccess = IsSuccess;
            this.ErrorMessage = ErrorMessage;
        }
    }
}
