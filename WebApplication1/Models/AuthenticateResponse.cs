namespace WebApplication1.Models
{
    public class AuthenticateResponse
    {
        public string access_token { get; set; }
        public string userName { get; set; }

        public string changePasswordOnLogin { get; set; }
    }
}