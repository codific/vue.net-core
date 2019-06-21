using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataTransferObjects.HttpBodies.Response
{
    public class RegisterResponse
    {
        public RegisterResponse()
        {
            Success = false;
            Message = "Invalid registration try.";
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
