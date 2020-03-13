using System;
namespace DriverTracker.Server.Models
{
    public class LoginModel
    {
        public InputModel Input { get; set; }

        public LoginModel()
        {
        }

        public class InputModel
        {
            public String Email { get; set; }
            public String Password { get; set; }
            public bool RememberMe { get; set; }
        }
    }
}
