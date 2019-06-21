using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataTransferObjects.HttpBodies.Response
{
    public class LoginResponse
    {
        public LoginResponse()
        {
            Success = false;
            Message = "Invalid login try.";
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public string Jwt { get; set; }
    }
}
