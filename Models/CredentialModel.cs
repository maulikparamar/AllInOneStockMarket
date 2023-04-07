using System;

namespace AllinOneStock.Models
{
    public class CredentialModel
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public Int16 VerificationCode { get; set; }

        public userType type { get; set; }
    }

    public class ChangePasswordModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }

    }

    public class ChangeVerificationModel
    {
        public string UserName { get; set; }
        public Int16 VerificationCode { get; set; }
        public Int16 NewVerificationCode { get; set; }

    }

    public enum userType{
        Admin = 0,
        Client = 1,
        PortfolioManager = 2
    }
}
