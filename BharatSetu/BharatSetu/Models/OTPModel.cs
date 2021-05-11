namespace BharatSetu.Models
{
    public class AuthConfirm
    {
        public string token { get; set; }
    }
    public class ConfirmAuthentication
    {
        public string otp { get; set; }
        public string txnId { get; set; }
    }

    public class Mobile
    {
        public string mobile { get; set; }
    }

    public class Authenticate
    {
        public string txnId { get; set; }
    }
}
