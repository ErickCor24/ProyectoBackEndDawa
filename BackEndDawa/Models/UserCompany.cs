namespace BackEndDawa.Models
{
    public class UserCompany
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        //Company relation
        public int CompanyId { get; set; }
        public Company? Company { get; set; }

    }
}
