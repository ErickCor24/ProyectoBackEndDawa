namespace BackEndDawa.Models
{
    public class UserClient
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Status { get; set; }

        //Client relation
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        
    }
}
