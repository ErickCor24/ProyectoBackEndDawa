namespace BackEndDawa.Models
{
    public class Client
    {

        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Ci { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public string Nationality { get; set; } = string.Empty;
        public bool Status { get; set; }

    }
}
