namespace TransShip.Objects
{
    public class UserInformation
    {
        public decimal id { get; set; }
        public decimal? customerId { get; set; }
        public string FullName { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string PasswordVerify { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string CompanyName { get; set; }       
    }
}
