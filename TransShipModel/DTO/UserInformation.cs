namespace TransShipModel.DTO {
    public class UserInformation
    {
        public decimal id { get; set; }
        public decimal? customerId { get; set; }
        public decimal? userId { get; set; }
        public string FullName { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string PasswordVerify { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string user_type { get; set; }
        public string CompanyName { get; set; }       
    }
}
